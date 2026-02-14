namespace HygreWebApp.Models
{
    public class PaymentDetail
    {
        public int Id { get; set; }

        // Bank details
        public string AccountName { get; set; }
        public string BankName { get; set; }
        public string AccountNumber { get; set; }
        public string IFSC { get; set; }
        public string Branch { get; set; }

        // UPI details
        public string UpiId { get; set; }
        public string AcceptedApps { get; set; }

        public DateTime UpdatedOn { get; set; } = DateTime.Now;
    }

}
