using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
            //var userDetails = context.Users.Include( u => u.UserShifts.Where(us => us.Giorno == 0) ).FirstOrDefault(u => u.Matricola == key);
            var userDetails = context.Users.Include(u => u.UserShifts).FirstOrDefault(u => u.Matricola == key);
            //var userDetails2 = context.Users.ToList();
            //userDetails2=userDetails2.Where(u => u.Matricola == key).ToList();
            return userDetails;
        }

        public List<User> GetEmps(string isAdmin, FilterDTOs filters)
        {
            var query = context.Users.AsQueryable();
            List<User> usersList;

            if(filters.Stato_Lavorativo != null)
                query = query.Where(u => u.Stato_Lavorativo == filters.Stato_Lavorativo);

            if (filters.Provincia_Residenza != null)
                query = query.Where(u => u.Provincia_Residenza == filters.Provincia_Residenza);

            if (filters.Citta_Nascita != null)
                query = query.Where(u => u.Citta_Nascita == filters.Citta_Nascita);

            if (isAdmin == "admin")
                usersList = query.ToList();
            else
                usersList = query.Where(u => !u.Admin).ToList();

            return usersList;
        }

        public bool AddEmp(User employer)
        {
            
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
