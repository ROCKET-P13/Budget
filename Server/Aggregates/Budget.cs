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

	public void AddCategory(string name)
	{
		var categoryId = Guid.NewGuid();
		var categoryAddedEvent = new AddedCategory
		{
			CategoryId = categoryId,
			BudgetId = Id,
			CategoryName = name,
		};

		AddEvent(categoryAddedEvent);
		Apply(categoryAddedEvent);
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

		AddEvent(transactionAddedEvent);
		Apply(transactionAddedEvent);
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
			}
		);
	}

}