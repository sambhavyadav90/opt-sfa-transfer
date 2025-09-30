using OptSfa.Migration.Data.Context;
using OptSfa.Migration.Domain.Interfaces;
using OptSfa.Migration.Domain.Models;

namespace OptSfa.Migration.Data.Repository;

public class ClientRepository : IClientRepository
{
    private readonly AppDbContext _db;
    public ClientRepository(AppDbContext db) => _db = db;

    public async Task AddAsync(ClientMaster customer, CancellationToken ct = default)
    {
        await _db.DbClientMaster.AddAsync(customer, ct);
    }

    public async Task<ClientMaster?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        return await _db.DbClientMaster.FindAsync(new object[] { id }, ct);
    }

    public async Task SaveChangesAsync(CancellationToken ct = default)
    {
        await _db.SaveChangesAsync(ct);
    }
}
