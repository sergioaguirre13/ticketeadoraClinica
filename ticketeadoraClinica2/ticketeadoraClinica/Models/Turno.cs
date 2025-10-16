namespace ticketeadoraClinica.Models
{
    public class Turno
    {
        public int Id { get; set; }
        public string Dni { get; set; }
        public string Codigo { get; set; }
        public string Tipo { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;



    }
}
