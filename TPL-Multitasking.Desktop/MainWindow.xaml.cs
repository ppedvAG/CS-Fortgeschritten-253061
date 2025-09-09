using System.Net.Http;
using System.Windows;
using System.Windows.Controls;

namespace TPL_Multitasking.Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Start(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(50);
                progressBar.Value = i;
                Output.Text += $"{i}\n";
            }
        }

        private void StartTaskRun(object sender, RoutedEventArgs e)
        {
            Task.Run(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    Thread.Sleep(50);

                    // UI Updates duerfen nicht von Side Thread/Tasks ausgefuehrt werden
                    // Dispatcher ist ein Property auf jedem WPF Element und ermoeglicht
                    // den Zugriff auf den UI Thread
                    Dispatcher.Invoke(() =>
                    {
                        progressBar.Value = i;
                        Output.Text += $"{i}\n";
                    });
                }
            });
        }

        private readonly CancellationTokenSource _cancellationTokenSource = new();

        private async void StartAsyncAwait(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn)
            {
                btn.IsEnabled = false;

                try
                {
                    for (int i = 0; i < 100; i++)
                    {
                        await Task.Delay(50, _cancellationTokenSource.Token);
                        progressBar.Value = i;
                        Output.Text += $"{i}\n";
                    }
                }
                catch (TaskCanceledException)
                {
                    Output.Text += "Task wurde abgebrochen\n";
                }
                finally
                {

                    btn.IsEnabled = true;
                }
            }
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            _cancellationTokenSource.Cancel();
        }

        const string RequestUrl = "http://www.gutenberg.org/files/54700/54700-0.txt";

        private async void SendRequest(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn)
            {
                btn.IsEnabled = false;

                try
                {
                    using (HttpClient client = new())
                    {
                        Output.Text = "Request gestartet\n";

                        var response = await client.GetAsync(RequestUrl);
                        if (response.IsSuccessStatusCode)
                        {
                            Output.Text += "Request erfolgreich\n";
                            var content = await response.Content.ReadAsStringAsync();
                            Output.Text += content;
                        } 
                        else 
                        { 
                            Output.Text += "Request fehlgeschlagen\n";
                            Output.Text += response.ReasonPhrase;
                        }
                    }
                }
                catch (TaskCanceledException)
                {
                    Output.Text += "Task wurde abgebrochen\n";
                }
                finally
                {

                    btn.IsEnabled = true;
                }
            }

        }

        #region Mit Legacy Code umgehen

        /// <summary>
        /// Simuliert Legacy Methode welche sehr langsam ist und wir nicht anfassen duerfen
        /// weil keine Tests existieren und beim Refactoring kaputt gehen kann.
        /// </summary>
        /// <returns></returns>
        public double CalcValuesVerySlowly(double input)
        {
            Thread.Sleep(5000);
            return 42 * input;
        }

        // Schritt 1: Async Variante machen
        public Task<double> CalcValuesVerySlowlyAsync(double input)
            => Task.Factory.StartNew(o => CalcValuesVerySlowly((double)o), input);

        #endregion

        private async void StartLegacyCode(object sender, RoutedEventArgs e)
        {
            try
            {
                Output.Text += "Anfrage gestartet fuer 42.0\n";

                var result = await CalcValuesVerySlowlyAsync(42.0);

                Output.Text += $"Ergebnis: {result}\n";

            }
            catch (Exception ex)
            {
                Output.Text += ex.Message;
            }
        }
    }
}