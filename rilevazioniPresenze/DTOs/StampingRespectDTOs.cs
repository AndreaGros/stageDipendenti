using rilevazioniPresenzaData.Models;

namespace rilevazioniPresenze.DTOs
{
    public class StampingRespectDTOs
    {
        public StampingDTOs[] Couple { get; set; } = new StampingDTOs[2];

        public bool Respect {  get; set; }
    }
}
