namespace API.Entities
{
    public class Result<T>
    {
        public int Page { get; set; }
        public int Qtd { get; set; }
        public long Total { get; set; }
        public long TotalPages { get; set; } // Total/Qtd
        public ICollection<T> Data { get; set; }
    }
}
