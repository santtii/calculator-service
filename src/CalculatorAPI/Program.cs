using Microsoft.AspNetCore;
using Serilog;

namespace CalculatorAPI
{
    internal class Program
    {
        [Obsolete]
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration().Enrich.FromLogContext().CreateBootstrapLogger(); // initialise the logger

            var builder = BuildWebHost(args);
            builder.Run();
        }

        [Obsolete]
        public static IWebHost BuildWebHost(string[] args)
        {
            var builder = WebHost.CreateDefaultBuilder(args)
                .UseSerilog((context, configuration) => // logger configuration
                    configuration.ReadFrom.Configuration(context.Configuration)
                    .Enrich.WithProperty("SourceContext", "") // add source context to log messages
                    .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] [{SourceContext}] {Message:lj}{NewLine}{Exception}")
                    .WriteTo.File("/var/log/calculator-.log", rollingInterval: RollingInterval.Day,
                        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] [{SourceContext}] {Message:lj}{NewLine}{Exception}"))
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>();

            return builder.Build();
        }
    }
}
