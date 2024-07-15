using Serilog;

Log.Logger = new LoggerConfiguration()
			.MinimumLevel.Debug()
			.WriteTo.Console()
			.WriteTo.File("logs/TPHDotNetCore.ConsoleAppLogging.txt", rollingInterval: RollingInterval.Hour)
			.CreateLogger();

Log.Fatal("Hello, world!");
Log.Error("Helloo worldd!");
Console.WriteLine("Hello, world!");

int a = 10, b = 0;
try
{
	Log.Debug("Dividing {A} by {B}", a, b);
	Console.WriteLine(a / b);
}
catch (Exception ex)
{
	Log.Error(ex, "Something went wrong");
}
finally
{
	await Log.CloseAndFlushAsync();
}

