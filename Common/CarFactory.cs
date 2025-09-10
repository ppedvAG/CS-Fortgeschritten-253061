using Bogus;
using Common.Models;
using System.Drawing;

namespace Common;

public class CarFactory
{
    public static List<Car> Generate(int count = 20)
    {
        return new Faker<Car>()
            .UseSeed(42)
            .RuleFor(x => x.Id, f => f.Random.Long())
            .RuleFor(x => x.Brand, f => f.Vehicle.Manufacturer())
            .RuleFor(x => x.Model, f => f.Vehicle.Model())
            .RuleFor(x => x.Type, f => f.Vehicle.Type())
            .RuleFor(x => x.Fuel, f => f.Vehicle.Fuel())
            .RuleFor(x => x.TopSpeed, f => f.Random.Int(10, 30) * 10)
            .RuleFor(x => x.Color, f => f.Random.Enum(ReservedColorNamesToExclude()))
            .Generate(count);
    }

    public static KnownColor[] ReservedColorNamesToExclude()
        => Enumerable.Range(1, 27).Select(i => (KnownColor)i).ToArray();
}
