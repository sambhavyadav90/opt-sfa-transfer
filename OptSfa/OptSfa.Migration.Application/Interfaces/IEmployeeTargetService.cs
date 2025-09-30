
using OptSfa.Migration.Domain.ViewModel;

namespace OptSfa.Migration.Application.Interfaces
{
    public interface IEmployeeTargetService
    {
        public Task<List<EmployeeTargetViewModel>> getAllEmployeeTarget(string empId, string itemType, string itemStatus);
    }
}