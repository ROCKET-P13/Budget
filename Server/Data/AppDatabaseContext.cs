using Microsoft.EntityFrameworkCore;
using Server.Events;

namespace Server.Data;

public class AppDatabaseContext(DbContextOptions<AppDatabaseContext> options) : DbContext(options)
{
	public DbSet<Event> Events { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Event>(entity =>
		{
			entity.ToTable("Events");
			entity.Property(e => e.Id).HasColumnName("id");
			entity.Property(e => e.Type).HasColumnName("type").IsRequired();
			entity.Property(e => e.BudgetId).HasColumnName("budget_id").IsRequired();
			entity.Property(e => e.Timestamp).HasColumnName("timestamp").IsRequired();
			entity.Property(e => e.EventData).HasColumnName("event_data");
		});
	}  
}
