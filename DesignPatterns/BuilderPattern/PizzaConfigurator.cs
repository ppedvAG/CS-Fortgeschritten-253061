using DesignPatterns.FactoryMethod;

namespace DesignPatterns.BuilderPattern;

/// <summary>
/// Baut ein komplexes Objekt zusammen und ist per Definition Lazy,
/// d. h. erst bei Build() wird die Bussineslogik ausgeführt.
/// </summary>
internal class PizzaConfigurator
{
    private readonly List<PizzaToppings> _toppings = [];

    public PizzaConfigurator()
    {
    }

    public PizzaConfigurator AddCheese()
    {
        _toppings.Add(PizzaToppings.Cheese);
        return this;
    }

    public PizzaConfigurator AddPepperoni()
    {
        _toppings.Add(PizzaToppings.Pepperoni);
        return this;
    }

    public PizzaConfigurator AddSalami()
    {
        _toppings.Add(PizzaToppings.Salami);
        return this;
    }

    public Pizza Build()
    {
        return new CustomPizza(_toppings);
    }
}
