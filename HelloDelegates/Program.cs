



namespace HelloDelegates
{
    internal class Program
    {
        // Function Deklaration fuer einen Delegate Type, den wir spaeter benutzen werden
        public delegate void Hello(string name);

        // Ueberladene Function Deklaration sind nicht erlaubt
        //public delegate void Hello(string name, int age);

        static void Main(string[] args)
        {
            HelloDelegates();

            Console.WriteLine("\n\nActions und Funcs");
            ActionSamples();


            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }

        #region Delegates
        private static void HelloDelegates()
        {
            var hello = new Hello(HalloDeutschland);

            hello("Max"); // Ausfuehrung des Delegates
            Console.WriteLine();

            // Mit += koennen wir weitere Delegates hinzufuegen
            hello += HalloDeutschland;
            hello("Denis");
            Console.WriteLine();

            hello += HelloAmerican;
            hello += HelloAmerican;
            hello("John");
            Console.WriteLine();

            hello -= HelloAmerican;
            hello -= HelloAmerican;
            hello -= HalloDeutschland;
            hello!("Tim"); // Mit ! sagen wir dem Compiler, dass hello garantiert nicht null ist

            hello -= HalloDeutschland;
            //hello("nobody"); // NullReferenceException weil hello null ist

            if (hello is not null)
            {
                hello("nobody");
            }

            hello?.Invoke("Alternativ");
        }

        private static void HelloAmerican(string name)
        {
            Console.WriteLine($"Howdy, my name is {name}!");
        }

        private static void HalloDeutschland(string name)
        {
            Console.WriteLine($"Hallo, mein Name ist {name}!");
        }
        #endregion

        private static void ActionSamples()
        {
            var printNumberAction = new Action<int, int>(PrintRandomNumber); // Action ist ein Delegate>
        
            printNumberAction(10, 1);
            // Besser mit null check
            printNumberAction?.Invoke(10, 1);

            var addNumberFunc = new Func<int, int, int>((x, y) => x + y);
            int result = addNumberFunc(12, 24);
            Console.WriteLine($"12 + 24 = {result}");

            // Local Functions
            bool IsEvenPredicate(int number) => number % 2 == 0;

            var numbers = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var firstEvenNumber = numbers.ToList().Find(IsEvenPredicate);
            Console.WriteLine($"First Even Number: {firstEvenNumber}");

        }

        private static void PrintRandomNumber(int max, int min)
        {
            Console.WriteLine($"Random Number: {new Random().Next(min, max)}");
        }
    }
}
