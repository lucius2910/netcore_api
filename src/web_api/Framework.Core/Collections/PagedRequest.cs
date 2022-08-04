namespace Framework.Core.Collections
{
    public class PagedRequest
    {
        public int page { get; set; } = 1;

        public int size { get; set; } = 10;

        public string? sort { get; set; }
    }
}
