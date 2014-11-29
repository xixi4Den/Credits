namespace AvDB_lab4.Entities.Clients
{
    public class JuridicalPerson : BaseClient
    {
        public string Name { get; set; }
        public string LegalAddress { get; set; }
        public string Director { get; set; }
        public string AccountantGeneral { get; set; }
    }
}