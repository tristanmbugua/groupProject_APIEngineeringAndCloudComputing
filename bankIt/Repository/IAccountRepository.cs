using System.Security.Principal;
using bankIt.Models;
namespace bankIt.Repository
{
    public interface IAccountRepository
    {
        Task<Account> GetAccountByIdAsync(int accountId);
        Task<IEnumerable<Account>> GetAllAccountsAsync();
        Task CreateAccountAsync(Account account);
        Task UpdateAccountAsync(Account account);
        Task DeleteAccountAsync(int accountId);
        Task<decimal> GetTotalInvestmentsAsync(int accountId);
        Task<IEnumerable<MonthlyChargeDto>> GetMonthlyChargesByIdAsync(int id);
    }
}
