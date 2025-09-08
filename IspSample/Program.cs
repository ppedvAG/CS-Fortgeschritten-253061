using IspSample.Contracts;
using IspSample.Models;

namespace IspSample;

internal class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Console.WriteLine("Creature Sample\n");

        var bunny = new Animal("Bunny 🐰") 
        {
            FavoriteFood = "Carrot 🥕🥕"
        };

        // Wir wollen mit Abstraktionen arbeiten (Interfaces oder sog. Contracts),
        // weil wir nicht zwischen Creature und Person unterscheiden wollen
        EatSomething(bunny);

        var person = new Human("Max Mustermann")
        {
            FavoriteFood = "Pizza 🍕🍕"
        };

        EatSomething(person);

        DoWork(person);

        DoBetterWork(person);

        var duck = CreateCreature<Animal>("Duffy 🦆🦆", "🍮🍮");
        EatSomething(duck);

        var cat = CreateNamedCreature<Animal>("Garfield 🐈🐈", "🍎🍎");

    }

    private static void EatSomething(IEats eater)
    {
        eater.Eat();
    }

    private static void DoWork(IHuman person)
    {
        person.CookFood("Eggplant 🍆🍆");
        person.Eat();
        person.Move();
    }

    // Contraints verwenden statt einem Super-Interface
    // Mit den Contraints gebe ich an, welche "Contracts" konsumiert werden sollen
    private static void DoBetterWork<T>(T person) 
        where T : IChef, IEats, IMoveable
    {
        person.CookFood("Eggplant 🍆🍆");
        person.Eat();
        person.Move();
    }



    // Wir koennten auch hier CreatureBase verwenden, 
    // aber dann wuerde das Beispiel nicht mehr funktionieren
    private static T CreateCreature<T>(string name, string food)
        // Contraints fuer Referenztypen (class) und Default-Constructor
        where T : class, IEats, new()
    {
        var creature = new T();
        creature.FavoriteFood = food;
        return creature;
    }

    // Als es noch das new()-Constraint nicht gab...
    private static T CreateNamedCreature<T>(string name, string food)
        // Contraints fuer Referenztypen (class) und Default-Constructor
        where T : class, IEats, new()
    {
        // Reflection
        Type creatureType = typeof(T);

        // Vorteil: Wir koennen Parameter uebergeben
        // Jedoch ist es nicht zur Compilerzeit Typsicher
        object obj = Activator.CreateInstance(creatureType, name);

        // Bei Casts kann potentiell immer eine InvalidCastException auftreten
        var creature = (T)obj;
        creature.FavoriteFood = food;
        return creature;
    }
}
