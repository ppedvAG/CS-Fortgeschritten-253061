using System.Drawing;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Common.Models;

public class Car
{
    [XmlIgnore]
    [JsonIgnore]
    public long Id { get; set; }

    public string Brand { get; set; }

    public string Model { get; set; }

    [XmlAttribute("VehicleType")]
    [JsonPropertyName("VehicleType")]
    public string Type { get; set; }

    public string Fuel { get; set; }

    public int TopSpeed { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public KnownColor Color { get; set; }

    public override string ToString()
    {
        return $"{Brand,-20} {Model,-20} {Type,-20} {Fuel,-20} {Color,-20} {TopSpeed} km/h max";
    }
}
