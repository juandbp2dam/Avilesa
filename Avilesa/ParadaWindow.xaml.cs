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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Avilesa
{
    /// <summary>
    /// Lógica de interacción para LineaWindow.xaml
    /// </summary>
    public partial class ParadaWindow : Window
    {
        public List<Municipio> Municipios { get; set; }
        public int NumeroLinea { get; set; }
        public string CodMunicipioParada { get; set; }
        public TimeOnly Intervalo { get; set; }
        public AppAvilesaDBContext DBContext {get; set;}
        

        public ParadaWindow(int numeroLinea, AppAvilesaDBContext dbContext)
        {
            InitializeComponent();
            this.DBContext = dbContext;
            this.NumeroLinea = numeroLinea;
            inicializar();
            
            
        }

        private void inicializar() {
            Municipios = LogicaNegocio.LstMunicipios.OrderBy(m => m.Nombre).ToList();
            this.DataContext = this;
        }

        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Parada newParada = new Parada(NumeroLinea, CodMunicipioParada, Intervalo);
            DBContext.Paradas.Add(newParada);
            DBContext.SaveChanges();
            this.Close();
                    
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
