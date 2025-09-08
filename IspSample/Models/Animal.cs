namespace IspSample.Models;

public class Animal : CreatureBase
{
    public Animal(string name) : base(name) { }

    public Animal() : base("Unknown")
    {
        
    }
}