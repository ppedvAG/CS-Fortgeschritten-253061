using Common;
using Common.Attributes;
using Common.Contracts;
using Common.Models;
using Common.Services;
using System.Drawing;
using System.Reflection;
using System.Text;

namespace Reflection
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Reflection arbeitet mit Typen
            // d.h. zwei Moeglichkeiten
            Car vehicle = CarFactory.Generate(1).First();
            Type carType = vehicle.GetType();

            // oder besser, weil keine potentielle NullReferenceException
            carType = typeof(Car);

            GetMembersSamples(vehicle, carType);

            AccessPrivateMembersSample(vehicle, carType);

            var assembly = AssemblySamples();

            var service = AttributeSampes(assembly);

            InvokeEventSample(service);

            Console.WriteLine("\nService anhand von einem Attribut erstellen");

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();

        }

        private static IVehicleService? AttributeSampes(Assembly? assembly)
        {
            var serviceTypes = assembly?.GetTypes()
                .Where(t => t.GetCustomAttribute<ServiceAttribute>() != null)
                .ToList();

            var serviceType = serviceTypes.Select(type =>
            {
                var attribute = type.GetCustomAttribute<ServiceAttribute>();
                Console.WriteLine($"{attribute.Order} {type.Name}: {attribute.Description}");

                return new
                {
                    Type = type,
                    attribute.Order
                };
            }).OrderBy(t => t.Order)
            .First();

            var service = Activator.CreateInstance(serviceType.Type) as IVehicleService;

            var privateFields = serviceType.Type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            privateFields[0].SetValue(service, "___infiltrated___");

            service?.ShowInfo();

            return service;
        }

        private static void InvokeEventSample(IVehicleService? service)
        {
            EventHandler<DataChangedEventArgs<Car>>? dataChangedHandler = (sender, args) => Console.WriteLine("Changed: {0}", args.NewValue);

            typeof(VehicleService).GetEvent(nameof(VehicleService.DataChanged))
                .AddEventHandler(service, dataChangedHandler);

            var updateMethod = typeof(VehicleService).GetMethod(nameof(VehicleService.Update))
                .Invoke(service, [2, new Car {
                    Brand = "Mazda",
                    Model = "Miata",
                    Type = "Coupe",
                    Color = KnownColor.AliceBlue
                }]);

            Console.WriteLine($"Update: {service.Data[2]}");
        }

        private static Assembly? AssemblySamples()
        {
            // akutelles Projekt
            _ = Assembly.GetExecutingAssembly();

            var commonAssembly = Assembly.GetAssembly(typeof(Car));

            // Alternativ DLL aus bin Verzeichnis laden
            var currentPath = Environment.CurrentDirectory;
            var commonAssemblyPath = Path.Combine(currentPath.Replace(nameof(Reflection), "Common"), "Common.dll");
            if (!File.Exists(commonAssemblyPath)) 
            { 
                throw new FileNotFoundException($"Assembly {commonAssemblyPath} not found");
            }

            // Im Debugger ausprobieren
            var commonAssemblyFromDll = Assembly.LoadFile(commonAssemblyPath);

            return commonAssembly;
        }

        private static void AccessPrivateMembersSample(Car vehicle, Type carType)
        {
            // System.ArgumentException: 'Property set method not found.' wenn kein Setter definiert ist
            carType.GetProperty("Passcode", BindingFlags.NonPublic | BindingFlags.Instance)!
                .SetValue(vehicle, null);

            // Im Debugger ausprobieren
            carType.GetField("_passcode", BindingFlags.NonPublic | BindingFlags.Instance)!
                .SetValue(vehicle, "infiltrated");
        }

        private static void GetMembersSamples(Car vehicle, Type carType)
        {
            string vehicleInfo = carType.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Aggregate(new StringBuilder(), (sb, p) => sb.AppendLine($"{p.Name,-12}: {p.GetValue(vehicle)}"))
                .ToString();
            Console.WriteLine(vehicleInfo);

            Console.WriteLine("Member-Name\tMember-Type\tTyp zur Laufzeit\tTyp zur Compilezeit");
            var memberInfo = PrintMembers(new VehicleService(), m => $"{m.Name,-16} {m.MemberType,-16} {m.ReflectedType,-16} {m.DeclaringType,-16}");
            Console.WriteLine(memberInfo);
        }

        private static string PrintMembers<T>(T obj, Func<MemberInfo, string> formatter)
        {
            return typeof(T)
                .GetMembers()
                .Aggregate(new StringBuilder(), (sb, p) => sb.AppendLine(formatter(p)))
                .ToString();
        }
    }
}
