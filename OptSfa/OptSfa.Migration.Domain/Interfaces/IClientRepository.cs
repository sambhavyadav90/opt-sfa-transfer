using System.Diagnostics.Contracts;
using OptSfa.Migration.Domain.Models;
using OptSfa.Migration.Domain.ViewModel;

namespace OptSfa.Migration.Domain.Interfaces;

public interface IClientRepository
{
    public Task<List<ClientMasterViewModel>> getAll(string empId, string clientType, string areaMain, string status,int page, int pageSize);
}
