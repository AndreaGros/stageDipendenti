using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using rilevazioniPresenzaData;
using rilevazioniPresenzaData.Models;
using rilevazioniPresenze.DTOs;

namespace rilevazioniPresenza.Reps.UserFiles
{
    public class UserRepository : IUserRepository
    {
        RilevazionePresenzaContext context;

        public UserRepository(RilevazionePresenzaContext _context)
        {
            context = _context;
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

        //public bool AddEmp(User employer,List<UserShiftDTOs> userShiftsDTOs)
        //{
        //    //using var transaction = context.Database.BeginTransaction();

        //    //var added = context.Users.Add(employer);

        //    //_ = context.SaveChanges();

        //    //List<UserShift> userShifts = new List<UserShift>();
        //    //foreach (var userShiftDTOs in userShiftsDTOs)
        //    //{
        //    //    userShifts.Add(new UserShift
        //    //    {
        //    //        IdMatricola = employer.Matricola,
        //    //        Giorno = userShiftDTOs.Giorno,
        //    //        T1 = userShiftDTOs.T1,
        //    //        FT1 = userShiftDTOs.FT1,
        //    //        T2 = userShiftDTOs.T2,
        //    //        FT2 = userShiftDTOs.FT2,
        //    //    });
        //    //}

        //    //context.UserShifts.AddRange(userShifts);

        //    //int result = context.SaveChanges();
        //    //if (result > 0)
        //    //{ 
        //    //    transaction.Commit();
        //    //    return true;
        //    //}
        //    //return false;
        //}

        public bool AddEmp(User user)
        {
            context.Users.Add(user);
            return context.SaveChanges() > 0;
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
                foreach (var userShift in employer.UserShifts)
                {
                    var shift = context.UserShifts.FirstOrDefault(us => us.IdMatricola == employer.Matricola);
                    if (shift != null)
                        context.Entry(shift).CurrentValues.SetValues(userShift);
                }
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
