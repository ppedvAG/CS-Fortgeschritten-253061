using Lab_SolarSystem.Data;

namespace Lab_SolarSystem;

internal class Program
{
    static void Main(string[] args)
    {

        var sunNode = CreateNode<Star>("Sonne", null);

        var merkurNode = CreateNode<Planet>("Merkur", sunNode);
        var venusNode = CreateNode<Planet>("Venus", sunNode);
        var earthNode = CreateNode<Planet>("Erde", sunNode);
        var moonNode = CreateNode<Moon>("Mond", earthNode);
        var mondUnterNode = CreateNode<Moon>("Mond", moonNode);

        var marsNode = CreateNode<Planet>("Mars", sunNode);
        var phobosNode = CreateNode<Moon>("Phobos", marsNode);
        var deimosNode = CreateNode<Moon>("Deimos", marsNode);

        var jupiterNode = CreateNode<Planet>("Jupiter", sunNode);
        var europaNode = CreateNode<Moon>("Europa", jupiterNode);
        var ioNode = CreateNode<Moon>("Io", jupiterNode);
        var ganymedNode = CreateNode<Moon>("Ganymed", jupiterNode);
        var kallistoNode = CreateNode<Moon>("Kallisto", jupiterNode);
        var metisNode = CreateNode<Moon>("Metis", jupiterNode);
        var adrasteaNode = CreateNode<Moon>("Adrastea", jupiterNode);
        var amaltheaNode = CreateNode<Moon>("Amalthea", jupiterNode);
        var thebeNode = CreateNode<Moon>("Thebe", jupiterNode);

        var saturnNode = CreateNode<Planet>("Saturn", sunNode);
        var titanNode = CreateNode<Moon>("Titan", saturnNode);
        var rheaNode = CreateNode<Moon>("Rhea", saturnNode);
        var dioneNode = CreateNode<Moon>("Dione", saturnNode);
        var tethysNode = CreateNode<Moon>("Tethys", saturnNode);
        var japetusNode = CreateNode<Moon>("Japetus", saturnNode);
        var telestoNode = CreateNode<Moon>("Telesto", saturnNode);
        var calypsoNode = CreateNode<Moon>("Calypso", saturnNode);

        var uranusNode = CreateNode<Planet>("Uranus", sunNode);
        var mirandaNode = CreateNode<Moon>("Miranda", uranusNode);
        var arielNode = CreateNode<Moon>("Ariel", uranusNode);
        var umbrielNode = CreateNode<Moon>("Umbriel", uranusNode);
        var titaniaNode = CreateNode<Moon>("Titania", uranusNode);
        var oberonNode = CreateNode<Moon>("Oberon", uranusNode);
        var tritonNode = CreateNode<Moon>("Triton", uranusNode);

        var neptunNode = CreateNode<Planet>("Neptun", sunNode);
        var proteusNode = CreateNode<Moon>("Proteus", neptunNode);
        var halimedeNode = CreateNode<Moon>("Halimede", neptunNode);
        var nereidNode = CreateNode<Moon>("Nereid", neptunNode);
        var naiadNode = CreateNode<Moon>("Naiad", neptunNode);
        var thalasaaNode = CreateNode<Moon>("Thalasaa", neptunNode);

        DisplaySolarSystemDepth(sunNode);
    }

    static Node<SolarItem> CreateNode<T>(string description, Node<SolarItem>? parentNode)
        where T : SolarItem, new()
    {
        var item = new T() { Description = description };
        return new Node<SolarItem>(item, parentNode);
    }

    static void DisplaySolarSystemDepth(Node<SolarItem> solarNode, int depth = 0)
    {
        // depth wird an die Children weitergegeben
        if (depth == 0)
            Console.WriteLine($"{solarNode.Item.GetType().Name}: {solarNode.Item.Description}");

        foreach (Node<SolarItem> node in solarNode.Childrens)
        {
            string kreistUm = node.ParentNode != null ? $" - kreist um {node.ParentNode.Item.Description}" : "";
            depth++;
            Console.WriteLine($"{new string('\t', depth)} {node.Item.GetType().Name}: {node.Item.Description}{kreistUm}");
            DisplaySolarSystemDepth(node, depth);
            depth--;
        }
    }
}
