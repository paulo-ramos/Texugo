using MediatR;
using Texugo.Login.Core.Contexts.AccountContext.Entities;
using Texugo.Login.Core.Contexts.AccountContext.UseCases.Authenticate.Contracts;

namespace Texugo.Login.Core.Contexts.AccountContext.UseCases.Authenticate;

public class AuthenticateHandler : IRequestHandler<AuthenticateRequest, AuthenticateResponse>
{
    private readonly IAuthenticateRepository _repository;

    public AuthenticateHandler(IAuthenticateRepository repository)
    {
        _repository = repository;
    }

    public async Task<AuthenticateResponse> Handle(AuthenticateRequest request, CancellationToken cancellationToken)
    {
        #region 01 - Valida Requisição

        try
        {
            var res = AuthenticateSpecification.Ensure(request);
            if (!res.IsValid)
            {
                return new AuthenticateResponse("Requisição inválida", 400, res.Notifications);
            }
        }
        catch 
        {
            return new AuthenticateResponse("Não foi possível validar sua requisição.", 500);
        }

        #endregion

        #region 02 - obter o perfil atual no banco de dados

        User? user;
        try
        {
            user = await _repository.GetUserByEmailAsync(request.Email, cancellationToken);
            if (user is null)
            {
                return new AuthenticateResponse("Perfil não localizado.", 400);
            }
        }
        catch (Exception e)
        {
            return new AuthenticateResponse("Ocorreu um erro ao localizar o perfil no banco de dados.", 500);
        }

        #endregion

        #region 03 - Validar a senha

        try
        {
            if (!user.Password.Challenge(request.Password))
            {
                return new AuthenticateResponse("Usuário ou senha inválidos.", 400);
            }
        }
        catch (Exception e)
        {
            return new AuthenticateResponse("Usuário ou senha inválidos.", 500);
        }
        

        #endregion

        #region 04 - Confirmar se a conta já foi verificada

        try
        {
            if (!user.Email.Verification.IsActive)
            {
                return new AuthenticateResponse("Conta inativa.", 400);
            }
        }
        catch 
        {
            return new AuthenticateResponse("Não foi possívekl verificar seu perfil.", 500);
        }

        #endregion

        #region 05 - retornar os dados

        try
        {
            var data = new ResponseData
            {
                Id = user.Id.ToString(),
                Name = user.Name,
                Email = user.Email,
                Roles = user.Roles.Select(x => x.Name).ToArray()
            };

            return new AuthenticateResponse("ok", data);
        }
        catch
        {
            return new AuthenticateResponse("Não foi possivel obter dados do perfil", 500);
        }

        #endregion
    }
}