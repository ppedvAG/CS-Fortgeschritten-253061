namespace DesignPatterns.FactoryMethod
{
    public class Salami : Pizza
    {
        public Salami() : base("Salami")
        {
            Toppings.Add(PizzaToppings.TomatoSauce);
            Toppings.Add(PizzaToppings.Salami);
            Toppings.Add(PizzaToppings.Cheese);
        }
    }
}
