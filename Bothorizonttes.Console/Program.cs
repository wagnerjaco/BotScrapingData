using Bothorizonttes.Application.Interfaces;
using Bothorizonttes.Application.Services;
using Bothorizonttes.Domain;
using Bothorizonttes.Infrastructure.Database;
using Bothorizonttes.Infrastructure.FileSystem;
using Bothorizonttes.Infrastructure.Selenium;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

var configuration = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false)
    .Build();

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console(
        outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
    .WriteTo.File(
        path: "logs/bot-.log",
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 7,
        outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
    .CreateLogger();

var services = new ServiceCollection();


var siengeSettings = configuration
    .GetSection("Sienge")
    .Get<Settings>()
    ?? throw new InvalidOperationException("Configuração 'Sienge' não encontrada.");

var pathSettings = configuration
    .GetSection("Paths")
    .Get<PathSettings>()
    ?? throw new InvalidOperationException("Configuração 'Paths' não encontrada.");

var postgresSettings = configuration
    .GetSection("Postgres")
    .Get<PostgresSettings>()
    ?? throw new InvalidOperationException("Configuração 'Postgres' não encontrada.");

services.AddSingleton(siengeSettings);
services.AddSingleton(pathSettings);
services.AddSingleton(postgresSettings);


services.AddSingleton<IBotOrchestrator, BotOrchestrator>();

services.AddSingleton<IBotAutomation, BotAutomation>();
services.AddSingleton<IFileProcessor, FileProcessor>();
services.AddSingleton<IDatabaseRestore, PgRestoreService>();

services.AddLogging(builder =>
{
    builder.ClearProviders();
    builder.AddSerilog(dispose: true);
});


var provider = services.BuildServiceProvider();

var bot = provider.GetRequiredService<IBotOrchestrator>();

await bot.ExecutarAsync();

Console.WriteLine("BOT EXECUTADO COM SUCESSO");
