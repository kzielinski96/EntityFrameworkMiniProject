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
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
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
    }
}
