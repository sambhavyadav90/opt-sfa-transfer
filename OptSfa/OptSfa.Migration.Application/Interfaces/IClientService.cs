using OptSfa.Migration.Domain.Models;
using OptSfa.Migration.Domain.ViewModel;

namespace OptSfa.Migration.Application.Interfaces;

public interface IClientService
{
    public Task<List<ClientMasterViewModel>> getAll(string empId, string clientType, string areaMain, string status, int page, int pageSize);
    public Task<ClientMasterViewModalList> createClient(string id, ClientMasterViewModalList data);

}
