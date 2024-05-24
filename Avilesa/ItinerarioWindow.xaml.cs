using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AvilesLogic;
using CsvHelper;
using Microsoft.EntityFrameworkCore.Diagnostics;
namespace Avilesa
{
    /// <summary>
    /// Interaction logic for ItinerarioWindow.xaml
    /// </summary>
    public partial class ItinerarioWindow : Window
    {
        private AppAvilesaDBContext dbContext;
        public ObservableCollection<Parada> Paradas { get; set; }
        private int numeroLinea;
        private Linea linea;

        public ItinerarioWindow(int numLinea, AppAvilesaDBContext dbContext)
        {
            InitializeComponent();
            this.DataContext = this;
            this.dbContext = dbContext;
            this.numeroLinea = numLinea;
            this.linea = dbContext.Lineas.FirstOrDefault(l => l.Numero.Equals(numLinea));
            Paradas = new ObservableCollection<Parada>();
            fillDataGrid();
        }

        private void fillDataGrid() {
            Paradas.Clear();
            dbContext.Paradas
                .Where(p => p.NumLinea == numeroLinea)
                .OrderBy(p => p.Intervalo)
                .ToList()
                .ForEach(p => Paradas.Add(p));
        }
        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            ParadaWindow paradaW = new ParadaWindow(linea, dbContext);
            paradaW.ShowDialog();
            fillDataGrid();
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (dgParadas.SelectedIndex >= 0)
            {
                if (MessageBox.Show("¿Desea borrar la parada seleccionada?", "Borrado de parada", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    dbContext.Paradas.Remove(dgParadas.SelectedItem as Parada);
                    dbContext.SaveChanges();
                    fillDataGrid();
                }
            }
        }

        private void btnConsulta_Click(object sender, RoutedEventArgs e)
        {
            //if (dgLineas.SelectedIndex >= 0)
            //{
            //    Linea l = dgLineas.SelectedItem as Linea;
            //    if (l != null) { 
            //        LineaWindow lineaWindow = new LineaWindow(l.Numero, context);
            //        lineaWindow.ShowDialog();
            //        this.dgLineas.Items.Refresh();
            //    }
            //}
            
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            

        }
    }
}