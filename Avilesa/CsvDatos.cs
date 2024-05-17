using AvilesLogic;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avilesa
{
    public static class CsvDatos
    {
        
        private static string ruta = AppDomain.CurrentDomain.BaseDirectory;

        public static string RutaArchivoLineas =
            System.IO.Path.Combine(ruta, "Data", LogicaNegocio.NombreArchLineas);
        public static string RutaArchivoParadas =
            System.IO.Path.Combine(ruta, "Data", LogicaNegocio.NombreArchParadas);

        /// <summary>
        /// Configuración del CSV
        /// </summary>
        public static CsvConfiguration CsvConfig =
            new CsvConfiguration(CultureInfo.CurrentCulture)
            { Delimiter = ",", Encoding = Encoding.UTF8 };
    }
}
