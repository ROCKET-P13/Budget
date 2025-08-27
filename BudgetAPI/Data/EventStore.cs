using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using Microsoft.EntityFrameworkCore;
using BudgetAPI.Data.Interfaces;
using BudgetAPI.Events.Budget;
using BudgetAPI.Events;
using BudgetAPI.Events.Category;

namespace BudgetAPI.Data;

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
									new JsonDerivedType(typeof(CreatedCategory), "CreatedCategory"),
									new JsonDerivedType(typeof(AddedCategory), "AddedCategory"),
									new JsonDerivedType(typeof(AddedTransaction), "AddedTransaction"),
									new JsonDerivedType(typeof(UpdatedCategoryName), "UpdatedCategoryName")
								}
							};
						}
					}
				}
			}
		};
	}

	public async Task SaveBudgetEvents(Guid budgetId, IReadOnlyCollection<EventEntity> domainEvents)
	{
		foreach (var domainEvent in domainEvents)
		{
			var eventId = Guid.NewGuid();
			var eventTimeStamp = DateTime.UtcNow;

			var eventEntity = new BudgetEvent
			{
				Id = eventId,
				Type = domainEvent.GetType().Name,
				BudgetId = budgetId,
				Timestamp = eventTimeStamp,
				EventData = SerializeDomainEvent(domainEvent, eventId, eventTimeStamp),
			};

			_dbContext.BudgetEvents.Add(eventEntity);
		}

		await _dbContext.SaveChangesAsync();
	}

	public async Task SaveCategoryEvents(Guid categoryId, IReadOnlyCollection<EventEntity> domainEvents)
	{
		foreach (var domainEvent in domainEvents)
		{
			var eventId = Guid.NewGuid();
			var eventTimeStamp = DateTime.UtcNow;

			var eventEntity = new CategoryEvent
			{
				Id = eventId,
				Type = domainEvent.GetType().Name,
				CategoryId = categoryId,
				Timestamp = eventTimeStamp,
				EventData = SerializeDomainEvent(domainEvent, eventId, eventTimeStamp),
			};

			_dbContext.CategoryEvents.Add(eventEntity);
		}

		await _dbContext.SaveChangesAsync();
	}

	private string SerializeDomainEvent(EventEntity domainEvent, Guid eventId, DateTime eventTimeStamp)
	{
			domainEvent.Id = eventId;
			domainEvent.Timestamp = eventTimeStamp;		

			var eventJson = JsonSerializer.Serialize(domainEvent, _serializerOptions);
			var jsonDict = JsonSerializer.Deserialize<Dictionary<string, object>>(eventJson);

			var serializedEvent = JsonSerializer.Serialize(jsonDict, _serializerOptions);
			return serializedEvent;
	}

	public async Task<List<BudgetEventEntity>> GetBudgetEvents(Guid budgetId)
	{
		var eventEntities = await _dbContext.BudgetEvents
			.Where(e => e.BudgetId == budgetId)
			.OrderBy(e => e.Timestamp)
			.ToListAsync();

		var events = new List<BudgetEventEntity>();

		foreach (var entity in eventEntities)
		{
			var eventType = Type.GetType($"BudgetAPI.Events.Budget.{entity.Type}") ?? throw new InvalidOperationException($"Cannot resolve event type: {entity.Type}");

			var deserializedEvent = JsonSerializer.Deserialize(entity.EventData, eventType, _serializerOptions) ?? throw new InvalidOperationException($"Failed to deserialize event of type{eventType}");

			var domainEvent = (BudgetEventEntity)deserializedEvent;
            events.Add(domainEvent);
        }

		return events;
    }

	public async Task<List<CategoryEventEntity>> GetCategoryEvents(Guid categoryId)
	{
		var eventEntities = await _dbContext.CategoryEvents
			.Where(e => e.CategoryId == categoryId)
			.OrderBy(e => e.Timestamp)
			.ToListAsync();

		var events = new List<CategoryEventEntity>();

		foreach (var entity in eventEntities)
		{
			var eventType = Type.GetType($"BudgetAPI.Events.Category.{entity.Type}") ?? throw new InvalidOperationException($"Cannot resolve event type: {entity.Type}");

			var deserializedEvent = JsonSerializer.Deserialize(entity.EventData, eventType, _serializerOptions) ?? throw new InvalidOperationException($"Failed to deserialize event of type{eventType}");

			var domainEvent = (CategoryEventEntity)deserializedEvent;
            events.Add(domainEvent);
        }

		return events;
    }
}