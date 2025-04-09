using AppInvWebSharedLibrary.Contracts;
using AppInvWebSharedLibrary.DTOs;
using AppInvWebSharedLibrary.DTOs.Excel;
using Dapper;
using Microsoft.Data.SqlClient;

namespace AppInvWebServer.Repositories
{
    public class InputsData : IInputsData
    {
        private readonly string? _connectionSQL;

        public InputsData(IConfiguration configuration)
        {
            _connectionSQL = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<PeriodoDTO>> GetPeriodoAsync()
        {
            var dtos = new List<PeriodoDTO>();
            try
            {
                using (var connection = new SqlConnection(_connectionSQL))
                {
                    await connection.OpenAsync();
                    string sql = "SELECT * FROM periodo ORDER BY id_periodo desc;";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                dtos.Add(new PeriodoDTO
                                {
                                    id = Convert.ToInt32(reader["id_periodo"]),
                                    periodo = reader["periodo"].ToString(),
                                    activo = Convert.ToInt32(reader["activo"]),
                                    fecha = DateTime.Parse(reader["fecha"].ToString()),
                                    consecutivo = reader.IsDBNull(reader.GetOrdinal("consecutivo")) ? 0 : Convert.ToInt32(reader["consecutivo"]),
                                    identificador = reader.IsDBNull(reader.GetOrdinal("identificador")) ? "" : reader["identificador"].ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: ", ex);
            }
            return dtos;
        }
        public async Task InsertPeriodoAsync(PeriodoDTO model)
        {
            try
            {
                List<PeriodoDTO> listPeriodo = await GetPeriodoAsync();
                var filtroListPeriodo = listPeriodo.Where(x => x.periodo == model.periodo).OrderByDescending(z => z.consecutivo).ToList();

                using (var connection = new SqlConnection(_connectionSQL))
                {
                    await connection.OpenAsync();
                    string sql = "INSERT INTO periodo (periodo, activo, fecha, consecutivo, identificador) VALUES (@Periodo, @Activo, @Fecha, @Consecutivo, @Identificador)";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Periodo", model.periodo);
                        command.Parameters.AddWithValue("@Activo", model.activo);
                        command.Parameters.AddWithValue("@Fecha", model.fecha);
                        command.Parameters.AddWithValue("@Consecutivo", filtroListPeriodo.Count() + 1 );
                        command.Parameters.AddWithValue("@Identificador", model.identificador);

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
        public async Task UpdatePeriodoAsync(PeriodoDTO model)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionSQL))
                {
                    await connection.OpenAsync();
                    string sql = "UPDATE periodo SET periodo = @Periodo, activo = @Activo, fecha = @Fecha WHERE id_periodo = @Id";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Id", model.id);
                        command.Parameters.AddWithValue("@Periodo", model.periodo);
                        command.Parameters.AddWithValue("@Activo", model.activo);
                        command.Parameters.AddWithValue("@Fecha", model.fecha);

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

        public async Task<List<StorageBinDTO>> GetStorageAsync(int periodo)
        {
            var dtos = new List<StorageBinDTO>();
            int pageSize = 1000; // Tamaño de cada página
            int offset = 0;      // Inicio de los registros a consultar

            try
            {
                using (var connection = new SqlConnection(_connectionSQL))
                {
                    await connection.OpenAsync();

                    // Consulta con paginación
                    string sql = @"
                SELECT id_storage AS Id, storagebin, storagetype, fkPeriodo
                FROM data_storage
                WHERE fkPeriodo = @Periodo
                ORDER BY id_storage
                OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;";

                    while (true)
                    {
                        // Ejecuta la consulta para una página
                        var partialData = (await connection.QueryAsync<StorageBinDTO>(
                            sql,
                            new { Periodo = periodo, Offset = offset, PageSize = pageSize }
                        )).ToList();

                        // Si no hay más datos, rompe el bucle
                        if (!partialData.Any())
                            break;

                        // Agrega los datos a la lista principal
                        dtos.AddRange(partialData);

                        // Incrementa el offset para la siguiente página
                        offset += pageSize;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los datos de almacenamiento: ", ex);
            }

            return dtos;
        }
        public async Task InsertStorageAsync(List<StorageBinDTO> list)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionSQL))
                {
                    await connection.OpenAsync();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            string sql = "INSERT INTO data_storage (storagebin, storagetype, fkPeriodo) VALUES (@storagebin, @storagetype, @fkperiodo);";
                            foreach (var obj in list)
                            {
                                using (var command = new SqlCommand(sql, connection, transaction))
                                {
                                    command.Parameters.AddWithValue("@storagebin", obj.storagebin);
                                    command.Parameters.AddWithValue("@storagetype", obj.storagetype);
                                    command.Parameters.AddWithValue("@fkperiodo", obj.fkPeriodo);

                                    await command.ExecuteNonQueryAsync();
                                }
                            }
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw new Exception("Error al insertar: ", ex);
                        }
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
        public async Task UpdateStorageAsync(StorageBinDTO obj)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionSQL))
                {
                    await connection.OpenAsync();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            string sql = "UPDATE data_storage SET storagebin = @storagebin, storagetype = @storagetype, fkPeriodo = @fkperiodo WHERE id_storage = @id_storage;";

                            using (var command = new SqlCommand(sql, connection, transaction))
                            {
                                command.Parameters.AddWithValue("@id_storage", obj.id);
                                command.Parameters.AddWithValue("@storagebin", obj.storagebin);
                                command.Parameters.AddWithValue("@storagetype", obj.storagetype);
                                command.Parameters.AddWithValue("@fkperiodo", obj.fkPeriodo);

                                await command.ExecuteNonQueryAsync();
                            }

                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw new Exception("Error al actualizar: ", ex);
                        }
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
        public async Task DeleteStorageAsync(List<int> list)
        {
            string sql = string.Empty;
            try
            {
                using (var connection = new SqlConnection(_connectionSQL))
                {
                    await connection.OpenAsync();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            sql = "DELETE FROM data_storage WHERE id_storage = @id;";
                            foreach (var obj in list)
                            {
                                using (var command = new SqlCommand(sql, connection, transaction))
                                {
                                    command.Parameters.AddWithValue("@id", obj);

                                    await command.ExecuteNonQueryAsync();
                                }
                            }
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw new Exception("Error al eliminar: ", ex);
                        }
                    }
                    sql = "DECLARE @ultimoId INT; " +
                        "SELECT @ultimoId = ISNULL(MAX(id_storage), 0) FROM data_storage;" +
                        "DBCC CHECKIDENT ('data_storage', RESEED, @ultimoId);";
                    using (var command = new SqlCommand(sql, connection))
                    {
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

        public async Task<List<MasterDataDTO>> GetMasterDataAsync(int periodo)
        {
            var dtos = new List<MasterDataDTO>();
            int pageSize = 500; // Tamaño de cada página
            int offset = 0;      // Inicio de los registros a consultar

            try
            {
                using (var connection = new SqlConnection(_connectionSQL))
                {
                    await connection.OpenAsync();

                    // Consulta con paginación
                    string sql = @"
                SELECT id_material, materialID, descripcion, unit_price, fkPeriodo
                FROM data_materials
                WHERE fkPeriodo = @Periodo
                ORDER BY id_material
                OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;";

                    while (true)
                    {
                        // Ejecuta la consulta para una página
                        var partialData = (await connection.QueryAsync<MasterDataDTO>(
                            sql,
                            new { Periodo = periodo, Offset = offset, PageSize = pageSize }
                        )).ToList();

                        // Si no hay más datos, rompe el bucle
                        if (!partialData.Any())
                            break;

                        // Agrega los datos a la lista principal
                        dtos.AddRange(partialData);

                        // Incrementa el offset para la siguiente página
                        offset += pageSize;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los datos maestros: ", ex);
            }

            return dtos;
        }
        public async Task InsertMasterDataAsync(List<MasterDataDTO> list)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionSQL))
                {
                    await connection.OpenAsync();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            string sql = "INSERT INTO data_materials (materialID, descripcion, unit_price, fkPeriodo) VALUES (@materialID, @descripcion, @unit_price, @fkperiodo);";
                            foreach (var obj in list)
                            {
                                using (var command = new SqlCommand(sql, connection, transaction))
                                {
                                    command.Parameters.AddWithValue("@materialID", obj.materialID);
                                    command.Parameters.AddWithValue("@descripcion", obj.descripcion);
                                    command.Parameters.AddWithValue("@unit_price", obj.unit_price);
                                    command.Parameters.AddWithValue("@fkperiodo", obj.fkPeriodo);

                                    await command.ExecuteNonQueryAsync();
                                }
                            }
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw new Exception("Error al insertar: ", ex);
                        }
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
        public async Task UpdateMasterDataAsync(MasterDataDTO obj)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionSQL))
                {
                    await connection.OpenAsync();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            string sql = "UPDATE data_materials SET materialID = @materialID, descripcion = @descripcion, unit_price = @unit_price, fkPeriodo = @fkperiodo WHERE id_material = @id_material;";

                            using (var command = new SqlCommand(sql, connection, transaction))
                            {
                                command.Parameters.AddWithValue("@id_material", obj.id_material);
                                command.Parameters.AddWithValue("@materialID", obj.materialID);
                                command.Parameters.AddWithValue("@descripcion", obj.descripcion);
                                command.Parameters.AddWithValue("@unit_price", obj.unit_price);
                                command.Parameters.AddWithValue("@fkperiodo", obj.fkPeriodo);

                                await command.ExecuteNonQueryAsync();
                            }

                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw new Exception("Error al insertar: ", ex);
                        }
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
        public async Task DeleteMasterDataAsync(List<int> list)
        {
            string sql = string.Empty;
            try
            {
                using (var connection = new SqlConnection(_connectionSQL))
                {
                    await connection.OpenAsync();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            sql = "DELETE FROM data_materials WHERE id_material = @id;";
                            foreach (var obj in list)
                            {
                                using (var command = new SqlCommand(sql, connection, transaction))
                                {
                                    command.Parameters.AddWithValue("@id", obj);

                                    await command.ExecuteNonQueryAsync();
                                }
                            }
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw new Exception("Error al eliminar: ", ex);
                        }
                    }
                    sql = "DECLARE @ultimoId INT; " +
                        "SELECT @ultimoId = ISNULL(MAX(id_material), 0) FROM data_materials;" +
                        "DBCC CHECKIDENT ('data_materials', RESEED, @ultimoId);";
                    using (var command = new SqlCommand(sql, connection))
                    {
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

        public async Task<List<InitialLoadDTO>> GetInitialLoadAsync(int periodo)
        {
            var dtos = new List<InitialLoadDTO>();
            int pageSize = 500; // Tamaño de cada página
            int offset = 0;      // Inicio de los registros a consultar

            try
            {
                using (var connection = new SqlConnection(_connectionSQL))
                {
                    await connection.OpenAsync();

                    // Consulta con paginación
                    string sql = @"
                SELECT id_saldos, storage_type, storage_bin, material_number, material_description, 
                       base_unit_of_measure, total_quantity, total_cost, currency, unit_standard_cost, fkPeriodo, folio, estado
                FROM saldos_iniciales
                WHERE fkPeriodo = @Periodo
                ORDER BY id_saldos
                OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;";

                    while (true)
                    {
                        // Ejecuta la consulta para una página
                        var partialData = (await connection.QueryAsync<InitialLoadDTO>(
                            sql,
                            new { Periodo = periodo, Offset = offset, PageSize = pageSize }
                        )).ToList();

                        // Si no hay más datos, rompe el bucle
                        if (!partialData.Any())
                            break;

                        // Agrega los datos a la lista principal
                        dtos.AddRange(partialData);

                        // Incrementa el offset para la siguiente página
                        offset += pageSize;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cargar los datos: ", ex);
            }

            return dtos;
        }
        public async Task<List<InitialLoadDTO>> GetInitialLoadFirstAsync(int periodo)
        {

            var dtos = new List<InitialLoadDTO>();
            try
            {
                using (var connection = new SqlConnection(_connectionSQL))
                {

                    await connection.OpenAsync();

                    string sql = "SELECT TOP 1 id_saldos, storage_type, storage_bin, material_number, material_description, base_unit_of_measure, total_quantity, " +
                        "total_cost, currency, unit_standard_cost, fkPeriodo, folio, estado  FROM saldos_iniciales WHERE fkPeriodo = @Periodo";
                    dtos = (await connection.QueryAsync<InitialLoadDTO>(sql, new { Periodo = periodo })).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: ", ex);
            }
            return dtos;
        }
        public async Task<List<InitialLoadDTO>> GetInitialMaterialLoadAsync(int periodo, string material)
        {

            var dtos = new List<InitialLoadDTO>();
            try
            {
                using (var connection = new SqlConnection(_connectionSQL))
                {

                    await connection.OpenAsync();

                    string sql = "SELECT id_saldos, storage_type, storage_bin, material_number, material_description, base_unit_of_measure, total_quantity, " +
                        "total_cost, currency, unit_standard_cost, fkPeriodo, folio, estado  FROM saldos_iniciales WHERE fkPeriodo = @Periodo and material_number = @Material_number;";
                    dtos = (await connection.QueryAsync<InitialLoadDTO>(sql, new { Periodo = periodo, Material_number = material })).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: ", ex);
            }
            return dtos;
        }
        public async Task<List<InitialLoadDTO>> GetInitialLoadMaterialxStorageAsync(int periodo, string material, string storage)
        {

            var dtos = new List<InitialLoadDTO>();
            try
            {
                using (var connection = new SqlConnection(_connectionSQL))
                {

                    await connection.OpenAsync();

                    string sql = "SELECT id_saldos, storage_type, storage_bin, material_number, material_description, base_unit_of_measure, total_quantity, " +
                        "total_cost, currency, unit_standard_cost, fkPeriodo, folio, estado FROM saldos_iniciales WHERE fkPeriodo = @Periodo and material_number = @Material_number and storage_bin = @Storagebin order by id_saldos asc ;";
                    dtos = (await connection.QueryAsync<InitialLoadDTO>(sql, new { Periodo = periodo, Material_number = material, Storagebin = storage })).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: ", ex);
            }
            return dtos;
        }
        public async Task InsertInitialLoadAsync(List<InitialLoadDTO> list)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionSQL))
                {
                    await connection.OpenAsync();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            string sql = "INSERT INTO saldos_iniciales( plant, warehouse, storage_location, storage_type, storage_bin, storage_unit, material_number, material_description, base_unit_of_measure, total_quantity, total_cost, currency, unit_standard_cost, unrestricted_stock, blocked_stock, quality_inspection, returns_stock, transfer_stock,  consignment_stock, consignment_value, execution_date, fkPeriodo) " +
                                "VALUES(@plant, @warehouse, @storage_location, @storage_type, @storage_bin, @storage_unit, @material_number, @material_description, @base_unit_of_measure, @total_quantity, @total_cost, @currency, @unit_standard_cost, @unrestricted_stock, @blocked_stock, @quality_inspection, @returns_stock, @transfer_stock,  @consignment_stock, @consignment_value, @execution_date, @fkPeriodo)";
                            foreach (var obj in list)
                            {

                                using (var command = new SqlCommand(sql, connection, transaction))
                                {
                                    command.Parameters.AddWithValue("@plant", obj.plant);
                                    command.Parameters.AddWithValue("@warehouse", obj.warehouse);
                                    command.Parameters.AddWithValue("@storage_location", obj.storage_location);
                                    command.Parameters.AddWithValue("@storage_type", obj.storage_type);
                                    command.Parameters.AddWithValue("@storage_bin", obj.storage_bin);
                                    command.Parameters.AddWithValue("@storage_unit", obj.storage_unit);
                                    command.Parameters.AddWithValue("@material_number", obj.material_number);
                                    command.Parameters.AddWithValue("@material_description", obj.material_description);
                                    command.Parameters.AddWithValue("@base_unit_of_measure", obj.base_unit_of_measure);
                                    command.Parameters.AddWithValue("@total_quantity", obj.total_quantity);
                                    command.Parameters.AddWithValue("@total_cost", obj.total_cost);
                                    command.Parameters.AddWithValue("@currency", obj.currency);
                                    command.Parameters.AddWithValue("@unit_standard_cost", obj.unit_standard_cost);
                                    command.Parameters.AddWithValue("@unrestricted_stock", obj.unrestricted_stock);
                                    command.Parameters.AddWithValue("@blocked_stock", obj.blocked_stock);
                                    command.Parameters.AddWithValue("@quality_inspection", obj.quality_inspection);
                                    command.Parameters.AddWithValue("@returns_stock", obj.returns_stock);
                                    command.Parameters.AddWithValue("@transfer_stock", obj.transfer_stock);
                                    command.Parameters.AddWithValue("@consignment_stock", obj.consignment_stock);
                                    command.Parameters.AddWithValue("@consignment_value", obj.consignment_value);
                                    command.Parameters.AddWithValue("@execution_date", obj.execution_date);
                                    command.Parameters.AddWithValue("@fkPeriodo", obj.fkPeriodo);
                                    await command.ExecuteNonQueryAsync();
                                }
                            }
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw new Exception("Error al insertar: ", ex);
                        }
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
        public async Task DeleteInitialLoadAsync(List<int> list)
        {
            string sql = string.Empty;
            try
            {
                using (var connection = new SqlConnection(_connectionSQL))
                {
                    await connection.OpenAsync();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            sql = "DELETE FROM saldos_iniciales WHERE id_saldos = @id;";
                            foreach (var obj in list)
                            {
                                using (var command = new SqlCommand(sql, connection, transaction))
                                {
                                    command.Parameters.AddWithValue("@id", obj);

                                    await command.ExecuteNonQueryAsync();
                                }
                            }
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw new Exception("Error al eliminar: ", ex);
                        }
                    }
                    sql = "DECLARE @ultimoId INT; " +
                        "SELECT @ultimoId = ISNULL(MAX(id_saldos), 0) FROM saldos_iniciales;" +
                        "DBCC CHECKIDENT ('saldos_iniciales', RESEED, @ultimoId);";
                    using (var command = new SqlCommand(sql, connection))
                    {
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
        public async Task UpdateInitialLoadFolioAsync()
        {
            List<InitialLoadDTO> listInitial = new List<InitialLoadDTO>();
            List<PeriodoDTO> listPeriodo = new List<PeriodoDTO>();

            listPeriodo = await GetPeriodoAsync();
            var periodoActual = listPeriodo.Where(item => item.activo == 1).FirstOrDefault();
            string stringPeriodo = periodoActual!.periodo!.Replace("-", "");
            listInitial = await GetInitialLoadAsync(periodoActual.id);
            string folio = string.Empty;
            string sql = string.Empty;
            try
            {
                using (var connection = new SqlConnection(_connectionSQL))
                {
                    await connection.OpenAsync();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            int i = 0;
                            sql = "UPDATE saldos_iniciales SET folio = @folio, estado = @estado WHERE id_saldos = @id;";
                            foreach (var obj in listInitial)
                            {
                                i++;
                                folio = "R" + stringPeriodo + "P" + periodoActual.consecutivo + "-" + i;
                                //Console.WriteLine(folio + "\n");
                                using (var command = new SqlCommand(sql, connection, transaction))
                                {
                                    command.Parameters.AddWithValue("@id", obj.id_saldos);
                                    command.Parameters.AddWithValue("@folio", folio);
                                    command.Parameters.AddWithValue("@estado", "PENDIENTE");

                                    await command.ExecuteNonQueryAsync();
                                }
                            }

                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw new Exception("Error al eliminar: ", ex);
                        }
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
        public async Task UpdateInitialLoadEstadoAsync(InitialLoadDTO obj)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionSQL))
                {
                    await connection.OpenAsync();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            string sql = "UPDATE saldos_iniciales SET estado = @estado WHERE folio = @folio;";

                            using (var command = new SqlCommand(sql, connection, transaction))
                            {
                                command.Parameters.AddWithValue("@estado", obj.estado);
                                command.Parameters.AddWithValue("@folio", obj.folio);

                                await command.ExecuteNonQueryAsync();
                            }

                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw new Exception("Error al insertar: ", ex);
                        }
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
        public async Task InsertReporteAsync(ReporteDTO obj)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionSQL))
                {
                    await connection.OpenAsync();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            string sql = "INSERT INTO reporte (folio, periodo, estado, storage_bin, storage_type, material_number, material_descripcion, unit_standard_cost, cantidad_inicial, cantidad_contada, diferencia_cantidad, porcentaje_diferencia, importe_inicial, importe_contada, diferencia_importe, porcentaje_variacion_importe, usuario, fecha, periodoConsecutivo ) " +
                                            "VALUES (@folio, @periodo, @estado, @storage_bin, @storage_type, @material_number, @material_descripcion, @unit_standar_cost, @cantidad_inicial, @cantidad_contada, @diferencia_cantidad, @porcentaje_diferencia, @importe_inicial, @importe_contada, @diferencia_importe, @porcentaje_variacion_importe, @usuario, GETDATE(), @periodoConsecutivo);";

                            using (var command = new SqlCommand(sql, connection, transaction))
                            {
                                command.Parameters.AddWithValue("@folio", obj.folio);
                                command.Parameters.AddWithValue("@periodo", obj.periodo);
                                command.Parameters.AddWithValue("@estado", obj.estado);
                                command.Parameters.AddWithValue("@storage_bin", obj.storage_bin);
                                command.Parameters.AddWithValue("@storage_type", obj.storage_type);
                                command.Parameters.AddWithValue("@material_number", obj.material_number);
                                command.Parameters.AddWithValue("@material_descripcion", obj.material_descripcion);
                                command.Parameters.AddWithValue("@unit_standar_cost", obj.unit_standard_cost);
                                command.Parameters.AddWithValue("@cantidad_inicial", obj.cantidad_inicial);
                                command.Parameters.AddWithValue("@cantidad_contada", obj.cantidad_contada);
                                command.Parameters.AddWithValue("@diferencia_cantidad", obj.diferencia_cantidad);
                                command.Parameters.AddWithValue("@porcentaje_diferencia", obj.porcentaje_diferencia);
                                command.Parameters.AddWithValue("@importe_inicial", obj.importe_inicial);
                                command.Parameters.AddWithValue("@importe_contada", obj.importe_contada);
                                command.Parameters.AddWithValue("@diferencia_importe", obj.diferencia_importe);
                                command.Parameters.AddWithValue("@porcentaje_variacion_importe", obj.porcentaje_variacion_importe);
                                command.Parameters.AddWithValue("@usuario", obj.usuario);
                                command.Parameters.AddWithValue("@periodoConsecutivo", obj.periodoConsecutivo);

                                await command.ExecuteNonQueryAsync();
                            }
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw new Exception("Error al insertar: ", ex);
                        }
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


        public async Task<List<ReporteDTO>> GetReportePorPeriodoAsync(string periodo)
        {
            var dtos = new List<ReporteDTO>();
            int pageSize = 500; // Tamaño de cada página
            int offset = 0;      // Inicio de los registros a consultar

            //--- Obtener PERIODO y CONSECUTIVO    $"{item.periodo}, {item.consecutivo}-{item.identificador}"
            string[] arrayP = periodo.Split(',');
            string periodoDB = arrayP[0];
            int consecutivo = 0;
            if (arrayP.Count() == 1)
                consecutivo = 0;
            else
            {
                string consecS = arrayP[1].Trim(' ');
                string[] array2P = consecS.Split("-");
                consecutivo = Convert.ToInt32(array2P[0]);
            }

            try
            {
                using (var connection = new SqlConnection(_connectionSQL))
                {
                    await connection.OpenAsync();

                    // Consulta con paginación
                    string sql = @" SELECT *, ISNULL(cantidad_segundo, -1) AS cantidad_segundo FROM reporte WHERE periodo = @Periodo ";
                    if (consecutivo != 0)
                    {
                        sql += " and periodoConsecutivo = @periodoConsecutivo ";
                    }
                    sql += "ORDER BY id OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;";


                    if(consecutivo == 0)
                    {
                        while (true)
                        {
                            // Ejecuta la consulta para una página
                            var partialData = (await connection.QueryAsync<ReporteDTO>(
                                sql,
                                new { Periodo = periodoDB, Offset = offset, PageSize = pageSize }
                            )).ToList();

                            // Si no hay más datos, rompe el bucle
                            if (!partialData.Any())
                                break;

                            // Agrega los datos a la lista principal
                            dtos.AddRange(partialData);

                            // Incrementa el offset para la siguiente página
                            offset += pageSize;
                        }
                    } else
                    {
                        while (true)
                        {
                            // Ejecuta la consulta para una página
                            var partialData = (await connection.QueryAsync<ReporteDTO>(
                                sql,
                                new { Periodo = periodoDB, periodoConsecutivo = consecutivo, Offset = offset, PageSize = pageSize }
                            )).ToList();

                            // Si no hay más datos, rompe el bucle
                            if (!partialData.Any())
                                break;

                            // Agrega los datos a la lista principal
                            dtos.AddRange(partialData);

                            // Incrementa el offset para la siguiente página
                            offset += pageSize;
                        }
                    }
                    


                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el reporte por periodo", ex);
            }

            return dtos;
        }
        public async Task UpdateReporteTodosCamposAsync(ReporteDTO obj)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionSQL))
                {
                    await connection.OpenAsync();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            string sql = "UPDATE reporte SET estado = @estado, cantidad_inicial = @cantidad_inicial, cantidad_contada = @cantidad_contada, diferencia_cantidad = @diferencia_cantidad, porcentaje_diferencia = @porcentaje_diferencia, importe_inicial = @importe_inicial, importe_contada = @importe_contada, diferencia_importe = @diferencia_importe, porcentaje_variacion_importe = @porcentaje_variacion_importe, usuario = @usuario, fecha = GETDATE()  WHERE id = @id;";

                            using (var command = new SqlCommand(sql, connection, transaction))
                            {
                                command.Parameters.AddWithValue("@estado", obj.estado);
                                command.Parameters.AddWithValue("@cantidad_inicial", obj.cantidad_inicial);
                                command.Parameters.AddWithValue("@cantidad_contada", obj.cantidad_contada);
                                command.Parameters.AddWithValue("@diferencia_cantidad", obj.diferencia_cantidad);
                                command.Parameters.AddWithValue("@porcentaje_diferencia", obj.porcentaje_diferencia);
                                command.Parameters.AddWithValue("@importe_inicial", obj.importe_inicial);
                                command.Parameters.AddWithValue("@importe_contada", obj.importe_contada);
                                command.Parameters.AddWithValue("@diferencia_importe", obj.diferencia_importe);
                                command.Parameters.AddWithValue("@porcentaje_variacion_importe", obj.porcentaje_variacion_importe);
                                command.Parameters.AddWithValue("@usuario", obj.usuario);
                                command.Parameters.AddWithValue("@id", obj.id);

                                await command.ExecuteNonQueryAsync();
                            }

                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw new Exception("Error al actualizar: ", ex);
                        }
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
        public async Task UpdateReporteAsync(ReporteDTO obj)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionSQL))
                {
                    await connection.OpenAsync();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            string sql = "UPDATE reporte SET cantidad_segundo = @cantidad_segundo, diferencia_cantidad = @diferencia_cantidad, porcentaje_diferencia = @porcentaje_diferencia, importe_inicial = @importe_inicial, importe_contada = @importe_contada, diferencia_importe = @diferencia_importe, porcentaje_variacion_importe = @porcentaje_variacion_importe WHERE id = @id;";

                            using (var command = new SqlCommand(sql, connection, transaction))
                            {
                                command.Parameters.AddWithValue("@cantidad_segundo", obj.cantidad_segundo);
                                command.Parameters.AddWithValue("@diferencia_cantidad", obj.diferencia_cantidad);
                                command.Parameters.AddWithValue("@porcentaje_diferencia", obj.porcentaje_diferencia);
                                command.Parameters.AddWithValue("@importe_inicial", obj.importe_inicial);
                                command.Parameters.AddWithValue("@importe_contada", obj.importe_contada);
                                command.Parameters.AddWithValue("@diferencia_importe", obj.diferencia_importe);
                                command.Parameters.AddWithValue("@porcentaje_variacion_importe", obj.porcentaje_variacion_importe);
                                command.Parameters.AddWithValue("@id", obj.id);

                                await command.ExecuteNonQueryAsync();
                            }

                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw new Exception("Error al actualizar: ", ex);
                        }
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
        public async Task DeleteReporteAsync(ReporteDTO obj)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionSQL))
                {
                    await connection.OpenAsync();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            string sql = "DELETE FROM reporte WHERE Id = @id;";

                            using (var command = new SqlCommand(sql, connection, transaction))
                            {
                                command.Parameters.AddWithValue("@id", obj.id);

                                await command.ExecuteNonQueryAsync();
                            }

                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw new Exception("Error al eliminar: ", ex);
                        }
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

        public async Task<List<ReporteDTO>> GetInitialLoadPendientesAsync(string periodo)
        {
            var dtos = new List<ReporteDTO>();
            int pageSize = 500; // Tamaño de cada página
            int offset = 0;      // Inicio de los registros a consultar

            //--- Obtener PERIODO y CONSECUTIVO    $"{item.periodo}, {item.consecutivo}-{item.identificador}"
            string[] arrayP = periodo.Split(',');
            string periodoDB = arrayP[0];
            int consecutivo = 0;
            if (arrayP.Count() == 1)
                consecutivo = 0;
            else
            {
                string consecS = arrayP[1].Trim(' ');
                string[] array2P = consecS.Split("-");
                consecutivo = Convert.ToInt32(array2P[0]);
            }

            try
            {
                using (var connection = new SqlConnection(_connectionSQL))
                {
                    await connection.OpenAsync();

                    string sql = @"
                SELECT 0 as id, 
                       folio, 
                       p.periodo, 
                       estado, 
                       storage_bin, 
                       storage_type, 
                       material_number, 
                       material_description as material_descripcion, 
                       unit_standard_cost, 
                       total_quantity as cantidad_inicial, 
                       0 as cantidad_contada, 
                       0 as cantidad_segundo, 
                       0 as diferencia_cantidad, 
                       0 as porcentaje_diferencia, 
                       total_cost as importe_inicial, 
                       0 as importe_contada, 
                       0 as diferencia_importe, 
                       0 as porcentaje_variacion_importe, 
                       '' as usuario, 
                       '0001-01-01 00:00:00.000' as fecha 
                FROM saldos_iniciales s 
                INNER JOIN periodo p ON p.id_periodo = s.fkPeriodo 
                WHERE periodo = @Periodo AND (estado = 'PENDIENTE' OR estado = 'AUDITADO') ";
                    
                    if(consecutivo != 0)
                    {
                        sql += " and p.consecutivo = @consecutivo ";
                    }


                sql += " ORDER BY id OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;";

                    // Ejecutamos la consulta por partes
                    if(consecutivo == 0)
                    {
                        while (true)
                        {
                            // Ejecuta la consulta para una página
                            var partialData = (await connection.QueryAsync<ReporteDTO>(
                                sql,
                                new { Periodo = periodoDB, Offset = offset, PageSize = pageSize }
                            )).ToList();

                            // Si no hay más datos, rompe el bucle
                            if (!partialData.Any())
                                break;

                            // Agrega los datos a la lista principal
                            dtos.AddRange(partialData);

                            // Incrementa el offset para la siguiente página
                            offset += pageSize;
                        }
                    } else
                    {
                        while (true)
                        {
                            // Ejecuta la consulta para una página
                            var partialData = (await connection.QueryAsync<ReporteDTO>(
                                sql,
                                new { Periodo = periodoDB, consecutivo = consecutivo, Offset = offset, PageSize = pageSize }
                            )).ToList();

                            // Si no hay más datos, rompe el bucle
                            if (!partialData.Any())
                                break;

                            // Agrega los datos a la lista principal
                            dtos.AddRange(partialData);

                            // Incrementa el offset para la siguiente página
                            offset += pageSize;
                        }
                    }
                    
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cargar los datos: ", ex);
            }

            return dtos;
        }



    }
}
