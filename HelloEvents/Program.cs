namespace HelloEvents
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var course = new Course();
            course.Started += (sender, e) => Console.WriteLine("Kurs gestartet");
            course.Finished += (sender, e) => Console.WriteLine($"Kurs fertig: {e.Contents}");

            Console.WriteLine("Press any key to start the course");
            Console.ReadKey();
            course.Start();
        }
    }

    public class Course
    {
        public record FinishedEventArgs(string Contents);

        public event EventHandler Started;

        public event EventHandler<FinishedEventArgs> Finished;

        public void Start()
        {
            // Wenn wir ein Event aufrufen, uebergeben wir uns selbst als "sender"
            // welches der this Kontext der Klasse ist (Best Practices)
            Started?.Invoke(this, EventArgs.Empty);

            // 2 Sekunden warten
            Thread.Sleep(2000);

            var args = new FinishedEventArgs("Kurs fertig!");
            Finished?.Invoke(this, args);
        }
    }
}
