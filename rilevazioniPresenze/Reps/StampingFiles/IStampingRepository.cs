using rilevazioniPresenzaData.Models;
using rilevazioniPresenze.DTOs;

namespace rilevazioniPresenze.Reps.StampingFiles
{
    public interface IStampingRepository
    {
        List<Stamping> GetStamps();
        bool AddStamp(Stamping stamp);
        bool RemoveStamp(ShiftType shiftType, DateTime orario);
        Stamping GetStampByKey(ShiftType shiftType, DateTime orario);
        bool UpdateStamp(ShiftType shiftType, DateTime orario);

    }
}
