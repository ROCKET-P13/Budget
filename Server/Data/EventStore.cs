using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using Microsoft.EntityFrameworkCore;
using Server.Data.Interfaces;
using Server.Events;

namespace Server.Data;

public class EventStore: IEventStore
{
	private readonly AppDatabaseContext _dbContext;
	private readonly JsonSerializerOptions _serializerOptions;

	public EventStore(AppDatabaseContext dbContext)
	{
		_dbContext = dbContext;
		_serializerOptions = new JsonSerializerOptions
		{
			PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
			WriteIndented = false,
			TypeInfoResolver = new DefaultJsonTypeInfoResolver
			{
				Modifiers =
				{
					static typeInfo =>
					{
						if (typeInfo.Type == typeof(EventEntity))
						{
							typeInfo.PolymorphismOptions = new JsonPolymorphismOptions
							{
								TypeDiscriminatorPropertyName = "type",
								IgnoreUnrecognizedTypeDiscriminators = true,
								DerivedTypes =
								{
									new JsonDerivedType(typeof(CreatedBudget), "CreatedBudget"),
									new JsonDerivedType(typeof(AddedCategory), "AddedCategory"),
									new JsonDerivedType(typeof(UpdatedCategory), "UpdatedCategory"),	
								}
							};
						}
					}
				}
			}
		};
	}

	public async Task SaveEventsAsync (Guid budgetId, IReadOnlyCollection<EventEntity> domainEvents)
	{
		foreach (var domainEvent in domainEvents)
		{

			var eventId = Guid.NewGuid();
			var eventTimeStamp = DateTime.UtcNow;
			domainEvent.Id = eventId;
			domainEvent.Timestamp = eventTimeStamp;

			var eventJson = JsonSerializer.Serialize(domainEvent, _serializerOptions);
			var jsonDict = JsonSerializer.Deserialize<Dictionary<string, object>>(eventJson);

			var serializedEvent = JsonSerializer.Serialize(jsonDict, _serializerOptions);

			var eventEntity = new Event
			{
				Id = eventId,
				Type = domainEvent.GetType().Name,
				BudgetId = budgetId,
				Timestamp = eventTimeStamp,
				EventData = serializedEvent,
			};

			_dbContext.Events.Add(eventEntity);
		}

		await _dbContext.SaveChangesAsync();
	}

	public async Task<List<EventEntity>> GetEventsAsync (Guid budgetId)
	{
		var eventEntities = await _dbContext.Events
			.Where(e => e.BudgetId == budgetId)
			.OrderBy(e => e.Timestamp)
			.ToListAsync();

		var events = new List<EventEntity>();

		foreach (var entity in eventEntities)
		{
			var eventType = Type.GetType($"Server.Events.{entity.Type}") ?? throw new InvalidOperationException($"Cannot resolve event type: {entity.Type}");

			var deserializedEvent = JsonSerializer.Deserialize(entity.EventData, eventType, _serializerOptions) ?? throw new InvalidOperationException($"Failed to deserialize event of type{eventType}");

			var domainEvent = (EventEntity)deserializedEvent;
            events.Add(domainEvent);
        }

		return events;
    }

}