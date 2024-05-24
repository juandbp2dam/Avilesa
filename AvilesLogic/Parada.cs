using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvilesLogic
{
    public class Parada
    {
        private string codMunicipioParada;
        public Parada() { 
            NumLinea = default(int);
            codMunicipioParada = string.Empty;
            NombreMunicipioParada = string.Empty;
            Intervalo = default(TimeOnly);
        }

        public Parada(int numLinea, string codMunicipioParada, TimeOnly intervalo)
        {
            NumLinea = numLinea;
            CodMunicipioParada = codMunicipioParada;
            Intervalo = intervalo;
        }

        [Key]
        public int NumLinea {  get; set; }
        [Key]
        public string CodMunicipioParada { 
            get { return codMunicipioParada; }
            set { 
                codMunicipioParada = value;
                Municipio mun = LogicaNegocio.ObtenerMunicipioPorCodigo(codMunicipioParada);
                NombreMunicipioParada = mun != null ? mun.Nombre : string.Empty;
            }
        }
        [Ignore]
        public string NombreMunicipioParada { get; private set; }
        [Format("HH:mm")]
        public TimeOnly Intervalo { get; set; }

        public bool ValidarParada(out string mensaje)
        {
            mensaje = string.Empty;
            if (string.IsNullOrEmpty(CodMunicipioParada))
            {
                mensaje = "No se ha introducido un municipio de parada";
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
