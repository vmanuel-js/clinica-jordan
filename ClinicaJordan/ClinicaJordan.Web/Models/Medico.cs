namespace ClinicaJordan.Web.Models
{
    public class Medico
    {
        public int IdMedico { get; set; }
        public string Dni { get; set; } = string.Empty;
        public string Nombres { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Turno { get; set; } = string.Empty;
        public string Especialidad { get; set; } = string.Empty;
        public int IdEspecialidad { get; set; }
        public DateOnly FechaNacimiento { get; set; }
        public DateOnly FechaRegistro { get; set; }
        public char Estado { get; set; }
    }
}
