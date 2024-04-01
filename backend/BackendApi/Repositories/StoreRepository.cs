using BackendApi.Entities;
using BackendApi.Utils;
using Microsoft.Extensions.Options;
using Npgsql;
using Dapper;

namespace BackendApi.Repositories;

public interface IStoreRepository
{
    public Task<IEnumerable<StoreEntity>> GetAll();
    public Task<IEnumerable<StoreEntity>> GetAllStoresForDistrict(int id);
}

public class StoreRepository : IStoreRepository
{
    private readonly NpgsqlConnection connection;

    public StoreRepository(IOptions<DbSettings> databaseSettings)
    {
        connection = new NpgsqlConnection(databaseSettings.Value.ConnectionString);
        connection.Open();
    }

    public async Task<IEnumerable<StoreEntity>> GetAll()
    {
        return await connection.QueryAsync<StoreEntity>("SELECT * FROM store");
    }

    public async Task<IEnumerable<StoreEntity>> GetAllStoresForDistrict(int id)
    {
        var sql = "SELECT * FROM store WHERE district_id = @id";
        var values = new { id };

        return await connection.QueryAsync<StoreEntity>(sql, values);
    }
}
