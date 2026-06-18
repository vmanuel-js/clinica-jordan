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
            cmd.Parameters.AddWithValue("@fecha_nacimiento", medico.FechaNacimiento.ToDateTime(TimeOnly.MinValue));

            cnx.Open();
            cmd.ExecuteNonQuery();
        }

        public Medico? ObtenerPorDni(string dni)
        {
            using var cnx = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("SP_ObtenerMedicoPorDni", cnx);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@dni", dni);
            cnx.Open();

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Medico
                {
                    IdMedico = Convert.ToInt32(reader["IdMedico"]),
                    Dni = reader["Dni"].ToString()!,
                    Nombres = reader["Nombres"].ToString()!,
                    Apellidos = reader["Apellidos"].ToString()!,
                    Correo = reader["Correo"].ToString()!,
                    Telefono = reader["Telefono"].ToString()!,
                    Turno = reader["Turno"].ToString()!,
                    IdEspecialidad = Convert.ToInt32(reader["IdEspecialidad"]),
                    FechaNacimiento = DateOnly.FromDateTime(Convert.ToDateTime(reader["FechaNacimiento"])),
                    Estado = Convert.ToChar(reader["Estado"])
                };
            }
            return null;
        }

        public void Actualizar(Medico medico)
        {
            using var cnx = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("SP_ActualizarMedico", cnx);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@dni", medico.Dni);
            cmd.Parameters.AddWithValue("@nombres", medico.Nombres);
            cmd.Parameters.AddWithValue("@apellidos", medico.Apellidos);
            cmd.Parameters.AddWithValue("@correo", medico.Correo);
            cmd.Parameters.AddWithValue("@telefono", medico.Telefono);
            cmd.Parameters.AddWithValue("@turno", medico.Turno);
            cmd.Parameters.AddWithValue("@id_especialidad", medico.IdEspecialidad);
            cmd.Parameters.AddWithValue("@fecha_nacimiento", medico.FechaNacimiento.ToDateTime(TimeOnly.MinValue));
            cmd.Parameters.AddWithValue("@estado", medico.Estado);

            cnx.Open();
            cmd.ExecuteNonQuery();
        }

        public string Desactivar(string dni)
        {
            var medico = ObtenerPorDni(dni);

            if (medico == null)
                return $"El médico con el DNI: {dni} no existe.";
            if (medico.Estado == 'I')
                return "El médico ya se encuentra inactivo";

            using var cnx = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("SP_DesactivarMedico", cnx);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@dni", dni);

            cnx.Open();
            cmd.ExecuteNonQuery();

            return "OK";
        }
    }
}
