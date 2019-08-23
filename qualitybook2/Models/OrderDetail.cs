namespace qualitybook2.Models
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public int BookId { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }

        public virtual Book book { get; set; }
        public virtual Order Order { get; set; }
    }
}