using BackendApi.Entities;
using BackendApi.Utils;
using Microsoft.Extensions.Options;
using Npgsql;
using Dapper;

namespace BackendApi.Repositories;

public interface ISalesPersonRepository
{
    public Task<IEnumerable<SalesPersonEntity>> GetAll();
    public Task<IEnumerable<SalesPersonEntity>> GetAllByDistrictId(int id);
}

public class SalesPersonRepository : ISalesPersonRepository
{
    private readonly NpgsqlConnection connection;
    private readonly ILogger<SalesPersonRepository> _logger;

    public SalesPersonRepository(IOptions<DbSettings> databaseSettings, ILogger<SalesPersonRepository> logger)
    {
        _logger = logger;
        connection = new NpgsqlConnection(databaseSettings.Value.ConnectionString);
        connection.Open();
    }

    public async Task<IEnumerable<SalesPersonEntity>> GetAll()
    {
        return await connection.QueryAsync<SalesPersonEntity>($"SELECT * FROM salesperson");
    }


    public async Task<IEnumerable<SalesPersonEntity>> GetAllByDistrictId(int id)
    {
        var sql = $"""
            SELECT s.id as id, s.name as name, true as is_primary
            FROM district d
            INNER JOIN salesperson s ON s.id = d.primary_salesperson_id
            WHERE d.id = @id
            UNION
            SELECT s.id as id, s.name as name, false as is_primary
            FROM salesperson s
            INNER JOIN district_salesperson d ON s.id = d.salesperson_id
            WHERE d.district_id = @id       
            ORDER BY id      
        """;
        var values = new { id };

        return await connection.QueryAsync<SalesPersonEntity>(sql, values);
    }
}
