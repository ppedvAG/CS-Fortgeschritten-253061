using System.Text.Json.Serialization;

namespace Lab5SolarSystem.Data
{
    [Serializable]
    public class SolarItem
    {
        [JsonPropertyName("Description")]
        public string Description { get; set; }

        [JsonPropertyName("Type")]
        public SolarItemType Type { get; set; }

        public SolarItem()
        {
        }

        public SolarItem(SolarItemType solarItemType)
        {
            Type = solarItemType;
        }
    }

    public class Star : SolarItem
    {
        public Star() : base(SolarItemType.Star)
        {            
        }
    }

    public class Planet : SolarItem
    {
        public Planet() : base(SolarItemType.Planet)
        {            
        }
    }

    public class Moon : SolarItem
    {
        public Moon() : base(SolarItemType.Trabant)
        {            
        }
    }

    public enum SolarItemType { Star, Planet, Trabant }
}
