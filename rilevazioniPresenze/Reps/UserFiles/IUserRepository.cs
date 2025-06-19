using rilevazioniPresenzaData.Models;
using rilevazioniPresenze.DTOs;

namespace rilevazioniPresenza.Reps.UserFiles
{
    public interface IUserRepository
    {
        User? GetAllDetailsByKey(string key);
        List<User> GetEmps();
        bool AddEmp(User employer);
        bool RemoveEmp(string key);
        bool UpdateEmp(DetailUserDTOs employer);
        User? GetUserByUsername(string username);
    }
}
