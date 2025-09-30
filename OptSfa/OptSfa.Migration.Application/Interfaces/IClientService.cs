using OptSfa.Migration.Domain.Models;

namespace OptSfa.Migration.Application.Interfaces;

public interface IClientService
{
    Task<ClientMaster?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task AddAsync(ClientMaster customer, CancellationToken ct = default);
    Task SaveChangesAsync(CancellationToken ct = default);
}
