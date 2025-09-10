namespace DesignPatterns.FactoryMethod
{
    public class Pizza
    {
        public List<PizzaToppings> Toppings { get; } = new List<PizzaToppings>();
        public string Name { get; }

        public virtual string Description => $"{Name} with {string.Join(", ", Toppings)}";

        public decimal Price { get; protected set; } = 5;

        public Pizza(string name)
        {
            Name = name;
        }

        public void Prepare()
        {
            Console.WriteLine($"{Name} vorbereiten...");
        }

        public virtual void Bake()
        {
            Console.WriteLine($"{Name} in Steinofen backen...");
        }

        public virtual decimal GetCost() => Price;

        public override string ToString() => Description;
    }
}
