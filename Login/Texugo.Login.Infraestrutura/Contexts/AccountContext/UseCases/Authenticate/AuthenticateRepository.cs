using Microsoft.EntityFrameworkCore;
using Texugo.Login.Core.Contexts.AccountContext.Entities;
using Texugo.Login.Core.Contexts.AccountContext.UseCases.Authenticate.Contracts;
using Texugo.Login.Infraestrutura.Data;

namespace Texugo.Login.Infraestrutura.Contexts.AccountContext.UseCases.Authenticate;

public class AuthenticateRepository : IAuthenticateRepository
{
    private readonly AppDbContext _context;

    public AuthenticateRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await _context
            .Users
            .AsNoTracking()
            .Include(x => x.Roles)
            .FirstOrDefaultAsync(x => x.Email.Address == email, cancellationToken);
    }
}