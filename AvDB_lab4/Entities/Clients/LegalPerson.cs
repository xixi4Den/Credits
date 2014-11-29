namespace AvDB_lab4.Entities.Clients
{
    public class LegalPerson : BaseClient
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CurrentAddress { get; set; }
        public string RegistrationAddress { get; set; }
        public string PassportDetails { get; set; }
        public string HomePhone { get; set; }
        public string MobilePhone { get; set; }
        public string Email { get; set; }
        public string Skype { get; set; }
        public string CompanyName { get; set; }
        public string WorkingPosition { get; set; }
    }
}