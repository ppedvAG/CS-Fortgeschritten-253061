using Common.Attributes;
using Common.Contracts;
using Common.Models;

namespace Common.Services
{
    [Service("Fahrzeugverwaltung", 1)]
    public class VehicleService : GenericService<Car>, IVehicleService
    {
        private readonly string? _state;

        public VehicleService() : base(CarFactory.Generate())
        {
            _state = "Top Secret";
        }

        public Car CreateCar(string modelName, string brandName)
        {
            var car = new Car
            {
                Model = modelName,
                Brand = brandName
            };

            Add(car);
            return car;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Anzahl Elemente: {Data.Count}\tStatus: {_state}");
        }
    }
}
