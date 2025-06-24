using rilevazioniPresenzaData.Models;

namespace rilevazioniPresenze.DTOs
{
    public class StampingWithoutIdDTOs
    {
        public required string IdMatricola { get; set; }

        public ShiftType ShiftType { get; set; }

        public DateTime Orario { get; set; }
    }
}
