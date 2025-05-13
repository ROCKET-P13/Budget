using Microsoft.EntityFrameworkCore;
using Server.Events.Budget;
using Server.Events.Category;

namespace Server.Data;

public class AppDatabaseContext(DbContextOptions<AppDatabaseContext> options) : DbContext(options)
{
	public DbSet<BudgetEvent> BudgetEvents { get; set; }
	public DbSet<CategoryEvent> CategoryEvents { get; set; }
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<BudgetEvent>(entity =>
		{
			entity.ToTable("BudgetEvents");
			entity.Property(e => e.Id).HasColumnName("id");
			entity.Property(e => e.Type).HasColumnName("type").IsRequired();
			entity.Property(e => e.BudgetId).HasColumnName("budget_id").IsRequired();
			entity.Property(e => e.Timestamp).HasColumnName("timestamp").IsRequired();
			entity.Property(e => e.EventData).HasColumnName("event_data");
		});

		modelBuilder.Entity<CategoryEvent>(entity =>
		{
			entity.ToTable("CategoryEvents");
			entity.Property(e => e.Id).HasColumnName("id");
			entity.Property(e => e.Type).HasColumnName("type").IsRequired();
			entity.Property(e => e.CategoryId).HasColumnName("category_id").IsRequired();
			entity.Property(e => e.Timestamp).HasColumnName("timestamp").IsRequired();
			entity.Property(e => e.EventData).HasColumnName("event_data");
		});
	}
}
