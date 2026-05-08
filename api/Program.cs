using Data;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Services;

var builder = WebApplication.CreateBuilder(args);

//1. Build Services

//Repositories
builder.Services.AddScoped<IAuthRepository, AuthRepository>();

//Custom services
builder.Services.AddScoped<IAuthService, AuthService>();

//-------------------//
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

//2. Build the app
var app = builder.Build();


//3. Middleware Pipelines
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();


//4. Run
app.Run();
