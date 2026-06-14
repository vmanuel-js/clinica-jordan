using ClinicaJordan.Web.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Net;

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

        public void Insertar(Paciente paciente)
        {
            using var cnx = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("SP_InsertarPaciente", cnx);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@dni", paciente.Dni);
            cmd.Parameters.AddWithValue("@nombres", paciente.Nombres);
            cmd.Parameters.AddWithValue("@apellidos", paciente.Apellidos);
            cmd.Parameters.AddWithValue("@correo", paciente.Correo);
            cmd.Parameters.AddWithValue("@telefono", paciente.Telefono);
            cmd.Parameters.AddWithValue("@fecha_nacimiento", paciente.FechaNacimiento.ToDateTime(TimeOnly.MinValue));
            cmd.Parameters.AddWithValue("@peso", paciente.Peso);
            cmd.Parameters.AddWithValue("@altura", paciente.Altura);
            cmd.Parameters.AddWithValue("@sexo", paciente.Sexo);

            cnx.Open();
            cmd.ExecuteNonQuery();
        }

        public Paciente? ObtenerPorDni(string dni)
        {
            using var cnx = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("SP_ObtenerPacientePorDni", cnx);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@dni", dni);

            cnx.Open();
            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new Paciente
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
                };
            }
            return null;
        }

        public void Actualizar(Paciente paciente)
        {
            using var cnx = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("SP_ActualizarPaciente", cnx);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@dni", paciente.Dni);
            cmd.Parameters.AddWithValue("@correo", paciente.Correo);
            cmd.Parameters.AddWithValue("@telefono", paciente.Telefono);
            cmd.Parameters.AddWithValue("@fecha_nacimiento", paciente.FechaNacimiento.ToDateTime(TimeOnly.MinValue));
            cmd.Parameters.AddWithValue("@peso", paciente.Peso);
            cmd.Parameters.AddWithValue("@altura", paciente.Altura);
            cmd.Parameters.AddWithValue("@sexo", paciente.Sexo);
            cmd.Parameters.AddWithValue("@estado", paciente.Estado);

            cnx.Open();
            cmd.ExecuteNonQuery();
        }
    }
}
