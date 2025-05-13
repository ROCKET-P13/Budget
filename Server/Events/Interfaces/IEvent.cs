namespace Server.Events.Interfaces;

public interface IEvent
{
	public Guid Id {get; }
	public DateTime Timestamp { get; }
}