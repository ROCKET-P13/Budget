using Server.Events;

namespace Server.Models;

public class Category (AddedCategory @event)
{
	public Guid Id { get; } = @event.CategoryId;
	public string? Name { get; private set; } = @event.CategoryName;
	public decimal PlannedAmount { get; private set; } = @event.PlannedAmount;
	public bool? IsDebt { get; private set; } = @event.IsDebt;

	public void UpdateFrom (UpdatedCategory @event)
	{
		if (!string.IsNullOrEmpty(@event.CategoryName))
			Name = @event.CategoryName;

		if (@event.PlannedAmount != decimal.Zero)
			PlannedAmount = @event.PlannedAmount;
	}
}