using CommandLine;

namespace TheSharks.FluentMigration;

public class MigrationRunnerOptions
{
    [Option('c', "connectionString", Required = true, HelpText = "ConnectionString of DB to migrate")]
    public string ConnectionString { get; set; } = String.Empty;

    [Option('s', "seed", Required = false, HelpText = "Seeds data")]
    public bool Seed { get; set; }
}