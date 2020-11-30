using System.Collections.Generic;

namespace HamnAdministration.Models
{
    public class Parkering
    {
        public string Namn { get; set; }
        public bool IsOpened { get; set; }

        public List<ParkeringsPlats> Platser { get; set; }
    }
}
