using OptSfa.Migration.Application.Interfaces;
using OptSfa.Migration.Domain.Interfaces;
using OptSfa.Migration.Domain.Models;
using OptSfa.Migration.Domain.ViewModel;

namespace OptSfa.Migration.Application.Services;

public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;
    public ClientService(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async Task<List<ClientMasterViewModel>> getAll(string empId, string clientType, string areaMain, string status,int page,int page_size)
    {
        return await _clientRepository.getAll(empId, clientType, areaMain, status,page,page_size);
    }
}
