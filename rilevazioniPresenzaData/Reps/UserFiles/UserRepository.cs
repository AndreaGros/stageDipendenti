using rilevazioniPresenzaData.Models;

namespace rilevazioniPresenzaData.Reps.UserFiles
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
            var userDetails = context.Users.FirstOrDefault(u => u.Matricola == key);
            return userDetails;
        }

        public List<User> GetEmps()
        {
            var usersList= context.Users.ToList();
            //var usersList = context.Users.GetType().GetProperty(status).GetValue(filter);
            return usersList;
        }

        public bool AddEmp(User employer)
        {
            var added = context.Users.Add(employer);
            int result = context.SaveChanges();
            if(result > 0)
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

        public bool UpdateEmp(User employer)
        {
            var dbEmp=context.Users.FirstOrDefault(u => u.Matricola == employer.Matricola);
            if (dbEmp != null)
            {
                context.Entry(dbEmp).CurrentValues.SetValues(employer);
                context.SaveChanges();
                return true;
            }
            return false;
        }

    }
}
