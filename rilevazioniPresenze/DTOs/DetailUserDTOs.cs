namespace rilevazioniPresenze.DTOs
{
    public class DetailUserDTOs
    {
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
        public string? Mail { get; set; }
        public string? Username { get; set; }
    }
}
