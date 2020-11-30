namespace HamnAdministration.Meddelanden.Responses
{
    public class ParkeringInformation
    {
        public string Namn { get; set; }
        public bool IsOpened { get; set; }
        public int MaxAntalPlatser { get; set; }
        public int LedigaPlatser { get; set; }
    }
}
