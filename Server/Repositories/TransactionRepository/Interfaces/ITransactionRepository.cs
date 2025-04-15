using Server.Models;

namespace Server.Repositories.TransactionRepository.Interfaces
{
	public interface ITransactionRepository
	{
		Task<Transaction> Create(Transaction transaction);
	}
}