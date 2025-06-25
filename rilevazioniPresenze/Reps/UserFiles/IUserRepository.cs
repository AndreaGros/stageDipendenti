using rilevazioniPresenzaData.Models;
using rilevazioniPresenze.DTOs;


namespace rilevazioniPresenza.Reps.UserFiles
{
    public interface IUserRepository
    {
        User? GetAllDetailsByKey(string key);
        List<User> GetEmps(string role, FilterDTOs filters);
        //bool AddEmp(User employer, List<UserShiftDTOs> userShifts);
        bool AddEmp(User user);
        bool RemoveEmp(string key);
        bool UpdateEmp(DetailUserDTOs employer);
        User? GetUserByUsername(string username);
        List<string> GetIds();
        List<UserShiftDTOs> GetShiftsByKey(string key);
    }
}
