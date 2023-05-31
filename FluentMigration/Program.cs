using CommandLine;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;

namespace TheSharks.FluentMigration;

public class Program
{
    static void Main(string[] args)
    {
        string connectionString = null;
        var options = new MigrationRunnerOptions();
        Parser.Default.ParseArguments<MigrationRunnerOptions>(args)
            .WithParsed(opts =>
            {
                connectionString = opts.ConnectionString;
                options = opts;
            })
            .WithNotParsed(errs => throw new ArgumentException(string.Join(Environment.NewLine, errs)));
        var serviceProvider = CreateServices(connectionString, options);


        using var scope = serviceProvider.CreateScope();
        var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
        runner.MigrateUp();
    }

    private static IServiceProvider CreateServices(string connectionString, MigrationRunnerOptions options)
    {
        return new ServiceCollection()
            .AddFluentMigratorCore()
            .ConfigureRunner(r => r
                .AddSqlServer()
                .WithGlobalConnectionString(connectionString)
                .ScanIn(typeof(Program).Assembly).For.Migrations())
            .AddLogging(l => l.AddFluentMigratorConsole())
            .AddSingleton(options)
            .BuildServiceProvider(false);
    }
}