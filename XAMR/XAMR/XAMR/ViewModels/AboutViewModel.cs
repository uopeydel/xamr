
using System;
using System.Windows.Input;
using Plugin.Share;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.SignalR.Client;

namespace XAMR.ViewModel
{
	public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";
            OpenWebCommand = new OpenWeb();
        }


        /// <summary>
        /// Command to open browser to xamarin.com
        /// </summary>
        public ICommand OpenWebCommand { get; }
    }

    class OpenWeb : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void /*async Task*/ Execute(object parameter)
        {
            CrossShare.Current.OpenBrowser("https://www.google.com");
            //await ExecuteAsync("http://192.168.20.20:1000/NotificationHub");
        }




        static HubConnection connection = null;
        private static async Task<HubConnection> ConnectAsync(string baseUrl)
        {
            // Keep trying to until we can start
            while (true)
            {
                var conn = new HubConnectionBuilder()
                                .WithUrl(baseUrl)
                                .Build();
                try
                {
                    await conn.StartAsync();
                    return conn;
                }
                catch (Exception)
                {
                    await Task.Delay(1000);
                }
            }
        }
        public static void ShowTimeMobile(string data)
        {
            var aa = data;
        }

        public static async Task<int> ExecuteAsync(string baseUrl)
        {
            if (connection == null)
            {
                connection = await ConnectAsync(baseUrl);
            }
            try
            {
                connection.On<string>("Send", ShowTimeMobile);
                await connection.InvokeAsync<object>("Send", "my1mobile");
            }
            catch (AggregateException aex) when (aex.InnerExceptions.All(e => e is OperationCanceledException))
            {
            }
            catch (OperationCanceledException)
            {
            } 
            return 0;
        }











    }
}
