namespace Api_2W1_CQRS.Resultados
{
    public class ResultadoPersona : ResultadoBase
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public int Dni { get; set; }

        public string Sexo1 { get; set; }

        public string Pais { get; set; }

        public string Provincia { get; set; }
    }

}
