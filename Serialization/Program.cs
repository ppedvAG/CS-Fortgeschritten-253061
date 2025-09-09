using Common;
using Common.Models;
using CsvHelper;
using System.Drawing;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml.Serialization;
using LegacyJson = Newtonsoft.Json;

namespace Serialization
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var cars = CarFactory.Generate(10);

            Console.WriteLine("XML Serialization");
            SerializeXml(cars);

            Console.WriteLine("JSON Serialization");
            SerializeJson(cars);

            Console.WriteLine("JSON Serialization mit Newtonsoft.Json");
            SerializeNewtonsoftJson(cars);

            Console.WriteLine("JSON manuell de-serialisieren");
            SerializeJsonManual(cars, "cars.json");

            Console.WriteLine("Mit CSV-Helper serialisieren");
            SerializeCsv(cars);

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }

        private static void SerializeCsv(List<Car> cars)
        {
            using (var sw = new StreamWriter("cars.csv"))
            {
                using (var csv = new CsvWriter(sw, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(cars);
                }
            }

            using (var sr = new StreamReader("cars.csv"))
            {
                using (var csv = new CsvReader(sr, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<Car>().ToList();
                }
            }

            foreach (var car in cars)
            {
                Console.WriteLine(car);
            }
        }

        private static void SerializeJsonManual(List<Car> elements, string filename)
        {
            var json = File.ReadAllText(filename);

            // JSON-Dokument laden
            using (var document = JsonDocument.Parse(json))
            {
                var root = document.RootElement;

                // Daten auslesen
                foreach (var item in root.EnumerateArray())
                {
                    string brand = item.GetProperty(nameof(Car.Brand)).GetString();
                    string model = item.GetProperty(nameof(Car.Model)).GetString();
                    Enum.TryParse(item.GetProperty(nameof(Car.Color)).GetString(), out KnownColor color);

                    Console.WriteLine($"Ein {color} {brand} {model}.");
                }
            }
        }

        private static void SerializeNewtonsoftJson(List<Car> cars)
        {
            var settings = new LegacyJson.JsonSerializerSettings
            {
                Formatting = LegacyJson.Formatting.Indented,
                ReferenceLoopHandling = LegacyJson.ReferenceLoopHandling.Ignore
            };
            var json = LegacyJson.JsonConvert.SerializeObject(cars, settings);
            File.WriteAllText("cars-newton.json", json);

            string fileContent = File.ReadAllText("cars-newton.json");
            var deserialized = LegacyJson.JsonConvert.DeserializeObject<List<Car>>(fileContent, settings);

            foreach (var car in deserialized)
            {
                Console.WriteLine(car);
            }
        }

        private static void SerializeJson<T>(List<T> elements)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true, // pretty print
                ReferenceHandler = ReferenceHandler.IgnoreCycles // Rekursivität vermeiden
            };

            string json = JsonSerializer.Serialize(elements, options);
            File.WriteAllText("cars.json", json);

            string fileContent = File.ReadAllText("cars.json");
            var deserialized = JsonSerializer.Deserialize<List<T>>(fileContent, options);

            foreach (var car in deserialized)
            {
                Console.WriteLine(car);
            }
        }

        private static void SerializeXml<T>(List<T> elements) where T : class
        {
            var serializer = new XmlSerializer(typeof(List<T>));

            using (var fs = new FileStream("cars.xml", FileMode.Create))
            {
                serializer.Serialize(fs, elements);

                // Wird implizit durch das using-Statement ausgeführt
                // fs.Dispose();
            }

            // Serialisierte Datei wieder einlesen
            using (var fs = new FileStream("cars.xml", FileMode.Open))
            {
                // Um as zu verwenden, muss T ein Referenztyp sein (class)
                var deserialized = serializer.Deserialize(fs) as List<T>;

                foreach (var car in deserialized)
                {
                    Console.WriteLine(car);
                }
            }
        }
    }
}
