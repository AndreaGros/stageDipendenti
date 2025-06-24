using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rilevazioniPresenzaData.Models
{
    public class Stamping
    {
        public int Id { get; set; }
        public required string IdMatricola { get; set; }

        public ShiftType ShiftType { get; set; }
        
        public DateTime Orario { get; set; }

        public User? User { get; set; }
    }
}
