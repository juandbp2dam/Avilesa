using AvilesLogic;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Avilesa
{
    /// <summary>
    /// Lógica de interacción para LineaWindow.xaml
    /// </summary>
    public partial class LineaWindow : Window
    {
        public List<Municipio> Municipios { get; set; }
        public bool EsNuevo { get; set; }
        public int[] NumLineas { get; set; }
        private int lineaConsulta;
        public int NumeroLinea { get; set; }
        public string CodMunicipioOrigen { get; set; }
        public string CodMunicipioDestino { get; set; }
        public TimeOnly HoraSalida { get; set; }
        public TimeOnly Intervalo { get; set; }
        public AppAvilesaDBContext DBContext {get; set;}
        public LineaWindow() : this(0, new AppAvilesaDBContext()){}

        public LineaWindow(int lineaCons, AppAvilesaDBContext dbContext)
        {
            InitializeComponent();
            this.DBContext = dbContext;
            this.lineaConsulta = lineaCons;
            inicializar();
            
        }

        private void inicializar() {
            Municipios = LogicaNegocio.LstMunicipios.OrderBy(m => m.Nombre).ToList();
            this.DataContext = this;
            NumeroLinea = DBContext.Lineas.Max(l => l.Numero)+1;
            llenaNumLineas();

        }


        private void llenaNumLineas()
        {
            NumLineas = new int[99];
            for (int i=1; i<100; i++)
            {
                NumLineas[i-1] = i;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (lineaConsulta == 0)
            {
                if (DBContext.Lineas.Any(l => l.Numero.Equals(NumeroLinea)))
                {
                    // Línea duplicada
                    MessageBox.Show("El número de línea seleccionado ya existe", "Línea ya existente", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                { 
                    // Creamos una nueva línea
                    Linea nuevaLinea = new Linea(NumeroLinea, CodMunicipioOrigen, CodMunicipioDestino, HoraSalida, Intervalo);
                    string mensaje = string.Empty;

                    if (nuevaLinea.ValidarLinea(out mensaje)) { 
                        DBContext.Lineas.Add(nuevaLinea); 
                        DBContext.SaveChanges();
                        this.Close(); 
                    }
                    else {
                        MessageBox.Show(mensaje, "Error de validación", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                // Consultamos / actualizamos
            }
        }
    }
}
