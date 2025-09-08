using System.Drawing;

namespace Common.Models;

public class Car
{
    public long Id { get; set; }

    public string Brand { get; set; }

    public string Model { get; set; }

    public string Type { get; set; }

    public string Fuel { get; set; }

    public int TopSpeed { get; set; }

    public KnownColor Color { get; set; }

    public override string ToString()
    {
        return $"{Brand,-20} {Model,-20} {Type,-20} {Fuel,-20} {Color,-20} {TopSpeed} km/h max";
    }
}
