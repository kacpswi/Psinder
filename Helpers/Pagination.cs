namespace Psinder.Helpers
{
    public class Pagination<T>
    {
        public Pagination(IEnumerable<T> data, int total)
        {
            Data = data;
            TotalCount = total;
        }
        public IEnumerable<T> Data { get; set; }
        public int TotalCount { get; set; }
    }
}
