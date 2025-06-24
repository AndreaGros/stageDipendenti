using rilevazioniPresenzaData.Models;
using rilevazioniPresenze.DTOs;

namespace rilevazioniPresenze.Reps.StampingFiles
{
    public interface IStampingRepository
    {
        List<Stamping> GetStamps(string? matricola);
        bool AddStamp(Stamping stamp);
        bool RemoveStamp(int key);
        bool UpdateStamp(int key, StampingWithoutIdDTOs stamp);
    }
}
