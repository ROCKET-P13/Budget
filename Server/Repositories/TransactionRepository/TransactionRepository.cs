
using Server.Data;
using Server.Models;
using Server.Repositories.TransactionRepository.Interfaces;

namespace Server.Repositories.TransactionRepository
{
	public class TransactionRepository(AppDatabaseContext context): ITransactionRepository
	{
		private readonly AppDatabaseContext _context = context;
		public async Task<Transaction> Create(Transaction transaction)
		{
			_context.Transactions.Add(transaction);
			await _context.SaveChangesAsync();
			return transaction;
		}
	}
}