using BackendApi.Entities;
using BackendApi.Utils;
using Microsoft.Extensions.Options;
using Npgsql;
using Dapper;

namespace BackendApi.Repositories;

public interface IDistrictRepository
{
    Task<IEnumerable<DistrictEntity>> GetAll();
    Task<DistrictEntity> GetById(int id);

    Task AddPrimarySalesPerson(int districtId, int salesPersonId);

}

public class DistrictRepository : IDistrictRepository
{
    private readonly NpgsqlConnection connection;

    public DistrictRepository(IOptions<DbSettings> databaseSettings)
    {
        connection = new NpgsqlConnection(databaseSettings.Value.ConnectionString);
        connection.Open();
    }

    public async Task<IEnumerable<DistrictEntity>> GetAll()
    {
        return await connection.QueryAsync<DistrictEntity>("SELECT * FROM District ORDER BY id");
    }

    public async Task<DistrictEntity> GetById(int id)
    {
        var sql = "SELECT * FROM district WHERE id = @id";
        var values = new { id };

        return await connection.QueryFirstAsync<DistrictEntity>(sql, values);
    }

    public async Task AddPrimarySalesPerson(int districtId, int salesPersonId)
    {
        var sql = "UPDATE district SET primary_salesperson_id = @sid WHERE id = @did";
        var values = new { sid = salesPersonId, did = districtId };

        await connection.ExecuteAsync(sql, values);
    }
}
