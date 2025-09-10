using Common.Attributes;
using Common.Contracts;
using Common.Models;

namespace Common.Services
{
    public record VehicleOrder(int OrderId, string CustomerName, DateTime? DueDate);

    [Service("Spediteur", 2)]
    public class TransportService : ITransportService
    {
        private readonly IVehicleService _vehicleService;
        private readonly AppSettings _settings;
        private static int CurrentOrderId = 0;

        // Bad Practice
        // 1. Keine Abstraktion der Dependency (Interface)
        // 2. Abhängigkeit innerhalb der Klasse erzeugt
        private VehicleService _badDependency = new VehicleService();

        private List<Car> CarPool => _vehicleService.GetAll();

        public List<Car> Payload { get; set; }

        /// <summary>
        /// Constructor der IoC (Inversion of Control) veranschaulicht
        /// https://de.wikipedia.org/wiki/Inversion_of_Control
        /// und sog. Dependency Injection verwendet. D. h. unsere Klassen kennt keine 
        /// konkreten Implementierungen, sondern nur Abstraktionen die von aussen gestezt werden.
        /// </summary>
        /// <param name="vehicleService">Als abstrahierte Dependency</param>
        public TransportService(IVehicleService vehicleService, AppSettings settings)
        {
            _vehicleService = vehicleService;
            _settings = settings;
        }

        public void Load(string brandName)
        {
            Payload = CarPool.Where(c => c.Brand == brandName).ToList();
        }

        public List<Car>? Unload()
        {
            var copy = Payload.ToList();
            Payload = null;
            return copy;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"{GetHashCode().ToString().Substring(0, 6)} TransportService");
            _vehicleService.ShowInfo();
            Console.WriteLine(_settings.ConnectionString);

            if (Payload is null)
            {
                Console.WriteLine("Nichts geladen");
            }
            else
            {
                Console.WriteLine($"Geladene Autos: {Payload.Count}");
            }
        }

        public VehicleOrder CreateOrder(string customerName, int maxDays = 30)
        {
            if (!string.IsNullOrEmpty(customerName))
            {
                var dueDate = DateTime.Now.AddDays(maxDays);
                return new VehicleOrder(CurrentOrderId++, customerName, new DateTime(dueDate.Year, dueDate.Month, dueDate.Day));
            }

            return null;
        }

        public IEnumerable<Car> CreateVehicleFleet()
        {
            yield return _vehicleService.CreateCar("Toyota", "Camry");
            yield return _vehicleService.CreateCar("Ford", "Mustang");
            yield return _vehicleService.CreateCar("Honda", "Civic");
        }
    }
}
