namespace AvilesLogic
{
    public static class LogicaNegocio
    {
        public static string NombreArchLineas = "lineas.csv";
        public static string NombreArchParadas = "paradas.csv";

        public static List<Municipio> LstMunicipios = new List<Municipio>() { 
            new Municipio("33004", "Avilés"),
            new Municipio("33204", "Gijón"),
            new Municipio("33020", "Corvera"),
            new Municipio("33016", "Castrillón"),
            new Municipio("33044", "Oviedo"),
            new Municipio("33066", "Siero"),
            new Municipio("33070", "Tapia de Casariego"),
            new Municipio("33051", "Pravia"),
            new Municipio("33021", "Cudillero"),
            new Municipio("33034", "Valdés"),
            new Municipio("33041", "Navia"),
            new Municipio("33014", "Carreño"),
            new Municipio("33035", "Llanera")
        };

        public static Municipio ObtenerMunicipioPorCodigo(string codigo) {
            return LstMunicipios.FirstOrDefault(mun => mun.Codigo.Equals(codigo));
        }


    }
}
