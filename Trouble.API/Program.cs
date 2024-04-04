using Microsoft.EntityFrameworkCore;
using Trouble.PL.Data;
using Serilog;
using Serilog.Ui.MsSqlServerProvider;
using Serilog.Ui.Web;
using System.Reflection;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

public class Program
{
    private static void Main(string[] args)
    {
        //Need to work on API
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        //string connectionString = GetSecret("WebAPIKey").Result;

        // Add Connection Information
        builder.Services.AddDbContextPool<TroubleEntities>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection"));
            //options.UseSqlServer(connectionString);
            options.UseLazyLoadingProxies();
        });

        //THIS IS FOR LOGGING PLUMBING------------------------------------
        string connection = builder.Configuration.GetConnectionString("DatabaseConnection");


        builder.Services.AddSerilogUi(options =>
        {
            options.UseSqlServer(connection, "Logs");
        });


        var configsettings = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configsettings)
            .CreateLogger();

        builder.Services
            .AddLogging(c => c.AddDebug())
            .AddLogging(c => c.AddSerilog())
            .AddLogging(c => c.AddEventLog())
            .AddLogging(c => c.AddConsole());

        var app = builder.Build();

        app.UseSerilogUi(options => { options.RoutePrefix = "logs"; });

        //---------------------------------------------------------------- Logging from plumbing

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();

        app.MapControllers();


        app.Run();
    }

    public static async Task<string> GetSecret(string secretName)
    {
        try
        {
            var keyVaultName = "kv-300077578";
            var kvUri = $"https://{keyVaultName}.vault.azure.net";

            var client = new SecretClient(new Uri(kvUri), new DefaultAzureCredential());
            //using var client = GetClient();
            var secret = await client.GetSecretAsync(secretName);
            Console.WriteLine(secret.Value.Value.ToString());
            return secret.Value.Value.ToString();
            //return (await client.GetSecretAsync(kvUri, secretName)).Value.ToString();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }
}


