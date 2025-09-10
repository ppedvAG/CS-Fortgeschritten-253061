using DesignPatterns.FactoryMethod;

namespace DesignPatterns.BuilderPattern;

public class CustomPizza : Pizza
{
    public CustomPizza(IEnumerable<PizzaToppings> toppings) : base("Custom Pizza")
    {
        Toppings.Add(PizzaToppings.TomatoSauce);
        Toppings.AddRange(toppings);
    }
}