using rilevazioniPresenzaData;
using rilevazioniPresenzaData.Models;
using rilevazioniPresenze.DTOs;

namespace rilevazioniPresenza.Reps.UserFiles
{
    public class UserRepository : IUserRepository, IDisposable
    {
        RilevazionePresenzaContext context = new RilevazionePresenzaContext();
        public void Dispose()
        {
            context.Dispose();
        }

        public User? GetAllDetailsByKey(string key)
        {
            //var userDetails = context.Users.FirstOrDefault(u => u.Matricola == key);
            var userDetails = context.Users.Find(key);
            return userDetails;
        }

        public List<User> GetEmps(string isAdmin)
        {
            List<User> usersList;
            if(isAdmin == "admin")
                usersList = context.Users.ToList();
            else
                usersList = context.Users.Where(u => !u.Admin).ToList();
            //var usersList = context.Users.GetType().GetProperty(status).GetValue(filter);
            return usersList;
        }

        public bool AddEmp(User employer)
        {
            var added = context.Users.Add(employer);
            int result = context.SaveChanges();
            if (result > 0)
                return true;
            return false;
        }

        public bool RemoveEmp(string key)
        {
            var employer = context.Users.FirstOrDefault(u => u.Matricola == key);

            if (employer != null)
            {
                context.Users.Remove(employer);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool UpdateEmp(DetailUserDTOs employer)
        {
            var dbEmp = context.Users.FirstOrDefault(u => u.Matricola == employer.Matricola);
            if (dbEmp != null)
            {
                context.Entry(dbEmp).CurrentValues.SetValues(employer);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public User? GetUserByUsername(string username)
        {
            var user = context.Users.FirstOrDefault(u => u.Username == username);
            if (user == null)
                return null;
            return user;
        }
    }
}
