using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClientApi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            Task.Run(() => Reader());
        }
        private async Task Reader()
        {
            var client = new HttpClient();
            string Json = await client.GetStringAsync("https://localhost:5001/api/users");
            List<User> restoredUsers = JsonSerializer.Deserialize<List<User>>(Json);
            Dispatcher.Invoke(() => Ourtable.ItemsSource = restoredUsers);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddEditwindow addEditWindow = new AddEditwindow();
            if(addEditWindow.ShowDialog()==true)
            {
                User user = addEditWindow.NewUser;
                Task.Run(()=>PostRequestAsync(user));
            }
        }
        private async Task PostRequestAsync(User user)
        {
            try
            {
                WebRequest request = WebRequest.Create("https://localhost:5001/api/users");
                request.Method = "POST";
                string rest = $"name={user.Name}&age={user.Age}";
                byte[] byteArray = System.Text.Encoding.ASCII.GetBytes(rest);
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = byteArray.Length;
                using (Stream dataStream = request.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                }
                WebResponse response =await  request.GetResponseAsync();
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        MessageBox.Show(reader.ReadToEnd());
                    }
                }
                response.Close();
            }
            catch(WebException ex)
            {
                WebExceptionStatus status = ex.Status;
                if (status == WebExceptionStatus.ProtocolError)
                {
                    HttpWebResponse httpWebResponse = (HttpWebResponse)ex.Response;
                    string mes = (int)httpWebResponse.StatusCode + " " + httpWebResponse.StatusCode;
                    MessageBox.Show(mes);
                }
            }
        }
    }
}
