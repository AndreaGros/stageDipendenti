using System.ComponentModel.DataAnnotations;

namespace rilevazioniPresenze.DTOs;

public class GeneralUserDTOs
{
    public required string Matricola { get; set; }
    public required int Badge { get; set; }
    public required string Nominativo { get; set; }
    public string? Citta_Nascita { get; set; }
    public DateOnly? Data_Nascita { get; set; }
    public string? Indirizzo_Residenza { get; set; }
    public string? Numero_Telefono { get; set; }
    public string? Stato_Lavorativo { get; set; }
}

