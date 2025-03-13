namespace PortfolioManagementSystem.Models
{
    public class Investment
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Ticker { get; set; }
        public int Shares { get; set; }
        public decimal PurchasePrice { get; set; }
        public DateTime PurchaseDate { get; set; }
    }
}
