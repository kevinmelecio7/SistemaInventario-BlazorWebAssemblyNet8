using AppInvWebSharedLibrary.Contracts;
using AppInvWebSharedLibrary.DTOs;
using Dapper;
using Microsoft.Data.SqlClient;

namespace AppInvWebServer.Repositories
{
    public class User : IUser
    {
        private readonly string? _connectionSQL;

        public User(IConfiguration configuration)
        {
            _connectionSQL = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<UserDTO>> GetUsersAsync()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionSQL))
                {
                    await connection.OpenAsync();
                    string sql = "SELECT ID as id, Name as nombre, Email as correo, Role as rol FROM users;";
                    var dtos = await connection.QueryAsync<UserDTO>(sql);
                    return dtos.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los usuarios", ex);
            }
        }
        public async Task UpdateUserAsync(UserDTO user)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionSQL))
                {
                    await connection.OpenAsync();
                    string sql = "UPDATE Users SET Name = @name, Email = @email, Role = @role WHERE Id = @id;";
                    var parameters = new
                    {
                        id = user.id,
                        name = user.nombre,
                        email = user.correo,
                        role = user.rol
                    };

                    await connection.ExecuteAsync(sql, parameters);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar: ", ex);
            }
        }
        public async Task UpdatePasswordUserAsync(UserDTO user)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionSQL))
                {
                    await connection.OpenAsync();
                    string sql = "UPDATE Users SET Password = @password WHERE Id = @id;";

                    string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.correo);

                    var parameters = new
                    {
                        id = user.id,
                        password = hashedPassword
                    };

                    await connection.ExecuteAsync(sql, parameters);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar: ", ex);
            }
        }
        public async Task DeleteUserAsync(UserDTO user)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionSQL))
                {
                    await connection.OpenAsync();
                    string sql = "DELETE FROM Users WHERE Id = @id;";

                    var parameters = new { id = user.id };

                    await connection.ExecuteAsync(sql, parameters);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar: ", ex);
            }
        }

    }
}
