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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AppAvilesaDBContext context = new AppAvilesaDBContext();

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = context;
            this.fillDataGrid();
        }

        private void fillDataGrid() {
            using (var reader = new StreamReader(CsvDatos.RutaArchivoLineas))
            using (var csv = new CsvReader(reader, CsvDatos.CsvConfig))
            {
                List<Linea> lstLineas = new List<Linea>(csv.GetRecords<Linea>().ToList());
                lstLineas.ForEach(l => context.Lineas.Add(l));
            }
        }
        private void dummy() {
            // Voy a añadir líneas al contexto para ver si graba en el csv vacío
            context.Lineas.Add(new Linea(1, "33004", "33204", new TimeOnly(0,0), new TimeOnly(0,20)));
            context.Lineas.Add(new Linea(2, "33204", "33044", new TimeOnly(0, 0), new TimeOnly(0, 15)));
            context.SaveChanges();
        }

        private void dummy2()
        {
           
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            LineaWindow lineaWindow = new LineaWindow(0, context);

            lineaWindow.Show();
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            // Primero necesito obtener la línea seleccionada en el grid
            // Y luego la elimino de la colección
            if (dgLineas.SelectedIndex >= 0)
            {
                if (MessageBox.Show("¿Desea borrar la línea seleccionada?", "Borrado de línea", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    context.Lineas.Remove(dgLineas.SelectedItem as Linea);
                    context.SaveChanges();
                }
            }
            
        }
    }
}