using System;

namespace HamnAdministration.Models
{
    public class Bestall
    {
        public long Id { get; set; }
        public string Type { get; set; }
        public string Data { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UserId { get; set; }
    }
}
