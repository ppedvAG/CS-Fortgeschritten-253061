namespace Lab_SolarSystem.Data;

public abstract class SolarItem
{
    public string Description { get; init; }
}

public class Star : SolarItem
{
}

public class Planet : SolarItem
{
}

public class Moon : SolarItem
{
}