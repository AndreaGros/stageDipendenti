using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using rilevazioniPresenzaData;
using rilevazioniPresenzaData.Models;


namespace rilevazioniPresenze.Reps.StampingFiles
{
    public class StampingRepository : IStampingRepository
    {
        RilevazionePresenzaContext _context;

        public StampingRepository(RilevazionePresenzaContext context)
        {
            _context = context;
        }
        public List<Stamping> GetStamps()
        {
            return _context.Stampings.ToList();
        }

        public bool AddStamp(Stamping stamp)
        {
            _context.Stampings.Add(stamp);
            return _context.SaveChanges() > 0;
        }

        public bool RemoveStamp(string idMatricola, ShiftType shiftType, DateTime orario)
        {
            var stamp = _context.Stampings.FirstOrDefault(s => s.IdMatricola == idMatricola && s.ShiftType == shiftType && s.Orario == orario);

            if (stamp != null)
            {
                _context.Stampings.Remove(stamp);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public Stamping? GetStampByKey(string idMatricola, ShiftType shiftType, DateTime orario)
        {
            return _context.Stampings.FirstOrDefault(s => s.IdMatricola == idMatricola && s.ShiftType == shiftType && s.Orario == orario);
        }
    }
}
