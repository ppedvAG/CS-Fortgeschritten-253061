using Common;
using Common.Contracts;
using Common.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DependencyInjection
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Abhaengigkeit von außen zu injektieren
            // DIP: Dependency Inversion Principle (https://de.wikipedia.org/wiki/Dependency-Inversion-Prinzip)
            var vehicleService = new VehicleService();

            // Die konsumierende Klasse soll nicht wissen, welche Implementierung verwendet wird
            var transportService = new TransportService(vehicleService, new AppSettings());
            transportService.ShowInfo();

            Console.WriteLine("\n==== Dependency Injection verwenden ==== ");
            ServiceProvider serviceProvider = RegisterServicesOnStartupOnce();

            // Aufloesung erfolgt bei der ersten Verwendung der Abhaengigkeit
            var transportService2 = serviceProvider.GetService<ITransportService>();
            transportService2.ShowInfo();

            var transportService3 = serviceProvider.GetService<ITransportService>();
            transportService3.ShowInfo();


            Console.WriteLine("\n==== Scoped Dependency Injection verwenden ==== ");
            using (var scope = serviceProvider.CreateScope())
            {
                var transportService4 = scope.ServiceProvider.GetService<ITransportService>();
                transportService4.ShowInfo();

                var transportService5 = scope.ServiceProvider.GetService<ITransportService>();
                transportService5.ShowInfo();
            }


            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }

        private static ServiceProvider RegisterServicesOnStartupOnce()
        {
            // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection
            var services = new ServiceCollection();

            // Best Practice: Extension Method je Assembly
            // worin die Abhaengigkeiten definiert werden
            services.AddServices();

            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider;
        }
    }
}
