using DesignPatterns.FactoryMethod;

namespace DesignPatterns.Decorator;

public abstract class PizzaDecoratorWithCastOverload
{
    private readonly Pizza _pizza;

    public PizzaDecoratorWithCastOverload(Pizza pizza)
    {
        _pizza = pizza;
    }

    // Cast Overload
    public static implicit operator Pizza(PizzaDecoratorWithCastOverload decorator) 
        => decorator._pizza;
}

public class ExtraCheeseDecorator : PizzaDecoratorWithCastOverload
{
    public ExtraCheeseDecorator(Pizza pizza) : base(pizza)
    {
        pizza.Toppings.AddRange([PizzaToppings.Cheese, PizzaToppings.Cheese, PizzaToppings.Cheese]);
    }
}