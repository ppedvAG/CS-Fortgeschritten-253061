using IspSample.Contracts;

namespace IspSample.Models;

public abstract class CreatureBase : IEats, IMoveable
{
    public string Name { get; set; }

    public string FavoriteFood { get; set; }

    public CreatureBase(string name)
    {
        Name = name;
    }

    public void Eat()
    {
        Console.WriteLine($"{Name} is eating {FavoriteFood}");
    }

    public void Sleep()
    {
        Console.WriteLine($"{Name} is sleeping");
    }

    public void Move()
    {
        Console.WriteLine($"{Name} is moving");
    }
}
