﻿using rilevazioniPresenzaData.Models;

namespace rilevazioniPresenze.DTOs
{
    public class StampingDTOs
    {
        public int Id { get; set; }
        public required string IdMatricola { get; set; }

        public ShiftType ShiftType { get; set; }

        public DateTime Orario { get; set; }
    }
}
