namespace rilevazioniPresenze.DTOs
{
    public class UserShiftDTOs
    {
        public required string IdMatricola { get; set; }
        public DayOfWeek Giorno { get; set; }
        public TimeOnly? T1 { get; set; }
        public TimeOnly? FT1 { get; set; }
        public TimeOnly? T2 { get; set; }
        public TimeOnly? FT2 { get; set; }
    }
}
