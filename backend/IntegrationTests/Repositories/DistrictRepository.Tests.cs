using Testcontainers.PostgreSql;
using BackendApi.Repositories;
using BackendApi.Utils;
using Microsoft.Extensions.Options;
using NUnit.Framework;

namespace IntegrationTests;

public class Tests
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
    public async Task GetAllStores()
    {
        // Arrange
        var settings = new DbSettings { ConnectionString = postgres.GetConnectionString() };
        var opts = Options.Create(settings);
        var repo = new DistrictRepository(opts);

        // Act
        var result = await repo.GetAll();

        // Assert
        Assert.That(result.Count(), Is.EqualTo(3));
    }

    [Test]
    public async Task GetById()
    {
        // Arrange
        var settings = new DbSettings { ConnectionString = postgres.GetConnectionString() };
        var opts = Options.Create(settings);
        var repo = new DistrictRepository(opts);

        // Act
        var result = await repo.GetById(1);

        // Assert
        Assert.That(result.name, Is.EqualTo("Northern Denmark"));
    }
}

