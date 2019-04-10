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

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            context.Table_1.Load();
            tabViewSource.Source = context.Table_1.Local;
        }

        private void AddCommandHandler(object sender, ExecutedRoutedEventArgs e)
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

            if (city_nameTextBox.Equals(""))
            {
                cur.city_name = city_nameTextBox.Text;
            }

            if (city_idTextBox.Equals(""))
            {
                cur.city_id = city_idTextBox.Text;
            }

            if (temperatureTextBox.Equals(""))
            {
                cur.temperature = double.Parse(temperatureTextBox.Text);
            }

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
    }


}
