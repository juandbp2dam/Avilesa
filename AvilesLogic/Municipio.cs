using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvilesLogic
{
    public class Municipio
    {
        public Municipio(string codigo, string nombre)
        {
            Codigo = codigo;
            Nombre = nombre;
        }

        public string Codigo { get; set;}
        public string Nombre { get; set;}
    }
}
