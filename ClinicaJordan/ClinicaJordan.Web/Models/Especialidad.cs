namespace ClinicaJordan.Web.Models
{
    public class Especialidad
    {
        public int IdEspecialidad { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public char Estado { get; set; }
    }
}
