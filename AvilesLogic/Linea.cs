using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace AvilesLogic
{
    public class Linea
    {
        private List<Parada> itinerario = new List<Parada>();
        private string codMunicipioOrigen;
        private string codMunicipioDestino;

        public Linea() {
            Numero = default(int);
            codMunicipioOrigen = string.Empty;
            codMunicipioDestino = string.Empty;
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
        public string CodMunicipioOrigen { 
            get { 
                return codMunicipioOrigen;
            } 
            set {
                codMunicipioOrigen = value;
                Municipio mun = LogicaNegocio.ObtenerMunicipioPorCodigo(codMunicipioOrigen);
                NombreMunicipioOrigen = mun!=null ? mun.Nombre : string.Empty;
            } 
        }
        [Ignore]
        public string NombreMunicipioOrigen { get; private set; }
        public string CodMunicipioDestino
        {
            get
            {
                return codMunicipioDestino;
            }
            set
            {
                codMunicipioDestino = value;
                Municipio mun = LogicaNegocio.ObtenerMunicipioPorCodigo(codMunicipioDestino);
                NombreMunicipioDestino = mun != null ? mun.Nombre : string.Empty;
            }
        }
        [Ignore]
        public string NombreMunicipioDestino { get; private set; }
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

        public void InsertarItinerario(List<Parada> nuevoItinerario) {
            itinerario.Clear();
            itinerario.AddRange(nuevoItinerario);
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
            else if (itinerario.Count > 0)
            {
                // 1. ¿Está el destino de la parada en el itinerario?
                if (!itinerario.Any(p => p.CodMunicipioParada.Equals(codMunicipioDestino))) {
                    mensaje = "El itinerario debe contener el destino de la línea";
                    return false;
                } // 2. ¿Es la última parada la correspondiente al destino?
                else
                {
                    Parada ultimaParada = itinerario.OrderBy(i => i.Intervalo).Last();
                    if (!ultimaParada.CodMunicipioParada.Equals(codMunicipioDestino))
                    {
                        mensaje = "El itinerario debe acabar en el municipio de destino";
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
