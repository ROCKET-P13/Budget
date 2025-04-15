using Microsoft.EntityFrameworkCore;
using Server.Models;

namespace Server.Data;

public class AppDatabaseContext(DbContextOptions<AppDatabaseContext> options): DbContext(options)
{
	public DbSet<Budget> Budgets { get; set; }
	public DbSet<Category> Categories { get; set; }
	public DbSet<Transaction> Transactions { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Budget>()
			.HasMany(b => b.Categories)
			.WithOne(c => c.Budget)
			.HasForeignKey(c => c.BudgetId)
			.OnDelete(DeleteBehavior.Cascade);

		modelBuilder.Entity<Category>()
			.HasMany(c => c.Transactions)
			.WithOne(t => t.Category)
			.HasForeignKey(t => t.CategoryId)
			.OnDelete(DeleteBehavior.Cascade);

		modelBuilder.Entity<Transaction>()
			.HasIndex(t => t.Date);

		modelBuilder.Entity<Category>()
			.HasIndex(c => c.Name);
	}  
}
