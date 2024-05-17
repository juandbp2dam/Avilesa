using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvilesLogic
{
    public class Linea
    {
        private List<Parada> itinerario = new List<Parada>();
        private string nomMunicipioOrigen = string.Empty;

        public Linea() {
            Numero = default(int);
            CodMunicipioOrigen = string.Empty;
            CodMunicipioDestino = string.Empty;
            HoraSalida = default(TimeOnly);
            Intervalo = default(TimeOnly);
        }

        public Linea(int numero, string codMunicipioOrigen, string codMunicipioDestino, TimeOnly horaSalida, TimeOnly intervalo)
        {
            Numero = numero;
            CodMunicipioOrigen = codMunicipioOrigen;
            CodMunicipioDestino = codMunicipioDestino;
            HoraSalida = horaSalida;
            Intervalo = intervalo;
        }

        [Key]
        public int Numero { get; set; }
        public string CodMunicipioOrigen {  get; set; }
        
        public string CodMunicipioDestino { get; set; }
        [Format("HH:mm")]
        public TimeOnly HoraSalida { get; set; }
        [Format("HH:mm")]
        public TimeOnly Intervalo { get; set; }

        public List<Parada> ObtenerItinerario() {
            return itinerario;
        }

        public void InsertarParada(Parada p) {
            itinerario.Add(p);
        }

        public void EliminarParada(Parada p) {
            itinerario.Remove(p);
        }
        
        public void LimpiarParadas()
        {
            itinerario.Clear();
        }

        public bool ValidarLinea(out string mensaje) 
        {
            mensaje = string.Empty;
            if (string.IsNullOrEmpty(CodMunicipioOrigen))
            {
                mensaje = "No se ha introducido un municipio origen";
                return false;
            }
            else if (string.IsNullOrEmpty(CodMunicipioDestino))
            {
                mensaje = "No se ha introducido un municipio destino";
                return false;
            }
            else if (CodMunicipioDestino.Equals(CodMunicipioOrigen))
            {
                mensaje = "La línea no puede salir y llegar al mismo lugar";
                return false;
            }
            else if (Intervalo.Minute.Equals(0) && Intervalo.Hour.Equals(0))
            {
                mensaje = "El intervalo no puede ser 00:00";
                return false;
            }
            return true;
        }
    }
}
