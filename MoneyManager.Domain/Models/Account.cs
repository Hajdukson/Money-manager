namespace MoneyManager.Domain.Models
{
    public class Account
    {
        public int Id { get; set; }
        public User AccountHolder { get; set; }
        public decimal Income { get; set; }
        public decimal Outcome { get; set; }
        public double Balance { get; set; }
    }
}
