using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rilevazioniPresenzaData.Models
{
    public class UserShift
    {
        public required string IdMatricola { get; set; }
        public DayOfWeek Giorno { get; set; }
        public TimeOnly? T1 { get; set; }
        public TimeOnly? FT1 { get; set; }
        public TimeOnly? T2 { get; set; }
        public TimeOnly? FT2 { get; set; }

        public User? User { get; set; }
    }
}
