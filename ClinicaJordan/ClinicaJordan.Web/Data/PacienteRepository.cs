using ClinicaJordan.Web.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ClinicaJordan.Web.Data
{
    public class PacienteRepository
    {
        private readonly string _connectionString;

        public PacienteRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ClinicaJordan")!;
        }

        public List<Paciente> ObtenerTodos()
        { 
            var lista = new List<Paciente>();

            using var cnx = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("SP_ListarPaciente", cnx);
            cmd.CommandType = CommandType.StoredProcedure;

            cnx.Open();
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new Paciente
                {
                    IdPaciente = Convert.ToInt32(reader["IdPaciente"]),
                    Dni = reader["Dni"].ToString()!,
                    Nombres = reader["Nombres"].ToString()!,
                    Apellidos = reader["Apellidos"].ToString()!,
                    Correo = reader["Correo"].ToString()!,
                    Telefono = reader["Telefono"].ToString()!,
                    FechaNacimiento = DateOnly.FromDateTime(Convert.ToDateTime(reader["FechaNacimiento"])),
                    Peso = Convert.ToDecimal(reader["Peso"]),
                    Altura = Convert.ToDecimal(reader["Altura"]),
                    Sexo = Convert.ToChar(reader["Sexo"]),
                    FechaRegistro = DateOnly.FromDateTime(Convert.ToDateTime(reader["FechaRegistro"])),
                    Estado = Convert.ToChar(reader["Estado"])
                });
            }

            return lista;
        }
    }
}
