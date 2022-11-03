using CalculatorAPI.Client;
using CalculatorAPI.Client.Services;
using CalculatorAPI.Core.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

Log.Logger = new LoggerConfiguration().Enrich.FromLogContext().CreateBootstrapLogger(); // initialise the logger

var host = Host.CreateDefaultBuilder(args)
    .UseSerilog((context, configuration) => // logger configuration
        configuration.ReadFrom.Configuration(context.Configuration)
        .Enrich.WithProperty("SourceContext", "") // add source context to log messages
        .WriteTo.File("/var/log/calculator-client-.log", rollingInterval: RollingInterval.Day,
            outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] [{SourceContext}] {Message:lj}{NewLine}{Exception}"))
    .ConfigureServices(services =>
    {
        services.AddTransient<IClientService, CalculatorClient>();
        services.AddTransient<CalculatorConsole>();
    })
    .Build();

await host.Services.GetRequiredService<CalculatorConsole>()
    .ExecuteAsync();
