namespace ReportesCabildoAwa.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = null;
        public DateTime OrderDate { get; set; }
    }
}
