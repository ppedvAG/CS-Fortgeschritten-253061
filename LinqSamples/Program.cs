using Bogus.DataSets;
using Common;
using Common.Models;
using LinqSamples.Extensions;
using System.Text;

namespace LinqSamples
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int number = 4711;
            var digitSum = MathExtensions.DigitSum(number);

            // als Extionsion-Methode
            digitSum = 4711.DigitSum();

            Console.WriteLine($"Quersumme von {number} ist {digitSum}\n");

            LinqSamples(CarFactory.Generate(100));

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }

        private static void LinqSamples(IEnumerable<Car> vehicles)
        {
            Console.WriteLine("Top 10 Fahrzeuge");
            vehicles.Take(10)
                .ToList()
                .ForEach(Console.WriteLine);
            Console.WriteLine();

            var averageSpeed = vehicles.Take(10).Average(v => v.TopSpeed);
            var maxSpeed = vehicles.Take(10).Max(v => v.TopSpeed);
            var minSpeed = vehicles.Take(10).Min(v => v.TopSpeed);
            Console.WriteLine($"Durchschnittsgeschwindigkeit: {averageSpeed}km/h, Max: {maxSpeed}km/h, Min: {minSpeed}km/h\n");

            // Exception wenn Liste leer ist
            Console.WriteLine($"Erstes Auto: {vehicles.First()}");

            // Default-Wert wenn Liste leer ist
            Console.WriteLine($"Letztes Auto: {vehicles.LastOrDefault()}");

            // Wenn bei Single mehr als ein Element vorkommt fliegt eine Exception
            Console.WriteLine($"Einzelnes Auto: {vehicles.Single(x => x.Id == 5025415577531566182)}");

            Console.WriteLine("\nAlle Fahrzeuge mit einem rotem Farbton");
            vehicles.Where(v => v.Color.ToString().Contains("Red"))
                .ToList()
                .ForEach(Console.WriteLine);

            // Alternative
            var result = (from v in vehicles
                          where v.Color.ToString().Contains("Green")
                          select v).ToList();
            result.ForEach(Console.WriteLine);

            Console.WriteLine("\nAutos sortieren nach TopSpeed und Model");
            vehicles.OrderByDescending(v => v.TopSpeed)
                .ThenBy(v => v.Model)
                .Take(10)
                .ToList()
                .ForEach(Console.WriteLine);

            Console.WriteLine("\nAutos nach Treibstoffart gruppieren.");
            IEnumerable<IGrouping<string, Car>> groups = vehicles.GroupBy(v => v.Fuel);
            groups.Select(g => new { Fuel = g.Key, Count = g.Count() })
                .OrderByDescending(g => g.Count)
                .ToList()
                .ForEach(g => Console.WriteLine($"{g.Count} Autos mit {g.Fuel}"));

            Console.WriteLine("\nProjektion einer verschachtelten Aufzaehlung abflachen.");
            new Container[]
            {
                new(vehicles.Take(5).ToArray()),
                new(vehicles.Skip(5).Take(5).ToArray()),
                new(vehicles.Skip(10).Take(5).ToArray())
            }
            .SelectMany(c => c.Payload)
            .ToList()
            .ForEach(Console.WriteLine);


            static StringBuilder AppendLine(StringBuilder sb, Car car)
                => sb.AppendLine($"\tDer {car.Color} {car.Model} faehrt max. {car.TopSpeed}km/h.");

            var startValue = new StringBuilder();
            var sb = vehicles.Skip(10).Take(10).Aggregate(startValue, AppendLine);
            Console.WriteLine(sb.ToString());

            Console.WriteLine("\nAutos nach Hersteller gruppieren");
            Dictionary<string, string> dictionary = vehicles.Take(20)
                .Select(c => new { c.Brand, Vehicle = c })
                .GroupBy(v => v.Brand)
                .ToDictionary(g => g.Key, g => g.Select(v => v.Vehicle).Aggregate(new StringBuilder(), AppendLine).ToString());

            // Select wird an dieser Stelle noch nicht evaluiert, weil IEnumerable per default lazy ist
            var output = dictionary.Select(pair => {
                Console.WriteLine($"{pair.Key}: {pair.Value}");
                return pair;
            });

            // Linq Expressions werden erst bei ToList(), ToArray() usw. evaluiert
            Console.WriteLine("\n\nBis hierhin wurde noch nichts in die Console geschrieben.");
            _ = output.ToArray();
        }


        public record Container(Car[] Payload);
    }
}
