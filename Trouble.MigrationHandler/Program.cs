using Trouble.PL.Data;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddDbContextPool<TroubleEntities>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection"), builder => {
    builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
}));
var app = builder.Build();
app.Run();

