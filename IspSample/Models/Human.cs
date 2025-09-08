using IspSample.Contracts;

namespace IspSample.Models;

public class Human : CreatureBase, IChef, IHuman
{
    public Human(string name) : base(name)
    {
    }

    public void CookFood(string ingredients)
    {
        Console.WriteLine($"{Name} is cooking {FavoriteFood} with {ingredients}");
    }

    public void FeedPets()
    {
        Console.WriteLine($"{Name} is feeding pets");
    }
}