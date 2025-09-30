using OptSfa.Migration.Application.Interfaces;
using OptSfa.Migration.Domain.Interfaces;
using OptSfa.Migration.Domain.Models;

namespace OptSfa.Migration.Application.Services;

public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;
    public ClientService(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async Task AddAsync(ClientMaster customer, CancellationToken ct = default)
    {
        var result = await _clientRepository.GetByIdAsync(customer.client_id, ct);
    }

    public async Task<ClientMaster?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var result = await _clientRepository.GetByIdAsync(id.GetHashCode(), ct);
        return result;
    }

    public async Task SaveChangesAsync(CancellationToken ct = default)
    {
        await _clientRepository.SaveChangesAsync(ct);
    }
}
