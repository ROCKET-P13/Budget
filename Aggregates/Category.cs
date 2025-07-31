using Events.Category;

namespace Aggregates;

public class Category
{
	public Guid Id { get; private set; }
	public string Name { get; private set; } = string.Empty;
	public bool IsDebt { get; private set; } = false;
	public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

	private readonly List<CategoryEventEntity> _events = [];

	public IReadOnlyCollection<CategoryEventEntity> GetUncommittedChanges() => _events.FindAll(e => e.Id == Guid.Empty);

	public void MarkChangesAsCommitted() => _events.Clear();

	public Category(IEnumerable<CategoryEventEntity> events)
	{
		foreach (var @event in events)
		{
			AddEvent(@event);
			Apply(@event);
		}
	}

	private void Apply(CategoryEventEntity @event)
	{
		switch (@event)
		{
			case CreatedCategory e:
				ApplyCreatedCategory(e);
				break;

			case UpdatedCategoryName e:
				ApplyUpdatedCategoryName(e);
				break;

			default:
				throw new Exception("Event type not supported");
		}
	}

	public void UpdateName(string categoryName)
	{
		var updatedCategoryNameEvent = new UpdatedCategoryName
		{
			CategoryId = Id,
			Name = categoryName,
		};

		AddEvent(updatedCategoryNameEvent);
		Apply(updatedCategoryNameEvent);
	}

	private void AddEvent(CategoryEventEntity @event)
	{
		_events.Add(@event);
	}

	private void ApplyCreatedCategory(CreatedCategory @event)
	{
		Id = @event.CategoryId;
		Name = @event.CategoryName;
		IsDebt = @event.IsDebt;
		CreatedAt = @event.CreatedAt;
	}


	private void ApplyUpdatedCategoryName(UpdatedCategoryName @event)
	{
		Name = @event.Name;
	}
}