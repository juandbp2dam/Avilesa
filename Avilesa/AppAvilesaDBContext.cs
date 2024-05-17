using AvilesLogic;
using CsvHelper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avilesa
{
    public class AppAvilesaDBContext : DbContext
    {
        public ObservableCollection<Linea> Lineas { get; set; }
        public ObservableCollection<Parada> Paradas { get; set; }
        public ObservableCollection<Municipio> Municipios { get; set; }

        public AppAvilesaDBContext()
        {
            Lineas = new ObservableCollection<Linea>();
            Paradas = new ObservableCollection<Parada>();
            Municipios = new ObservableCollection<Municipio>();
            LogicaNegocio.LstMunicipios.ForEach(m => { 
                Municipios.Add(m);
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("InMemoryDatabase");
        }


        public override int SaveChanges()
        {
            int retValue = base.SaveChanges();
            saveToCsv();
            return retValue;
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            int retValue = base.SaveChanges(acceptAllChangesOnSuccess);
            saveToCsv();
            return retValue;
        }

        private void saveToCsv()
        {
            using (var writer = new StreamWriter(CsvDatos.RutaArchivoLineas))
            using (var csvWriter = new CsvWriter(writer, CsvDatos.CsvConfig))
            {
                csvWriter.WriteRecords(this.Lineas.ToList().OrderBy(l => l.Numero));
            }

            using (var writer = new StreamWriter(CsvDatos.RutaArchivoParadas))
            using (var csvWriter = new CsvWriter(writer, CsvDatos.CsvConfig))
            {
                csvWriter.WriteRecords(this.Paradas.ToList().OrderBy(p => p.NumLinea).ThenBy(p => p.Intervalo));
            }
        }

    }
}
