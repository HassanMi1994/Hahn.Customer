namespace Hahn.Customers.Domain
{
    public class PaginationResponse<T>
    {
        public int TotalSize { get; set; }
        public T Data { get; set; }
    }
}
