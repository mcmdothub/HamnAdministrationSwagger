namespace HamnAdministration.Models
{
    public class ParkeringsPlats
    {
        public string ParkeringsNamn { get; set; }
        public int Siffra { get; set; }

        public bool IsFree { get; set; }
        public string UserId { get; set; }

        public Parkering Parkering { get; set; }
    }
}
