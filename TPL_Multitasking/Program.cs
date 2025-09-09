
namespace TPL_Multitasking
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //CreateTaskSamples();

            //CreateTaskWaitSamples();

            //CreateTaskAndCancellationToken();

            //TaskExceptionHandlingSample();

            //TaskContinuationSamples();

            ParallelSamples();

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        private static void ParallelSamples()
        {

            Parallel.Invoke(Count, Count, Count);

            static void Count()
            {
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine($"Task Invoke #{i} [{Thread.CurrentThread.ManagedThreadId}] ist fertig.");
                }
            }


            Parallel.For(0, 10, i =>
            {
                Console.WriteLine($"Task For #{i} [{Thread.CurrentThread.ManagedThreadId}] ist fertig.");
            });

            var source = new CancellationTokenSource();
            var options = new ParallelOptions { CancellationToken = source.Token, MaxDegreeOfParallelism = 3 };

            var numberList = Enumerable.Range(0, 10).ToList();
            Parallel.ForEach(numberList, options, i => {
                if (i == 5)
                {
                    source.Cancel();
                }

                source.Token.ThrowIfCancellationRequested();

                Console.WriteLine($"Task Each #{i} [{Thread.CurrentThread.ManagedThreadId}] ist fertig.");
            });
        }

        private static void TaskContinuationSamples()
        {
            var task = new Task(() =>
            {
                Console.WriteLine("Task wurde gestartet.");
                Thread.Sleep(1000);
                Console.WriteLine("Task ist fertig.");

                if (Random.Shared.Next(0, 2) == 0)
                {
                    throw new Exception("Gerade Zahl erwischt!");
                }
            });

            task.ContinueWith(t => Console.WriteLine("Okay"),
                TaskContinuationOptions.NotOnFaulted);
            task.ContinueWith(t => Console.WriteLine("Fehlerhaft!"),
                TaskContinuationOptions.OnlyOnFaulted);
            task.ContinueWith(t => Console.WriteLine("Immer!"));

            task.Start();

            // Task mit Zwischenergebnissen

            var taskWithResult = new Task<int>(() =>
            {
                Console.WriteLine("Task wurde gestartet.");
                Thread.Sleep(1000);
                Console.WriteLine("Task ist fertig.");

                return new Random().Next(0, 100);
            });

            taskWithResult.ContinueWith(t => Console.WriteLine($"Ergebnis: {t.Result}"),
                TaskContinuationOptions.OnlyOnRanToCompletion);

            taskWithResult.Start();
        }

        private static void TaskExceptionHandlingSample()
        {
            try
            {
                var tasks = CreateTasks((i) =>
                {
                    Thread.Sleep(1000);
                    throw new ApplicationException($"Task #{i} [{Thread.CurrentThread.ManagedThreadId}] hatte ein Fehler.");
                }, 3);

                Task.WaitAll(tasks.ToArray());
            }
            catch (AggregateException aggregates)
            {
                foreach (Exception ex in aggregates.InnerExceptions)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private static void CreateTaskAndCancellationToken()
        {
            var source = new CancellationTokenSource();
            var token = source.Token;
            var task = Task.Factory.StartNew(RunWithCancellationToken, token);

            Thread.Sleep(2500);
            source.Cancel();

            static void RunWithCancellationToken(object? arg)
            {
                if (arg is CancellationToken token)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        if (token.IsCancellationRequested)
                        {
                            Console.WriteLine($"Task #{i} [{Thread.CurrentThread.ManagedThreadId}] wurde abgebrochen.");
                        }

                        token.ThrowIfCancellationRequested();
                        Thread.Sleep(1000);
                        Console.WriteLine($"Task [{Thread.CurrentThread.ManagedThreadId}] ist fertig.");
                    }
                }
            }

        }

        private static void CreateTaskWaitSamples()
        {
            var someTasks = CreateTasks((i) =>
            {
                Thread.Sleep(1000);
                Console.WriteLine($"Task #{i} [{Thread.CurrentThread.ManagedThreadId}] ist fertig.");
            });

            Console.WriteLine("Warten bis alle Tasks abgearbeitet sind");
            Task.WaitAll(someTasks.ToArray());

            someTasks = CreateTasks((i) =>
            {
                Thread.Sleep(1000);
                Console.WriteLine($"Task #{i} [{Thread.CurrentThread.ManagedThreadId}] ist fertig.");
            });

            Console.WriteLine("Warten bis mindestens ein Tasks abgearbeitet wurde");
            Task.WaitAny(someTasks.ToArray());
        }

        private static IEnumerable<Task> CreateTasks(Action<object?> action, int count = 10)
        {
            for (int i = 0; i < count; i++)
            {
                yield return Task.Factory.StartNew(action, i);
            }
        }

        private static void CreateTaskSamples()
        {
            static void GetRandomNumber100() => GetRandomNumber(100);

            static int GetRandomNumber(object? max)
            {
                Thread.Sleep(1000);

                int number = new Random().Next(0, (int)max!);
                Console.WriteLine($"Random number {number} von Thread {Thread.CurrentThread.ManagedThreadId} wurde erzeugt.");
                return number;
            }
            
            var task = new Task(GetRandomNumber100);
            task.Start();

            // ab .Net 4.0
            Task.Factory.StartNew(GetRandomNumber100);

            // ab .Net 4.5
            Task.Run(GetRandomNumber100);

            var taskWithArgs = new Task<int>(GetRandomNumber, 100);
            taskWithArgs.Start();

            var result = taskWithArgs.Result;
            Console.WriteLine($"Random number {result} von Thread {Thread.CurrentThread.ManagedThreadId} wurde erzeugt.");

            if (taskWithArgs.IsCompleted)
                Console.WriteLine($"Task ist fertig mit {result}");
        }
    }
}
