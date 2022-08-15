namespace Application.Common.Abstractions
{
    public interface ICurrentUserService
    {
        Guid? user_id { get; set; }
    }
}
