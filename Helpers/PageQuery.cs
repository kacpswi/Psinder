namespace Psinder.Helpers
{
    public class PageQuery
    {
        public string? SearchPhrase { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 15;
        public string? SortBy { get; set; }
        public SortDirection? SortDirection { get; set; }
    }
}
