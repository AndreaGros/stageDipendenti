using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rilevazioniPresenzaData.Models
{
    public class User
    {
        [Key]
        public required string Matricola { get; set; }
        public required int Badge { get; set; }
        public required string Nominativo { get; set; }
        public string? Sesso { get; set; }
        public string? Stato_Civile { get; set; }
        public DateOnly? Data_Nascita { get; set; }
        public string? Citta_Nascita { get; set; }
        public string? Provincia_Nascita { get; set; }
        public string? Stato_Nascita { get; set; }
        public string? Indirizzo_Residenza { get; set; }
        public string? Provincia_Residenza { get; set; }
        public string? Stato_Residenza { get; set; }
        public string? Stato_Lavorativo { get; set; }
        public string? Codice_Fiscale { get; set; }
        public string? Numero_Telefono { get; set; }
        public required string Mail { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public bool Admin { get; set; }
        public ICollection<UserShift> UserShifts { get; set; } = [];
        public ICollection<Stamping> Stampings { get; set; } = []; 
    }
}
