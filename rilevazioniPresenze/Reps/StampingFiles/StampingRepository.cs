using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using rilevazioniPresenzaData;
using rilevazioniPresenzaData.Models;
using rilevazioniPresenze.DTOs;


namespace rilevazioniPresenze.Reps.StampingFiles
{
    public class StampingRepository : IStampingRepository
    {
        RilevazionePresenzaContext _context;

        public StampingRepository(RilevazionePresenzaContext context)
        {
            _context = context;
        }
        public List<Stamping> GetStamps(string? matricola)
        {
            if (matricola == null)
                return _context.Stampings.ToList();
            else
                return _context.Stampings.Where(s => s.IdMatricola == matricola).OrderBy(s => s.Orario).ToList();
        }

        public bool AddStamp(Stamping stamp)
        {
            _context.Stampings.Add(stamp);
            return _context.SaveChanges() > 0;
        }

        public bool RemoveStamp(int key)
        {
            var stamp = _context.Stampings.FirstOrDefault(s => s.Id == key);

            if (stamp != null)
            {
                _context.Stampings.Remove(stamp);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        public bool UpdateStamp(int key, StampingWithoutIdDTOs stamp)
        {
            var dbStamp = _context.Stampings.FirstOrDefault(s => s.Id == key);
            if (dbStamp != null)
            {
                dbStamp.IdMatricola = stamp.IdMatricola;
                dbStamp.ShiftType = stamp.ShiftType;
                dbStamp.Orario = stamp.Orario;

                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
