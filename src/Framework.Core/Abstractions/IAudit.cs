namespace Framework.Core.Abstractions
{
    public interface IAudit
    {
        DateTime created_date { get; set; }
        Guid created_by { get; set; }
        DateTime? updated_date { get; set; }
        Guid? updated_by { get; set; }
    }
}
