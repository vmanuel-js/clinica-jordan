namespace ClinicaJordan.Web.Models
{
    public class Paciente
    {
        public int IdPaciente { get; set; }
        public string Dni { get; set; } = string.Empty;
        public string Nombres { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public DateOnly FechaNacimiento { get; set; } 
        public decimal Peso { get; set; }
        public decimal Altura { get; set; }
        public char Sexo { get; set; }
        public DateOnly FechaRegistro { get; set; }
        public char Estado { get; set; }
    }
}
