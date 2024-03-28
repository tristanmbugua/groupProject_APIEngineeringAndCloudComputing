namespace bankIt.Models
{
    public class AccountDetailsDto
    {
        public int Id { get; set; }
        public string AccountType { get; set; }
        public decimal Balance { get; set; }
    }

    public class AccountCreateDto
    {
        public string AccountType { get; set; }
        public decimal InitialBalance { get; set; }

    }

    public class AccountUpdateDto
    {
        public decimal Balance { get; set; }

    }

    public class TransferDto
    {
        public int FromAccountId { get; set; }
        public int ToAccountId { get; set; }
        public decimal Amount { get; set; }
    }
    public class MonthlyChargeDto
    {
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime ChargeDate { get; set; }
    }
}

