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

	public void AddCategory(string name, decimal spendingLimit)
	{
		var categoryAddedEvent = new AddedCategory
		{
			BudgetId = Id,
			CategoryId = Guid.NewGuid(),
			CategoryName = name,
			SpendingLimit = spendingLimit,
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

	public void UpdateCategory(Guid categoryId, decimal spendingLimit, string name)
	{

		var category = _categories.Find(c => c.Id == categoryId) ?? throw new Exception("Category not found");

        var UpdatedCategoryEvent = new UpdatedCategory
		{
			BudgetId = Id,
			CategoryId = categoryId,
			CategoryName = name,
			SpendingLimit = spendingLimit,
		};

		Apply(UpdatedCategoryEvent);
		AddEvent(UpdatedCategoryEvent);
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
				SpendingLimit = @event.SpendingLimit,
			}
		);
	}

	private void ApplyAddedTransaction(AddedTransaction @event)
	{
		_transactions.Add(
			new Transaction
			{
				Id = @event.TransactionId,
				Date = @event.Date,
				Amount = @event.Amount,
				Merchant = @event.Merchant,
				CategoryId = @event.CategoryId,
				Description = @event.Description,
			}
		);
	}

	private void ApplyUpdatedCategory(UpdatedCategory @event)
	{
		var category = _categories.Find(e => e.Id == @event.CategoryId);

		if (@event.CategoryName != "")
		{
			category.Name = @event.CategoryName;
		}

		if (@event.SpendingLimit != decimal.Zero)
		{
			category.SpendingLimit = @event.SpendingLimit;
		}

	}

}