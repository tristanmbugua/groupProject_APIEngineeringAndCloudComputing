using bankIt.Models;

namespace bankIt.Repository
{
    public interface ITransactionRepository
    {
        Task<bool> TransferAsync(TransferDto transferDto);
    }
}
