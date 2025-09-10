namespace DesignPatterns.FactoryMethod;

/// <summary>
/// Factory Method: Abstraktion welche lose Kopplung fördert.
/// Konsument muss nicht wissen wie ein Objekt konkret erzeugt werden muss,
/// weil Erzeugungslogik komplex sein kann. 
/// Factory Method foerdert lose Kopplung weil uns die konkrete Implementierung nicht
/// interessieren muss. Auch hohe Kohaesion weil Erzeugung von Klassen ist hier zusammen gefasst.
/// </summary>
public class PizzaShop
{
    public Pizza CreateMargherita()
    {
        var pizza = new Margherita();
        pizza.Prepare();
        pizza.Bake();
        return pizza;
    }

    public Pizza CreateFunghi()
    {
        var pizza = new Funghi();
        pizza.Prepare();
        pizza.Bake();
        return pizza;
    }

    public Pizza CreateSalamiPizza()
    {
        var pizza = new Salami();
        pizza.Prepare();
        pizza.Bake();
        return pizza;
    }

    public Pizza CreateByName(string name) => name switch
    {
        "Margherita" => CreateMargherita(),
        "Funghi" => CreateFunghi(),
        "Salami" => CreateSalamiPizza(),
        _ => throw new ArgumentException("Unknown pizza name: " + name),
    };
}