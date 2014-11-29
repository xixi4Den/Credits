namespace AvDB_lab4.Entities.Clients
{
    public abstract class BaseClient : BaseDbEntity
    {
        public ClientGroup ClientGroup { get; set; }
    }
}