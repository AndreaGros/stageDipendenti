using rilevazioniPresenzaData.Models;

namespace rilevazioniPresenze.Reps.StampingFiles
{
    public interface IStampingRepository
    {
        List<Stamping> GetStamps();
        bool AddStamp(Stamping stamp);
        bool RemoveStamp(string idMatricola, ShiftType shiftType, DateTime orario);
        Stamping? GetStampByKey(string idMatricola, ShiftType shiftType, DateTime orario);
    }
}
