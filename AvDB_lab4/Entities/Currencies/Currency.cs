namespace AvDB_lab4.Entities.Currencies
{
    public class Currency : BaseDbEntity
    {
        public string FullName { get; set; }
        public string Abbreviation { get; set; }
        public decimal Rate { get; set; }
    }
}