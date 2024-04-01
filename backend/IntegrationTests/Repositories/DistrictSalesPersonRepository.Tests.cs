using Testcontainers.PostgreSql;
using BackendApi.Repositories;
using BackendApi.Utils;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using Moq;
using Microsoft.Extensions.Logging;

namespace IntegrationTests;

public class DistrictSalesPersonRepositoryTests
{
    private PostgreSqlContainer postgres;

    [OneTimeSetUp]
    public async Task Init()
    {
        postgres = new PostgreSqlBuilder().Build();
        await postgres.StartAsync().ConfigureAwait(false);

        await DbInit.DbInit.Setup(postgres);
    }

    [OneTimeTearDown]
    public async Task Cleanup()
    {
        await postgres.DisposeAsync().AsTask();
    }

    [Test]
    public void AddSecondarySalesPersonAlreadyExists()
    {
        // Arrange
        var log = new Mock<ILogger<SalesPersonRepository>>();

        var settings = new DbSettings { ConnectionString = postgres.GetConnectionString() };
        var opts = Options.Create(settings);
        var repo = new DistrictSalesPersonRepository(opts, log.Object);

        // Act
        Assert.ThrowsAsync<AlreadyExistsException>(async () =>
            await repo.AddSecondarySalesPerson(1, 3)
        );
    }

    [Test]
    public void AddSecondarySalesPersonUnknownSalesPerson()
    {
        // Arrange
        var log = new Mock<ILogger<SalesPersonRepository>>();

        var settings = new DbSettings { ConnectionString = postgres.GetConnectionString() };
        var opts = Options.Create(settings);
        var repo = new DistrictSalesPersonRepository(opts, log.Object);

        // Act
        Assert.ThrowsAsync<NotFoundException>(async () =>
            await repo.AddSecondarySalesPerson(1, 8)
        );
    }

    [Test]
    public void AddSecondarySalesPersonUnknownDistrict()
    {
        // Arrange
        var log = new Mock<ILogger<SalesPersonRepository>>();

        var settings = new DbSettings { ConnectionString = postgres.GetConnectionString() };
        var opts = Options.Create(settings);
        var repo = new DistrictSalesPersonRepository(opts, log.Object);

        // Act
        Assert.ThrowsAsync<NotFoundException>(async () =>
            await repo.AddSecondarySalesPerson(8, 3)
        );
    }

}

