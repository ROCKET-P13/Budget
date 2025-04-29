using Server.Events;
using Server.Models;

namespace Server.Aggregates;

public class Budget
{
	public Guid Id { get; private set; }
	public string Name { get; private set; } = string.Empty;

	private readonly List<EventEntity> _events = [];
	private readonly List<Category> _categories = [];
	public IReadOnlyCollection<Category> Categories => _categories.AsReadOnly();

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

	public void UpdateCategory(Guid categoryId, decimal spendingLimit, string name)
	{

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

    public void AddTransaction(decimal amount, string description, DateTime date, Guid categoryId)
	{
		var transactionAddedEvent = new AddedTransaction
		{
			Amount = amount,
			Description = description,
			Date = date,
			CategoryId = categoryId,
		};

		Apply(transactionAddedEvent);
		AddEvent(transactionAddedEvent);
	}

	private void ApplyCreatedBudget(CreatedBudget @event)
	{
		Name = @event.BudgetName;
		Id = @event.BudgetId;
	}

	private void ApplyAddedCategory (AddedCategory @event)
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

	private void ApplyUpdatedCategory (UpdatedCategory @event)
	{
		var category = _categories.Find(e => e.Id == @event.CategoryId) ?? throw new Exception("Category not found");

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