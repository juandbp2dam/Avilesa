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
        public Parada() { 
            NumLinea = default(int);
            CodMunicipioParada = string.Empty;
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
        public string CodMunicipioParada { get; set; }
        [Format("HH:mm")]
        public TimeOnly Intervalo { get; set; }
    }
}
