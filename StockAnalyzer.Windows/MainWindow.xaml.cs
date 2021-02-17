using StockAnalyzer.Core.Domain;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace StockAnalyzer.Windows
{
    public partial class MainWindow : Window
    {
        private static readonly string API_URL = "https://ps-async.fekberg.com/api/stocks";
        private readonly Stopwatch stopwatch = new Stopwatch();
        private readonly Random random = new Random();

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Search_Click(object sender, RoutedEventArgs e)
        {
            BeforeLoadingStockData();

            var stocks = new Dictionary<string, IEnumerable<StockPrice>>
            {
                { "MSFT", Generate("MSFT") },
                { "GOOGL", Generate("GOOGL") },
                { "PS", Generate("PS") },
                { "AMAZ", Generate("AMAZ") },
                { "ABC", Generate("ABC") },
                { "DEF", Generate("DEF") }
            };

            // step -1. This will create bad user experience
            //var msft = Calculate(stocks["MSFT"]);
            //var googl = Calculate(stocks["GOOGL"]);
            //var ps = Calculate(stocks["PS"]);
            //var amaz = Calculate(stocks["AMAZ"]);

            //Stocks.ItemsSource = new[] { msft, googl, ps, amaz };

            // step -2. Running this using thread
            //var t = new Thread(() =>
            //{
            //    var msft = Calculate(stocks["MSFT"]);
            //    var googl = Calculate(stocks["GOOGL"]);
            //    var ps = Calculate(stocks["PS"]);
            //    var amaz = Calculate(stocks["AMAZ"]);
            //    Dispatcher.Invoke(() =>
            //    {
            //        Stocks.ItemsSource = new[] { msft, googl, ps, amaz };
            //    });
            //});
            //t.Name = "My Thread";
            //t.Priority = ThreadPriority.AboveNormal;
            //t.Start();

            // step -3. Using Task.Run to run in a separate context to improve user experience.
            // This will run the code in only two threads.
            //await Task.Run(() =>
            //{
            //    var msft = Calculate(stocks["MSFT"]);
            //    var googl = Calculate(stocks["GOOGL"]);
            //    var ps = Calculate(stocks["PS"]);
            //    var amaz = Calculate(stocks["AMAZ"]);

            //    Dispatcher.Invoke(() =>
            //    {
            //        Stocks.ItemsSource = new[] { msft, googl, ps, amaz };
            //    });
            //});

            // step -4. Manually running everything as task
            //var msft = Task.Run(() => Calculate(stocks["MSFT"]));
            //var googl = Task.Run(() => Calculate(stocks["GOOGL"]));
            //var amaz = Task.Run(() => Calculate(stocks["AMAZ"]));
            //var ps = Task.Run(() => Calculate(stocks["PS"]));
            //var result = await Task.WhenAll(msft, googl, amaz, ps);
            //Stocks.ItemsSource = result;

            // step -5. Parallel programming using Parallel Invoke. This will effiectively make use of all resources.
            // all the four independent task will now run in four threads.
            //var bag = new ConcurrentBag<StockCalculation>();
            //Parallel.Invoke(() =>
            //{
            //    var msft = Calculate(stocks["MSFT"]);
            //    bag.Add(msft);
            //},
            //() =>
            //{
            //    var msft = Calculate(stocks["GOOGL"]);
            //    bag.Add(msft);
            //},
            //() =>
            //{
            //    var msft = Calculate(stocks["PS"]);
            //    bag.Add(msft);
            //},
            //() =>
            //{
            //    var msft = Calculate(stocks["AMAZ"]);
            //    bag.Add(msft);
            //});

            // setting parallel options to control parallelism
            //Parallel.Invoke(new ParallelOptions { MaxDegreeOfParallelism = 2 },
            //() =>
            //{
            //    var msft = Calculate(stocks["MSFT"]);
            //    bag.Add(msft);
            //},
            //() =>
            //{
            //    var msft = Calculate(stocks["GOOGL"]);
            //    bag.Add(msft);
            //},
            //() =>
            //{
            //    var msft = Calculate(stocks["PS"]);
            //    bag.Add(msft);
            //},
            //() =>
            //{
            //    var msft = Calculate(stocks["AMAZ"]);
            //    bag.Add(msft);
            //});

            //Stocks.ItemsSource = bag;

            // step -6. combining asynchronous programming and parallel programming
            // if we move parallel operation to a separate thread that means that thread will not be available 
            // for doing parallel operation. This is a trade off that we need to consider.
            //var bag = new ConcurrentBag<StockCalculation>();
            //await Task.Run(() =>
            //{
            //    Parallel.Invoke(() =>
            //    {
            //        var msft = Calculate(stocks["MSFT"]);
            //        bag.Add(msft);
            //    },
            //    () =>
            //    {
            //        var msft = Calculate(stocks["GOOGL"]);
            //        bag.Add(msft);
            //    },
            //    () =>
            //    {
            //        var msft = Calculate(stocks["PS"]);
            //        bag.Add(msft);
            //    },
            //    () =>
            //    {
            //        var msft = Calculate(stocks["AMAZ"]);
            //        bag.Add(msft);
            //    });
            //});

            //Stocks.ItemsSource = bag;

            // step -7. Handling exceptions
            //var bag = new ConcurrentBag<StockCalculation>();
            //try
            //{
            //    await Task.Run(() =>
            //    {
            //        try
            //        {
            //            Parallel.Invoke(() =>
            //            {
            //                var msft = Calculate(stocks["MSFT"]);
            //                bag.Add(msft);
            //                throw new Exception("MSFT");
            //            },
            //            () =>
            //            {
            //                var msft = Calculate(stocks["GOOGL"]);
            //                bag.Add(msft);
            //                throw new Exception("MSFT");
            //            },
            //            () =>
            //            {
            //                var msft = Calculate(stocks["PS"]);
            //                bag.Add(msft);
            //                throw new Exception("MSFT");
            //            },
            //            () =>
            //            {
            //                var msft = Calculate(stocks["AMAZ"]);
            //                bag.Add(msft);
            //                throw new Exception("MSFT");
            //            });
            //        }
            //        catch (Exception ex)
            //        {
            //            throw ex;
            //        }
            //    });
            //}
            //catch (Exception ex)
            //{
            //    Notes.Text = ex.Message;
            //}

            //Stocks.ItemsSource = bag;

            // step -8. processing a collection of data.
            // Using Parallel.Foreach or For to process the code so that no code change is needed for new stocks.
            //var bag = new ConcurrentBag<StockCalculation>();
            //await Task.Run(() =>
            //{
            //    Parallel.ForEach(stocks, (element) =>
            //    {
            //        var result = Calculate(element.Value);
            //        bag.Add(result);
            //    });

            //    //Parallel.For(0, stocks.Count, (i) =>
            //    //{
            //    //    var result = Calculate(stocks.ElementAt(i).Value);
            //    //    bag.Add(result);
            //    //});
            //});

            //Stocks.ItemsSource = bag;

            AfterLoadingStockData();
        }

        private IEnumerable<StockPrice> Generate(string stockIdentifier)
        {
            return Enumerable.Range(1, random.Next(10, 250))
                .Select(x => new StockPrice
                {
                    Identifier = stockIdentifier,
                    Open = random.Next(10, 1024)
                });
        }

        private StockCalculation Calculate(IEnumerable<StockPrice> prices)
        {
            #region Start stopwatch
            var calculation = new StockCalculation();
            var watch = new Stopwatch();
            watch.Start();
            #endregion

            var end = DateTime.UtcNow.AddSeconds(4);

            // Spin a loop for a few seconds to simulate load
            while (DateTime.UtcNow < end)
            { }

            #region Return a result
            calculation.Identifier = prices.First().Identifier;
            calculation.Result = prices.Average(s => s.Open);

            watch.Stop();

            calculation.TotalSeconds = watch.Elapsed.Seconds;

            return calculation;
            #endregion
        }








        private void BeforeLoadingStockData()
        {
            stopwatch.Restart();
            StockProgress.Visibility = Visibility.Visible;
            StockProgress.IsIndeterminate = true;
        }

        private void AfterLoadingStockData()
        {
            StocksStatus.Text = $"Loaded stocks for {StockIdentifier.Text} in {stopwatch.ElapsedMilliseconds}ms";
            StockProgress.Visibility = Visibility.Hidden;
        }

        private void Hyperlink_OnRequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));

            e.Handled = true;
        }

        private void Close_OnClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
