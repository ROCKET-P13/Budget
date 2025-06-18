using Server.Events.Budget;

namespace Server.Aggregates;

public class Budget
{
	public Guid Id { get; private set; }
	public string Name { get; private set; } = string.Empty;
	public int Month { get; private set; }
	public int Year { get; private set; }
	public DateTime CreatedAt { get; private set; }

	public class BudgetCategory
	{
		public Guid Id { get; set; }
		public string? Name { get; set; }
		public decimal PlannedAmount { get; set; }
		public bool? IsDebt { get; set; }
	}

	public class Transaction
	{
		public Guid Id { get; set; }
		public Guid? CategoryId { get; set; }
		public string? Date { get; set; }
		public decimal Amount { get; set; }
		public string? Merchant { get; set; }
		public string? Description { get; set; }

		public void UpdateFrom (UpdatedTransaction @event)
		{
			if (@event.CategoryId != Guid.Empty)
				CategoryId = @event.CategoryId;

			if (!string.IsNullOrWhiteSpace(@event.Date))
				Date = @event.Date;

			if (@event.Amount != decimal.Zero)
				Amount = @event.Amount;

			if (!string.IsNullOrWhiteSpace(@event.Merchant))
				Merchant = @event.Merchant;

			if (!string.IsNullOrWhiteSpace(@event.Description))
				Description = @event.Description;
		}
	}

	private readonly List<BudgetEventEntity> _events = [];
	private readonly List<BudgetCategory> _categories = [];
	private readonly List<Transaction> _transactions = [];

	public IReadOnlyCollection<BudgetCategory> Categories => _categories.AsReadOnly();

	public IReadOnlyCollection<Transaction> Transactions => _transactions.AsReadOnly();

	public Budget(IEnumerable<BudgetEventEntity> events)
	{
		foreach (var @event in events)
		{
			AddEvent(@event);
			Apply(@event);
		}
	}

	private void Apply(BudgetEventEntity @event)
	{
		switch (@event)
		{
			case CreatedBudget e:
				ApplyCreatedBudget(e);
				break;

			case AddedCategory e:
				ApplyAddedCategory(e);
				break;

			case AddedTransaction e:
				ApplyAddedTransaction(e);
				break;

			case UpdatedTransaction e:
				ApplyUpdatedTransaction(e);
				break;

			default:
				throw new Exception("Event type not supported");
		}
	}

	private void AddEvent(BudgetEventEntity @event)
	{
		_events.Add(@event);
	}

	public IReadOnlyCollection<BudgetEventEntity> GetUncommittedChanges() => _events.FindAll(e => e.Id == Guid.Empty);
	
	public void MarkChangesAsCommitted() => _events.Clear();

	public void AddCategory(decimal PlannedAmount, Category category)
	{
		var categoryAddedEvent = new AddedCategory
		{
			BudgetId = Id,
			CategoryId = category.Id,
			CategoryName = category.Name,
			PlannedAmount = PlannedAmount,
			IsDebt = category.IsDebt
		};

		Apply(categoryAddedEvent);
		AddEvent(categoryAddedEvent);
	}

	public void AddTransaction(Guid? categoryId, string? merchant, decimal amount, string? date, string? description)
	{
		if (!_categories.Select(c => c.Id == categoryId).Any())
		{
			throw new Exception("Invalid category");
		}
		
		var transactionAddedEvent = new AddedTransaction
		{
			TransactionId = Guid.NewGuid(),
			BudgetId = Id,
			CategoryId = categoryId,
			Amount = amount,
			Date = date,
			Description = description,
			Merchant = merchant,
		};

		Apply(transactionAddedEvent);
		AddEvent(transactionAddedEvent);
	}

	public void UpdateTransaction(Guid transactionId, decimal amount, string merchant, string description)
	{
		var transaction = _transactions.Find(t => t.Id == transactionId) ?? throw new Exception("Transaction not found");
		var updatedTransactionEvent = new UpdatedTransaction
		{
			BudgetId = Id,
			CategoryId = transaction.CategoryId,
			Amount = amount,
			Description = description,
			Merchant = merchant,
			TransactionId = transaction.Id,
		};

		Apply(updatedTransactionEvent);
		AddEvent(updatedTransactionEvent);
	}

	private void ApplyCreatedBudget(CreatedBudget @event)
	{
		Name = @event.BudgetName;
		Id = @event.BudgetId;
		CreatedAt = @event.CreatedAt;
		Month = @event.Month;
		Year = @event.Year;
	}

	private void ApplyAddedCategory(AddedCategory @event)
	{
		_categories.Add(
			new BudgetCategory
			{
				Id = @event.CategoryId,
				Name = @event.CategoryName,
				PlannedAmount = @event.PlannedAmount,
				IsDebt = @event.IsDebt
			}
		);
	}

	private void ApplyAddedTransaction(AddedTransaction @event)
	{
		_transactions.Add(
			new Transaction
			{
				Id = @event.TransactionId,
				CategoryId = @event.CategoryId,
				Date = @event.Date,
				Amount = @event.Amount,
				Description = @event.Description,
				Merchant = @event.Merchant
			}
		);
	}

	private void ApplyUpdatedTransaction(UpdatedTransaction @event)
	{
		var transaction = _transactions.Find(t => t.Id == @event.TransactionId);
		transaction?.UpdateFrom(@event);
	}

}