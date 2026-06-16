using ClinicaJordan.Web.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ClinicaJordan.Web.Data
{
    public class MedicoRepository
    {
        private readonly string _connectionString;
        public MedicoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ClinicaJordan")!;
        }

        public List<Medico> Listar()
        {
            var lista = new List<Medico>();

            using var cnx = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("SP_ListarMedicos", cnx);
            cmd.CommandType = CommandType.StoredProcedure;

            cnx.Open();
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new Medico
                {
                    IdMedico = Convert.ToInt32(reader["IdMedico"]),
                    Dni = reader["Dni"].ToString()!,
                    Nombres = reader["Nombres"].ToString()!,
                    Apellidos = reader["Apellidos"].ToString()!,
                    Correo = reader["Correo"].ToString()!,
                    Telefono = reader["Telefono"].ToString()!,
                    Turno = reader["Turno"].ToString()!,
                    Especialidad = reader["Especialidad"].ToString()!,
                    FechaNacimiento = DateOnly.FromDateTime(Convert.ToDateTime(reader["FechaNacimiento"])),
                    FechaRegistro = DateOnly.FromDateTime(Convert.ToDateTime(reader["FechaRegistro"])),
                    Estado = Convert.ToChar(reader["Estado"])
                });
            }

            return lista;
        }

        public void Insertar(Medico medico)
        {
            using var cnx = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("SP_InsertarMedico", cnx);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@dni", medico.Dni);
            cmd.Parameters.AddWithValue("@nombres", medico.Nombres);
            cmd.Parameters.AddWithValue("@apellidos", medico.Apellidos);
            cmd.Parameters.AddWithValue("@correo", medico.Correo);
            cmd.Parameters.AddWithValue("@telefono", medico.Telefono);
            cmd.Parameters.AddWithValue("@turno", medico.Turno);
            cmd.Parameters.AddWithValue("@id_especialidad", medico.IdEspecialidad);
            cmd.Parameters.AddWithValue("@fecha_nacimiento", medico.FechaNacimiento);

            cnx.Open();
            cmd.ExecuteNonQuery();
        }
    }
}
