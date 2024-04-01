using MediatR;
using Texugo.Login.Core.Contexts.AccountContext.Entities;
using Texugo.Login.Core.Contexts.AccountContext.UseCases.Create.Contracts;
using Texugo.Login.Core.Contexts.AccountContext.ValueObjects;

namespace Texugo.Login.Core.Contexts.AccountContext.UseCases.Create;

public class CreateHandler : IRequestHandler<CreateRequest, CreateResponse>
{
    private readonly ICreateRepository _createRepository;
    private readonly ICreateService _createService;

    public CreateHandler(ICreateRepository createRepository, ICreateService createService)
    {
        _createRepository = createRepository;
        _createService = createService;
    }

    public async Task<CreateResponse> Handle(CreateRequest createRequest, CancellationToken cancellationToken)
    {
        #region 01. - validar requisição

        try
        {
            var result = CreateSpecification.Ensure(createRequest);
            if (!result.IsValid)
            {
                return new CreateResponse("Requisição inválida!", 400, result.Notifications);
            }
        }
        catch (Exception e)
        {
            return new CreateResponse("Ocorreu um erro ao validar sua requisição!", 500);
        }
        
        #endregion
        
        #region 02 - gerar os objetos

        Email email;
        Password password;
        User user;
        try
        {
            email = new Email(createRequest.Email);
            password = new Password(createRequest.Password);
            user = new User(createRequest.Name, email, password);
        }
        catch (Exception ex)
        {
            return new CreateResponse(ex.Message, 400);
        }

        #endregion

        #region 03 - verificar se usuário existe no DB

        try
        {
            var exists = await _createRepository.AnyAsync(createRequest.Email, cancellationToken);

            if (exists)
            {
                return new CreateResponse("Este email já foi utilizado.", 400);
            }
        }
        catch (Exception e)
        {
            return new CreateResponse("Ocorreu um ero ao verificar o cadastro no Banco de Dados.", 500);
        }

        #endregion

        #region 04 - persistir os dados

        try
        {
            await _createRepository.SaveAsync(user, cancellationToken);
        }
        catch 
        {
            return new CreateResponse("falha ao persistir os dados.", 500);
        }
        #endregion

        #region 05 - enviar email de ativação

        try
        {
            await _createService.SendVerificationEmailAsync(user, cancellationToken);
        }
        catch
        {
            // do nothing 
        }
        #endregion

        return new CreateResponse("Conta criada.", new ResponseData(user.Id, user.Name, user.Email));

    }
}