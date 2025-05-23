using Server.Events.Interfaces;

namespace Server.Events;

public class Event : IEvent
{
	public Guid Id { get; set; }
	public DateTime Timestamp { get; set; }
	public string Type { get; set;} = string.Empty;
	public string EventData { get; set; } = string.Empty;
}