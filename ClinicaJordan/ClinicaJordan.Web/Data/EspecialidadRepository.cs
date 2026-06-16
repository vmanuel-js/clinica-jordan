using ClinicaJordan.Web.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ClinicaJordan.Web.Data
{
    public class EspecialidadRepository
    {
        private readonly string _connectionString;

        public EspecialidadRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ClinicaJordan")!;    
        }

        public List<Especialidad> Listar()
        {
            var lista = new List<Especialidad>();

            using var cnx = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("SP_ListarEspecialidad", cnx);
            cmd.CommandType = CommandType.StoredProcedure;

            cnx.Open();
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new Especialidad
                {
                    IdEspecialidad = Convert.ToInt32(reader["IdEspecialidad"]),
                    Nombre = reader["Nombre"].ToString()!,
                    Descripcion = reader["Descripcion"].ToString()!,
                    Estado = Convert.ToChar(reader["Estado"])
                });
            }
            return lista;
        }

        public void Insertar(Especialidad especialidad)
        {
            using var cnx = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("SP_InsertarEspecialidad", cnx);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@nombre", especialidad.Nombre);
            cmd.Parameters.AddWithValue("@descripcion", especialidad.Descripcion);

            cnx.Open();
            cmd.ExecuteNonQuery();
        }

        public Especialidad? ObtenerEspecialidadPorId(int id_especialidad)
        {
            using var cnx = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("SP_ObtenerEspecialidadPorId", cnx);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@id_especialidad", id_especialidad);

            cnx.Open();
            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new Especialidad
                {
                    IdEspecialidad = Convert.ToInt32(reader["IdEspecialidad"]),
                    Nombre = reader["Nombre"].ToString()!,
                    Descripcion = reader["Descripcion"].ToString()!,
                    Estado = Convert.ToChar(reader["Estado"])
                };
            }

            return null;
        }


        public void Actualizar(Especialidad especialidad)
        {
            using var cnx = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("SP_ActualizarEspecialidad", cnx);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@id_especialidad", especialidad.IdEspecialidad);
            cmd.Parameters.AddWithValue("@nombre", especialidad.Nombre);
            cmd.Parameters.AddWithValue("@descripcion", especialidad.Descripcion);
            cmd.Parameters.AddWithValue("@estado", especialidad.Estado);

            cnx.Open();
            cmd.ExecuteNonQuery();
        }

        public string Desactivar(int id)
        {
            var especialidad = ObtenerEspecialidadPorId(id);

            if (especialidad == null)
                return "Esta especialidad no existe";

            if (especialidad.Estado == 'I')
                return "El estado de esta especialidad ya se encuentra inactiva";

            using var cnx = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("SP_DesactivarEspecialidad", cnx);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@id_especialidad", id);

            cnx.Open();
            cmd.ExecuteNonQuery();

            return "OK";
        }
    }
}
