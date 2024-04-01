using BackendApi.Utils;
using Microsoft.Extensions.Options;
using Npgsql;
using Dapper;

namespace BackendApi.Repositories;

public interface IDistrictSalesPersonRepository
{
    Task AddSecondarySalesPerson(int districtId, int salesPersonId);
    Task RemoveSecondarySalesPerson(int districtId, int salesPersonId);
}

public class DistrictSalesPersonRepository : IDistrictSalesPersonRepository
{
    private readonly NpgsqlConnection connection;
    private readonly ILogger<SalesPersonRepository> _logger;

    public DistrictSalesPersonRepository(IOptions<DbSettings> databaseSettings, ILogger<SalesPersonRepository> logger)
    {
        _logger = logger;
        connection = new NpgsqlConnection(databaseSettings.Value.ConnectionString);
        connection.Open();
    }

    public async Task AddSecondarySalesPerson(int districtId, int salesPersonId)
    {
        _logger.LogInformation("AddSecondarySalesPerson districtId: {0} salesPersonId: {1}", districtId, salesPersonId);

        string sql = "INSERT INTO district_salesperson (salesperson_id, district_id) VALUES (@sid, @did)";
        var values = new { sid = salesPersonId, did = districtId };

        try
        {
            await connection.ExecuteAsync(sql, values);
        }
        catch (PostgresException ex)
        {
            // Log the exception so we can see what it wrong with it
            _logger.LogError("SQL Exception: {0}", ex);

            if (ex.SqlState == "23505")
            {   // unique_violation - we tried to add a salesperson as secondary, but they were already there
                throw new AlreadyExistsException();
            }
            else if (ex.SqlState == "23503")
            {   // foreign_key_violation - we tried to add a salesperson/district that doesn't exist
                throw new NotFoundException();
            }

            throw new UnknownErrorException();
        }

    }

    public async Task RemoveSecondarySalesPerson(int districtId, int salesPersonId)
    {
        _logger.LogInformation("RemoveSecondarySalesPerson districtId: {0} salesPersonId: {1}", districtId, salesPersonId);

        var sql = "DELETE FROM district_salesperson WHERE district_id=(@did) AND salesperson_id=(@sid)";
        var values = new { did = districtId, sid = salesPersonId };
        try
        {
            var result = await connection.ExecuteAsync(sql, values);
            _logger.LogError("RemoveSecondarySalesPerson result: {0}", result);
            if (result == 0)
            {
                // If we didn't delete anything we must have specified something that didn't exist
                throw new NotFoundException();
            }

        }
        catch (PostgresException ex)
        {
            // Log the exception so we can see what is wrong with it
            _logger.LogError("Caught the exception: {0}", ex);

            // unique_violation
            if (ex.SqlState == "23505")
            {
                throw new AlreadyExistsException();
            }

            throw new UnknownErrorException();
        }
    }
}
