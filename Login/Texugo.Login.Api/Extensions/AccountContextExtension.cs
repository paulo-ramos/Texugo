using MediatR;
using SendGrid;
using Texugo.Login.Core.Contexts.AccountContext.UseCases.Authenticate;
using Texugo.Login.Core.Contexts.AccountContext.UseCases.Authenticate.Contracts;
using Texugo.Login.Core.Contexts.AccountContext.UseCases.Create;
using Texugo.Login.Core.Contexts.AccountContext.UseCases.Create.Contracts;
using Texugo.Login.Infraestrutura.Contexts.AccountContext.UseCases.Authenticate;
using Texugo.Login.Infraestrutura.Contexts.AccountContext.UseCases.Create;

namespace Texugo.Login.Api.Extensions;

public static class AccountContextExtension
{
    public static void AddAccountContext(this WebApplicationBuilder builder)
    {
        #region Create

        builder.Services.AddTransient<ICreateRepository, CreateRepository>();
        builder.Services.AddTransient<ICreateService, CreateService>();

        #endregion
        
        #region Authenticate

        builder.Services.AddTransient<IAuthenticateRepository, AuthenticateRepository>();

        #endregion
    }

    public static void MapAccountEndpoints(this WebApplication app)
    {
        #region Create

        app.MapPost("api/v1/users", async (
            CreateRequest request,
            IRequestHandler<CreateRequest, CreateResponse> handler) =>
        {
            var response = await handler.Handle(request, new CancellationToken());
            return response.IsSuccess
                ? Results.Created($"api/v1/users/{response.Data?.Id}", response)
                : Results.Json(response, statusCode: response.Status);
        }); //.RequireAuthorization();
        
        #endregion
        
        
        #region Authenticate

        app.MapPost("api/v1/authenticate", async (
            AuthenticateRequest request,
            IRequestHandler<AuthenticateRequest, AuthenticateResponse> handler) =>
        {
            var response = await handler.Handle(request, new CancellationToken());

            if (!response.IsSuccess)
            {
                return Results.Json(response, statusCode: response.Status);
            }

            if (response.Data is null)
            {
                return Results.Json(response, statusCode: 500);
            }

            response.Data.Token = JwtExtension.Generate(response.Data);
            
            return Results.Ok(response);
            
        });  //.RequireAuthorization();
        
        #endregion

    }
    
}