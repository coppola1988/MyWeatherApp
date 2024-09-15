using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net.Http;
using System.Text;
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
using Newtonsoft.Json;

namespace MyWeatherApp
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string apiKey = "48541d820dbe6bf80ebc1c1bda9049bb";

        private string requestUrl = "https://api.openweathermap.org/data/2.5/weather";

        public MainWindow()
        {
            InitializeComponent();
            UpdadeData("Berlin");

            

        }

        public void UpdadeData(string city)
        {
            WeatherMapResponse result = GetWeatherData(city);

            string finalImage = "sun.png";
            string currentWeather = result.weather[0].main.ToLower();

            if (currentWeather.Contains("cloud"))
            {
                finalImage = "cloud.png";
            }
            else if (currentWeather.Contains("rain"))
            {
                finalImage = "rain.png";
            }
            else if (currentWeather.Contains("snow"))
            {
                finalImage = "snow.png";
            }


            backgroundImage.ImageSource = new BitmapImage(new Uri("Images/" + finalImage, UriKind.Relative));
            LabelTemperature.Content = result.main.temp.ToString("F1") + "°C";
            LabelInfo.Content = result.weather[0].main;
        }

        public WeatherMapResponse GetWeatherData(string city)
        {
            HttpClient httpClient = new HttpClient();
            var finalUri = requestUrl + "?q=" + city + "&appid=" + apiKey + "&units=metric";
            HttpResponseMessage httpResponse = httpClient.GetAsync(finalUri).Result;
            string response = httpResponse.Content.ReadAsStringAsync().Result;
            WeatherMapResponse waetherMapResponse = JsonConvert.DeserializeObject<WeatherMapResponse>(response);

            return waetherMapResponse;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string query = textBoxQuery.Text;
            UpdadeData(query);
        }
       /* Made by Daniele Coppola*/
    }
}
