using Microsoft.EntityFrameworkCore;
using Server.DTOs.Projection;
using Server.Events.Budget;
using Server.Events.Category;

namespace Server.Data;

public class AppDatabaseContext(DbContextOptions<AppDatabaseContext> options) : DbContext(options)
{
	public DbSet<BudgetEvent> BudgetEvents { get; set; }
	public DbSet<CategoryEvent> CategoryEvents { get; set; }
	public DbSet<CategoryProjection> CategoryProjections { get; set; }
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<BudgetEvent>(entity =>
		{
			entity.ToTable("BudgetEvent");
			entity.Property(e => e.Id).HasColumnName("id");
			entity.Property(e => e.Type).HasColumnName("type").IsRequired();
			entity.Property(e => e.BudgetId).HasColumnName("budget_id").IsRequired();
			entity.Property(e => e.Timestamp).HasColumnName("timestamp").IsRequired();
			entity.Property(e => e.EventData).HasColumnName("event_data");
		});

		modelBuilder.Entity<CategoryEvent>(entity =>
		{
			entity.ToTable("CategoryEvent");
			entity.Property(e => e.Id).HasColumnName("id");
			entity.Property(e => e.Type).HasColumnName("type").IsRequired();
			entity.Property(e => e.CategoryId).HasColumnName("category_id").IsRequired();
			entity.Property(e => e.Timestamp).HasColumnName("timestamp").IsRequired();
			entity.Property(e => e.EventData).HasColumnName("event_data");
		});

		modelBuilder.Entity<CategoryProjection>(entity =>
		{
			entity.ToTable("CategoryProjection");
			entity.Property(e => e.Id).HasColumnName("id").IsRequired();
			entity.Property(e => e.Name).HasColumnName("name").IsRequired();
			entity.Property(e => e.CreatedAt).HasColumnName("created_at");
		});
	}
}
