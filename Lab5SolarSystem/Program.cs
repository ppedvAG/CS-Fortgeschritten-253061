using Lab5SolarSystem.Data;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

internal class Program
{
    private static void Main(string[] args)
    {
        Node<SolarItem> sunNode = BuildSolarSystem();

        Console.WriteLine("JSON TEST");
        var jsonSerializerOptions = new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            WriteIndented = true
        };


        string json = SaveWithJsonSerializerTest(sunNode);

        Node<SolarItem> nodeOfSun = LoadWithJsonSerilizerTest(json);

        ParentNodeBuilder(nodeOfSun);
        DisplaySolarSystem(nodeOfSun);


        Console.WriteLine("----------------------------------------------");
        //JSON 
        Console.WriteLine("JSON speichern/laden/anzeigen");

        //Speichern 
        string output = SaveWithJsonSerializer(sunNode);
        //Laden
        Node<SolarItem> newSunNode = LoadWithJsonSerilizer(output);
        //Anzeigen
        ParentNodeBuilder(newSunNode);
        DisplaySolarSystem(newSunNode);
        Console.WriteLine("----------------------------------------------");

        //Xml Formatter
        Console.WriteLine("XmlSerializer speichern/laden/anzeigen");
        SaveWithXmlSerilizer(newSunNode);
        Node<SolarItem> NewSunNode2 = LoadWithXmlSerilizer();
        ParentNodeBuilder(NewSunNode2);
        DisplaySolarSystem(NewSunNode2);

        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();

        void ParentNodeBuilder(Node<SolarItem> currentNode, Node<SolarItem> parentNode = default!)
        {
            if (parentNode == null)
            {
                foreach (Node<SolarItem> childNode in currentNode.Childrens)
                {
                    childNode.ParentNode = currentNode;
                    ParentNodeBuilder(childNode, currentNode);
                }
            }
        }

        string SaveWithJsonSerializerTest(Node<SolarItem> solarNode) 
            => JsonSerializer.Serialize(solarNode, jsonSerializerOptions);

        Node<SolarItem> LoadWithJsonSerilizerTest(string input) 
            => JsonSerializer.Deserialize<Node<SolarItem>>(input, jsonSerializerOptions) ?? throw new JsonException(input);

        string SaveWithJsonSerializer(Node<SolarItem> solarNode) 
            => JsonSerializer.Serialize(solarNode, jsonSerializerOptions);

        Node<SolarItem> LoadWithJsonSerilizer(string input) 
            => JsonSerializer.Deserialize<Node<SolarItem>>(input) ?? throw new JsonException(input);

        void SaveWithXmlSerilizer(Node<SolarItem> solarNode)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Node<SolarItem>));
            Stream stream = File.OpenWrite("Solar.xml");
            xmlSerializer.Serialize(stream, solarNode);
            stream.Close();
        }

        Node<SolarItem> LoadWithXmlSerilizer()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Node<SolarItem>));
            Stream stream = File.OpenRead("Solar.xml");
            Node<SolarItem> sunNode = (Node<SolarItem>)xmlSerializer.Deserialize(stream);
            stream.Close();

            return sunNode;
        }
    }

    private static Node<SolarItem> BuildSolarSystem()
    {
        var sunNode = CreateNode<Star>("Sonne");

        var merkurNode = CreateNode<Planet>("Merkur")
            .SetParentNode(sunNode); //Variante 1 -> ParentNode setzen

        var venusNode = CreateNode<Planet>("Venus");

        var earthNode = CreateNode<Planet>("Erde")
            .AddChildren(new Moon { Description = "Mond" });

        var marsNode = CreateNode<Planet>("Mars")
            .AddChildren(
                new Moon { Description = "Phobos" }, 
                new Moon { Description = "Deimos" }
            );

        var jupiterNode = CreateNode<Planet>("Jupiter")
            .AddChildren(
                new Moon { Description = "Europa" },
                new Moon { Description = "Io" },
                new Moon { Description = "Ganymed" },
                new Moon { Description = "Kallisto" },
                new Moon { Description = "Metis" },
                new Moon { Description = "Adrastea" },
                new Moon { Description = "Amalthea" },
                new Moon { Description = "Thebe" }
            );

        var saturnNode = CreateNode<Planet>("Saturn")
            .AddChildren(
                new Moon { Description = "Titan" },
                new Moon { Description = "Rhea" },
                new Moon { Description = "Dione" },
                new Moon { Description = "Tethys" },
                new Moon { Description = "Japetus" },
                new Moon { Description = "Telesto" },
                new Moon { Description = "Calypso" },
                new Moon { Description = "Tethys" }
            );

        var uranusNode = CreateNode<Planet>("Uranus")
            .AddChildren(
                new Moon { Description = "Miranda" },
                new Moon { Description = "Ariel" },
                new Moon { Description = "Umbriel" },
                new Moon { Description = "Titania" },
                new Moon { Description = "Oberon" },
                new Moon { Description = "Triton" }
            );

        var neptunNode = CreateNode<Planet>("Neptun")
            .AddChildren(
                new Moon { Description = "Triton" },
                new Moon { Description = "Proteus" },
                new Moon { Description = "Halimede" },
                new Moon { Description = "Nereid" },
                new Moon { Description = "Naiad" },
                new Moon { Description = "Thalasaa" }
            );

        sunNode.AddChildren(
            merkurNode,
            venusNode,
            earthNode,
            marsNode,
            jupiterNode,
            saturnNode,
            uranusNode,
            neptunNode
        );
        return sunNode;
    }

    static Node<SolarItem> CreateNode<T>(string description, Node<SolarItem>? parentNode = null)
        where T : SolarItem, new()
    {
        var item = new T() { Description = description };
        return new Node<SolarItem>(item, parentNode);
    }

    private static void DisplaySolarSystem(Node<SolarItem> solarNode)
    {
        if (solarNode.Item.Type == SolarItemType.Star)
            Console.WriteLine($"Sonne: {solarNode.Item.Description}");

        if (solarNode.ParentNode != null)
        {
            if (solarNode.Item.Type == SolarItemType.Planet)
                Console.WriteLine($"\tPlanet: {solarNode.Item.Description} - kreist um {solarNode.ParentNode.Item.Description}");

            if (solarNode.Item.Type == SolarItemType.Trabant)
                Console.WriteLine($"\t\t -Mond: {solarNode.Item.Description} - kreist um {solarNode.ParentNode.Item.Description}");
        }

        if (solarNode.Item.Type != SolarItemType.Trabant)
        {
            foreach (Node<SolarItem> node in solarNode.Childrens)
            {
                DisplaySolarSystem(node);
            }
        }
    }
}