using BackendApi.Models;
using BackendApi.Repositories;
using Moq;
using NUnit.Framework;

namespace BackendApi.Services.Tests;

public class SalesPersonServiceTests
{
    [TestFixture]
    public class AddingSalesPersons
    {
        private Mock<ISalesPersonRepository> mockSalesPersonRepository;
        private Mock<IDistrictRepository> mockDistrictRepository;
        private Mock<IDistrictSalesPersonRepository> mockDistrictSalesPersonRepository;//
        private SalesPersonService service;

        [SetUp]
        public void SetUp()
        {
            mockSalesPersonRepository = new Mock<ISalesPersonRepository>();
            mockDistrictRepository = new Mock<IDistrictRepository>();
            mockDistrictSalesPersonRepository = new Mock<IDistrictSalesPersonRepository>();
            service = new SalesPersonService(mockSalesPersonRepository.Object, mockDistrictRepository.Object, mockDistrictSalesPersonRepository.Object);
        }

        [Test]
        public async Task AddPrimarySalesPerson()
        {
            // Arrange 
            var req = new AddSalesPersonRequestModel { salesPersonId = 1, isPrimary = true };

            // Act
            await service.AddSalesPerson(req, 1);

            // Assert
            mockDistrictRepository.Verify(repo => repo.AddPrimarySalesPerson(1, 1), Times.AtLeastOnce());
            mockDistrictSalesPersonRepository.Verify(repo => repo.AddSecondarySalesPerson(1, 1), Times.Never());
        }

        [Test]
        public async Task AddSecondarySalesPerson()
        {
            // Arrange 
            var req = new AddSalesPersonRequestModel { salesPersonId = 1, isPrimary = false };

            // Act
            await service.AddSalesPerson(req, 1);

            // Assert
            mockDistrictRepository.Verify(repo => repo.AddPrimarySalesPerson(1, 1), Times.Never());
            mockDistrictSalesPersonRepository.Verify(repo => repo.AddSecondarySalesPerson(1, 1), Times.AtLeastOnce());
        }
    }
}


