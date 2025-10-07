namespace OptSfa.Migration.Domain.Interfaces;

public interface IMasterDatabaseRepository
{
    string GetUserCompanyConnectionString(string appKey);
}
