namespace DesignPatterns.Strategy;

public interface IVehicle
{
    string Name { get; }

    void Drive(string payload);
}

public class Car : IVehicle
{
    public string Name => "Fiat Punto 1.0";

    public void Drive(string payload)
    {
        Console.WriteLine($"{Name} faehrt {payload} aus zum Kunden.");
    }
}

public class Bike : IVehicle
{
    public string Name => "Drahtesel 0815";

    public void Drive(string payload)
    {
        Console.WriteLine($"{Name} faehrt {payload} aus zum Kunden.");
    }
}

public class Drone : IVehicle
{
    public string Name => "Mavic Air 2";

    public void Drive(string payload)
    {
        Console.WriteLine($"{Name} fliegt {payload} aus zum Kunden.");
    }
}