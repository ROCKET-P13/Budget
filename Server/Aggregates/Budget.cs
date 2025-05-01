using Server.Events;
using Server.Models;

namespace Server.Aggregates;

public class Budget
{
	public Guid Id { get; private set; }
	public string Name { get; private set; } = string.Empty;

	private readonly List<EventEntity> _events = [];
	private readonly List<Category> _categories = [];
	private readonly List<Transaction> _transactions = [];

	public IReadOnlyCollection<Category> Categories => _categories.AsReadOnly();
	public IReadOnlyCollection<Transaction> Transactions => _transactions.AsReadOnly();

	public Budget(IEnumerable<EventEntity> events)
	{
		foreach (var @event in events)
		{
			AddEvent(@event);
			Apply(@event);
		}
	}

	private void Apply(EventEntity @event)
	{
		switch (@event)
		{
			case CreatedBudget e:
				ApplyCreatedBudget(e);
				break;

			case AddedCategory e:
				ApplyAddedCategory(e);
				break;

			case UpdatedCategory e:
				ApplyUpdatedCategory(e);
				break;

			case AddedTransaction e:
				ApplyAddedTransaction(e);
				break;

			case UpdatedTransaction e:
				break;

			default:
				throw new Exception("Event type not supported");
		}
	}

	private void AddEvent(EventEntity @event)
	{
		_events.Add(@event);
	}

	public IReadOnlyCollection<EventEntity> GetUncommittedChanges() => _events.FindAll(e => e.Id == Guid.Empty);
	
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

	public void UpdateCategory(Guid categoryId, decimal PlannedAmount, string name)
	{

		var category = _categories.Find(c => c.Id == categoryId) ?? throw new Exception("Category not found");

        var updatedCategoryEvent = new UpdatedCategory
		{
			BudgetId = Id,
			CategoryId = categoryId,
			CategoryName = name,
			PlannedAmount = PlannedAmount,
		};

		Apply(updatedCategoryEvent);
		AddEvent(updatedCategoryEvent);
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
		_categories.Add(new Category(@event));
	}

	private void ApplyAddedTransaction(AddedTransaction @event)
	{
		_transactions.Add(new Transaction(@event));
	}

	private void ApplyUpdatedCategory(UpdatedCategory @event)
	{
		var category = _categories.Find(e => e.Id == @event.CategoryId);
	}

	private void ApplyUpdatedTransaction(UpdatedTransaction @event)
	{
		var transaction = _transactions.Find(t => t.Id == @event.TransactionId);
		transaction?.UpdateFrom(@event);
	}

}