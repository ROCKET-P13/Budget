using Server.Events.Budget;

namespace Server.Aggregates;

public class Budget
{
	public Guid Id { get; private set; }
	public string Name { get; private set; } = string.Empty;

	private class Category
	{
		public Guid Id { get; set; }
		public string? Name { get; set; }
		public decimal PlannedAmount { get; set; }
		public bool? IsDebt { get; set; }
	}

	private class Transaction
	{
		public Guid Id { get; set; }
		public Guid? CategoryId { get; set; }
		public string? Date { get; set; }
		public decimal? Amount { get; set; }
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
	private readonly List<Category> _categories = [];
	private readonly List<Transaction> _transactions = [];

	public IEnumerable<object> Categories => _categories.Select(category => new {
		category.Id,
		category.Name,
		category.PlannedAmount,
		category.IsDebt
	});

	public IEnumerable<object> Transactions => _transactions.Select(transaction => new {
		transaction.Id,
		transaction.CategoryId,
		transaction.Date,
		transaction.Amount,
		transaction.Merchant,
		transaction.Description
	});

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

	public void AddCategory(string name, decimal PlannedAmount)
	{
		var categoryAddedEvent = new AddedCategory
		{
			BudgetId = Id,
			CategoryId = Guid.NewGuid(),
			CategoryName = name,
			PlannedAmount = PlannedAmount,
		};

		Apply(categoryAddedEvent);
		AddEvent(categoryAddedEvent);
	}

	public void AddTransaction(Guid? categoryId, string? merchant, decimal amount, string? date, string? description)
	{
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

    public void AddTransaction(decimal amount, string description, string date, Guid categoryId, string merchant)
	{
		var transactionAddedEvent = new AddedTransaction
		{
			BudgetId = Id,
			Merchant = merchant,
			Amount = amount,
			Description = description,
			Date = date,
			CategoryId = categoryId,
			TransactionId = Guid.NewGuid()
		};

		Apply(transactionAddedEvent);
		AddEvent(transactionAddedEvent);
	}

	private void ApplyCreatedBudget(CreatedBudget @event)
	{
		Name = @event.BudgetName;
		Id = @event.BudgetId;
	}

	private void ApplyAddedCategory(AddedCategory @event)
	{
		_categories.Add(
			new Category
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
				Description = @event.Description
			}
		);
	}

	private void ApplyUpdatedTransaction(UpdatedTransaction @event)
	{
		var transaction = _transactions.Find(t => t.Id == @event.TransactionId);
		transaction?.UpdateFrom(@event);
	}

}