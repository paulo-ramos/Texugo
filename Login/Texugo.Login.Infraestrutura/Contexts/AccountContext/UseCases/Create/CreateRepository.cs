using Microsoft.EntityFrameworkCore;
using Texugo.Login.Core.Contexts.AccountContext.Entities;
using Texugo.Login.Core.Contexts.AccountContext.UseCases.Create.Contracts;
using Texugo.Login.Infraestrutura.Data;

namespace Texugo.Login.Infraestrutura.Contexts.AccountContext.UseCases.Create;

public class CreateRepository : ICreateRepository
{
    private readonly AppDbContext _context;

    public CreateRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> AnyAsync(string email, CancellationToken cancellationToken)
    {
        var result = _context
            .Users
            .AsNoTracking()
            .AnyAsync(x => x.Email.Address == email, cancellationToken:cancellationToken)
            .Result;
        
        return result;

    }

    public async Task SaveAsync(User user, CancellationToken cancellationToken)
    {
        await _context.Users.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}