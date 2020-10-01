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
using System.Windows.Threading;

namespace ServidorPeliculas
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PeliculasServer server = new PeliculasServer();

        private int time=0;
        private DispatcherTimer Timer;

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = server;
            dtgPeliculas.ItemsSource = server.CatalogoPeliculas.ListaPeliculas;

            Timer = new DispatcherTimer();
            Timer.Interval = new TimeSpan(0, 0, 10);
            Timer.Tick += Timer_Tick;
            Timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if(time>=0)
            {
                dtgPeliculas.ItemsSource = server.CatalogoPeliculas.ListaPeliculas;
                time++;
            }
        }
    }
}
