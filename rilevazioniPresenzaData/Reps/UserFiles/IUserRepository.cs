using rilevazioniPresenzaData.Models;

namespace rilevazioniPresenzaData.Reps.UserFiles
{
    public interface IUserRepository
    {
        User? GetAllDetailsByKey(string key);
        List<User> GetEmps();
        bool AddEmp(User employer);
        bool RemoveEmp(string key);
        bool UpdateEmp(User employer);
    }
}
