using OptSfa.Migration.Domain.Models;

namespace OptSfa.Migration.Domain.Interfaces;

public interface IClientRepository
{
    Task<ClientMaster?> GetByIdAsync(int id, CancellationToken ct = default);
    Task AddAsync(ClientMaster customer, CancellationToken ct = default);
    Task SaveChangesAsync(CancellationToken ct = default);
}
