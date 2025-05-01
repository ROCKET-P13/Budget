using Server.Events;

namespace Server.Models;

public class Transaction (AddedTransaction @event)
{

    public Guid Id { get; } = @event.TransactionId;
	public Guid? CategoryId { get; private set; } = @event.CategoryId;
	public string? Date { get; private set; } = @event.Date;
	public decimal? Amount { get; private set; } = @event.Amount;
	public string? Merchant { get; private set; } = @event.Merchant; 
	public string? Description { get; private set; } = @event.Description;

	public void UpdateFrom (UpdatedTransaction @event)
	{
		if (@event.CategoryId != Guid.Empty)
			CategoryId = @event.CategoryId;

		if (!string.IsNullOrWhiteSpace(@event.Date))
			Date = @event.Date;

		if (@event.Amount != decimal.Zero)
			Amount = @event.Amount;

		if (!string.IsNullOrWhiteSpace(@event.Merchant))
			Merchant = @event.Merchant;

		if (!string.IsNullOrWhiteSpace(@event.Description))
			Description = @event.Description;

	}
}