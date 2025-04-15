using Server.Data;
using Microsoft.EntityFrameworkCore;
using Server.Repositories.BudgetRepository.Interfaces;
using Server.Repositories.BudgetRepository;
using Server.Repositories.CategoryRepository.Interfaces;
using Server.Repositories.CategoryRepository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDatabaseContext>(options =>
	options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IBudgetRepository, BudgetRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

builder.Services.AddControllers();
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