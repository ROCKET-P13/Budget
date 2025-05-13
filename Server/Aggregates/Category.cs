using Server.Events.Category;

namespace Server.Aggregates;

public class Category
{
	public Guid Id { get; private set; }
	public string Name { get; private set; } = string.Empty;
	public bool IsDebt { get; private set; } = false;

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

			default:
				throw new Exception("Event type not supported");
		}
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
	}

}