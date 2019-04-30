using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Data.Entity;
using System.Net;
using System.Threading;
using System.Windows.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace EF___miniproject
{
    public partial class MainWindow : Window
    {
        Entities context = new Entities();
        CollectionViewSource tabViewSource;


        public MainWindow()
        {
            InitializeComponent();
            tabViewSource = ((CollectionViewSource) (FindResource("table_1ViewSource")));
            GetApiData();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            context.Table_1.Load();
            tabViewSource.Source = context.Table_1.Local;
        }

        public void AddCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            var newRecord = new Table_1()
            {
                city_name = city_nameTextBox.Text,
                city_id = city_idTextBox.Text,
                temperature = double.Parse(temperatureTextBox.Text),
            };

                context.Table_1.Add(newRecord);
                context.SaveChanges();
                tabViewSource.View.Refresh();
        }

        private void UpdateCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            var cur = tabViewSource.View.CurrentItem as Table_1;

            cur.city_name = city_nameTextBox.Text;
            cur.city_id = city_idTextBox.Text;
            cur.temperature = double.Parse(temperatureTextBox.Text);

            context.SaveChanges();
            tabViewSource.View.Refresh();
        }

        private void DeleteCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            var cur = tabViewSource.View.CurrentItem as Table_1;

            if (cur != null)
            {
                context.Table_1.Remove(cur);
            }

            context.SaveChanges();
            tabViewSource.View.Refresh();
        }


        private async void GetApiData()
        {
            Random random = new Random();
            string apiKey = "747fe0e760b5276cc648effd30d3a2a8";
            string apiBaseUrl = "https://api.openweathermap.org/data/2.5/weather";

            await Task.Run(() =>
            {
                using (var client = new WebClient())
                {
                    while (true)
                    {
                        double lat = random.Next(35, 71);
                        double lon = random.Next(-9, 68);

                        string apiCall = apiBaseUrl + "?lat=" + lat + "&lon=" + lon + "&apikey=" + apiKey +
                                         "&mode=json&units=metric";

                        string jsonString = client.DownloadString(apiCall);
                        var jsonObject = JsonConvert.DeserializeObject<JObject>(jsonString);
                        string j_name = jsonObject["name"].ToString();
                        string j_id = jsonObject["id"].ToString();
                        string j_temp = jsonObject["main"]["temp"].ToString();

                        if (j_name == "")
                        {
                            j_name = "Ocean or See";
                        }

                        var newRecord = new Table_1()
                        {
                            city_name = j_name,
                            city_id = j_id,
                            temperature = double.Parse(j_temp),
                        };

                        Dispatcher.Invoke(() =>
                        {
                            context.Table_1.Add(newRecord);
                            context.SaveChanges();
                            tabViewSource.View.Refresh();
                        });

                        Thread.Sleep(5000);
                    }
                }
            });
        }

        private void City_idTextBox_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(city_idTextBox.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter digits only!");
                city_idTextBox.Text = city_idTextBox.Text.Remove(city_idTextBox.Text.Length - 1);
            }
        }

        private void TemperatureTextBox_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(temperatureTextBox.Text, @"[^0-9]+[^.^[0-9]]?"))
            {
                MessageBox.Show("Please enter digits only!");
                temperatureTextBox.Text = temperatureTextBox.Text.Remove(temperatureTextBox.Text.Length - 1);
            }
        }

        private void Table1_DataGrid_DoubleClick(object sender, EventArgs e)
        {
            Table_1 tab = tabViewSource.View.CurrentItem as Table_1;
            city_nameTextBox.Text = tab.city_name;
            city_idTextBox.Text = tab.city_id;
            temperatureTextBox.Text = tab.temperature.ToString();
        }
    }
}
