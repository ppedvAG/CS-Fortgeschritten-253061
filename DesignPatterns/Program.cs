using DesignPatterns.Adapter;
using DesignPatterns.BuilderPattern;
using DesignPatterns.Decorator;
using DesignPatterns.FactoryMethod;
using DesignPatterns.Strategy;

namespace DesignPatterns;

internal class Program
{
    static void Main(string[] args)
    {
        // Sonderzeichen darstellen
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        // Creational Patterns
        Console.WriteLine("Factory Pattern:\nStandard-🍕 erzeugen");
        var joeys = new PizzaShop();
        var margherita = joeys.CreateMargherita();
        Console.WriteLine(margherita);

        Console.WriteLine("\nBuilder Pattern:\nEigene-🍕 erzeugen");
        var builder = new PizzaConfigurator();
        var myPizza = builder
            .AddCheese()
            .AddPepperoni()
            .Build();
        Console.WriteLine(myPizza);

        Console.WriteLine("\nDecorator Pattern:\n🍕 wird dekoriert");
        var boxedPizza = new BoxDecorator(new ExtraCheeseDecorator(margherita));
        Console.WriteLine(boxedPizza);

        // Behavioral Patterns
        Console.WriteLine("\nStrategy Pattern:\n🍕 zustellen mit geeignetem Fahrzeug");
        var service = new DeliveryService(boxedPizza);
        service.OrderDelivery(3456);
        service.DeliverPizza();

        Console.WriteLine("\nAdapter Pattern:\n🍕 in 🥘 backen");
        var panPizza = new PanPizzaAdapter(new PanPizza());
        Console.WriteLine(panPizza);

        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}
