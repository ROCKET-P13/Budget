using Budget.Data;
using Microsoft.EntityFrameworkCore;
using Budget.Repositories.BudgetRepository;
using Budget.Data.Interfaces;
using Budget.Repositories.BudgetRepository.Interfaces;
using Budget.Factories.BudgetFactory.Interfaces;
using Budget.Factories.BudgetFactory;
using System.Text.Json;
using Budget.Factories.BudgetViewModelFactory.Interfaces;
using Budget.Factories.BudgetViewModelFactory;
using Budget.Repositories.CategoryRepository.Interfaces;
using Budget.Repositories.CategoryRepository;
using Budget.Factories.CategoryFactory.Interfaces;
using Budget.Factories.CategoryFactory;
using Budget.Finders.CategoryFinder.Interfaces;
using Budget.Finders.CategoryFinder;
using Budget.Finders.BudgetFinder.Interfaces;
using Budget.Finders.BudgetFinder;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDatabaseContext>(options =>
	options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<IEventStore, EventStore>();

builder.Services.AddScoped<IBudgetRepository, BudgetRepository>();
builder.Services.AddScoped<IBudgetFactory, BudgetFactory>();
builder.Services.AddScoped<IBudgetViewModelFactory, BudgetViewModelFactory>();

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryFactory, CategoryFactory>();

builder.Services.AddScoped<ICategoryFinder, CategoryFinder>();
builder.Services.AddScoped<IBudgetFinder, BudgetFinder>();

builder.Services.AddControllers()
	.AddJsonOptions(options => 
	{
		options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
	});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAll", policy =>
	policy.AllowAnyOrigin()
		.AllowAnyHeader()
		.AllowAnyMethod());
});

var app = builder.Build();

app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();


app.Run();