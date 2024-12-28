using AppInvWebSharedLibrary.Contracts;
using AppInvWebSharedLibrary.DTOs.Excel;
using Microsoft.Data.SqlClient;

namespace AppInvWebServer.Repositories
{
    public class Bitacora : IBitacora
    {
        private readonly string? _connectionSQL;

        public Bitacora(IConfiguration configuration)
        {
            _connectionSQL = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task InsertBitacoraAsync(BitacoraDTO model)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionSQL))
                {
                    await connection.OpenAsync();
                    string sql = "INSERT INTO bitacora(vista, accion, tipo, descripcion, usuario, fecha) VALUES (@vista, @accion, @tipo, @descripcion, @usuario, GETDATE());";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@vista", model.vista);
                        command.Parameters.AddWithValue("@accion", model.accion);
                        command.Parameters.AddWithValue("@tipo", model.tipo);
                        command.Parameters.AddWithValue("@descripcion", model.descripcion);
                        command.Parameters.AddWithValue("@usuario", model.usuario);

                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                throw new Exception("Error SQL: ", sqlEx);
            }
            catch (Exception ex)
            {
                throw new Exception("Error: ", ex);
            }
        }
    }
}
