using Data;
using Microsoft.EntityFrameworkCore;
using Repositories.BudgetRepository;
using Data.Interfaces;
using Repositories.BudgetRepository.Interfaces;
using Factories.BudgetFactory.Interfaces;
using Factories.BudgetFactory;
using System.Text.Json;
using Factories.BudgetViewModelFactory.Interfaces;
using Factories.BudgetViewModelFactory;
using Repositories.CategoryRepository.Interfaces;
using Repositories.CategoryRepository;
using Factories.CategoryFactory.Interfaces;
using Factories.CategoryFactory;
using Finders.CategoryFinder.Interfaces;
using Finders.CategoryFinder;
using Finders.BudgetFinder.Interfaces;
using Finders.BudgetFinder;

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