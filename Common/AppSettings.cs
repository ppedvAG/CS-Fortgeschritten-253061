namespace Common;

public class AppSettings
{
    public string ConnectionString { get; set; } = "Random Database Connection String";

    public AppSettings()
    {
        ConnectionString = $"{GetHashCode().ToString().Substring(0, 6)} AppSettings";
    }
}