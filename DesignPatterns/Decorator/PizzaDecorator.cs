using DesignPatterns.FactoryMethod;

namespace DesignPatterns.Decorator;

/// <summary>
/// OCP: Open for extension, closed for modification
/// Verhalten wird zur Laufzeit erweitert
/// </summary>
public abstract class PizzaDecorator : Pizza
{
    protected readonly Pizza _pizza;

    public PizzaDecorator(Pizza pizza) : base(pizza.Name)
    {
        _pizza = pizza;
    }
}

public class BoxDecorator : PizzaDecorator
{
    public BoxDecorator(Pizza pizza) : base(pizza)
    {        
    }

    public override string Description => _pizza.Description + ", in Schachtel verpackt";
}

public class CuttingDecorator : PizzaDecorator
{
    public CuttingDecorator(Pizza pizza) : base(pizza)
    {        
    }

    public override string Description => _pizza.Description + ", geschnitten";
}
