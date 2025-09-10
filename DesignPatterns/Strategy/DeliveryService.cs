using DesignPatterns.FactoryMethod;

namespace DesignPatterns.Strategy;

/// <summary>
/// Mit dem Strategy Pattern koennen wir Verhalten zur Laufzeit dynamisch beeinflussen.
/// Typischer Anwendungsfall waere ein Plugin-Pattern, wo wir die konkrete Implementierung nicht kennen.
/// </summary>
internal class DeliveryService
{
    private Pizza _pizza;

    public IVehicle DeliveryStrategy { get; set; }

    public DeliveryService(Pizza pizza)
    {
        _pizza = pizza;
    }

    public void OrderDelivery(int distanceOfTargetInMeters)
    {
        SelectDeliveryStrategy(distanceOfTargetInMeters);
    }

    private void SelectDeliveryStrategy(int distanceOfTargetInMeters)
    {
        if (distanceOfTargetInMeters < 1000)
        {
            DeliveryStrategy = new Bike();
        }
        else if (distanceOfTargetInMeters < 5000)
        {
            DeliveryStrategy = new Car();
        }
        else
        {
            DeliveryStrategy = new Drone();
        }
    }

    public void DeliverPizza()
    {
        if (DeliveryStrategy == null)
        {
            throw new ApplicationException("Keine Bestellung vorhanden. Bitte erst OrderDelivery aufrufen.");
        }

        DeliveryStrategy.Drive(_pizza.Name);
    }
}
