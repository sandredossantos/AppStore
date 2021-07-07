using AppStore.Domain.Entities;
using AppStore.Domain.Interfaces;
using AppStore.Service;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace AppStore.Test
{
    public class AppStoreServiceTest
    {

        [Fact(DisplayName = "Should create new user")]
        [Trait("Service", "User")]
        public async void CreateUser_ShouldCreateNewUser()
        {
            //Arrange
            Mock<IUserRepository> mock = new Mock<IUserRepository>();
            mock.Setup(m => m.Insert(It.IsAny<User>())).Returns(Task.FromResult(new User() { Id = Guid.NewGuid().ToString() }));

            User user = new User()
            {
                Name = "Sandre",
                Sex = "Male",
                TaxNumber = "45966089499",
                BirthDate = DateTime.Now,
                CreationDate = DateTime.Now,
                Address = new Address
                {
                    Country = "Brasil",
                    State = "SP",
                    City = "Poá",
                    Neighborhood = "Jardim Medina",
                    ZipCode = "08556360",
                    Street = "Rua Visconde de Aguiar Toledo",
                    Number = "120"
                }
            };

            //Act
            var result = await new UserService(mock.Object).CreateUser(user);

            //Assert
            Assert.NotNull(result.Id);
        }

        [Fact(DisplayName = "Should search user by taxnumber")]
        [Trait("Service", "User")]
        public async void GetByTaxNumber_ShouldSearchUserByTaxNumber()
        {
            //Arrange
            Mock<IUserRepository> mock = new Mock<IUserRepository>();
            mock.Setup(m => m.GetByTaxNumber(It.IsAny<string>())).Returns(Task.FromResult(
                new User()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Sandre",
                    Sex = "Male",
                    TaxNumber = "45966089499",
                    BirthDate = DateTime.Now,
                    CreationDate = DateTime.Now,
                    Address = new Address
                    {
                        Country = "Brasil",
                        State = "SP",
                        City = "Poá",
                        Neighborhood = "Jardim Medina",
                        ZipCode = "08556360",
                        Street = "Rua Visconde de Aguiar Toledo",
                        Number = "120"
                    }
                }));

            string taxNumber = "45906640894";

            //Act
            var result = await new UserService(mock.Object).GetByTaxNumber(taxNumber);

            //Assert
            Assert.NotNull(result);
        }

        [Fact(DisplayName = "Should create payment order")]
        [Trait("Service", "Purchase")]
        public async void CreatePurchaseOrder_ShouldCreatePaymentOrder()
        {
            //Arrange
            Mock<IPurchaseRepository> mock = new Mock<IPurchaseRepository>();
            mock.Setup(m => m.Insert(It.IsAny<Purchase>())).Returns(Task.FromResult(new Purchase() { Id = Guid.NewGuid().ToString(), Status = "Created" }));

            Purchase purchase = new Purchase()
            {
                TaxNumber = "45906689066",
                Code = "909",
                CardNumber = "0000-0000-0000-0000",
                ValidThru = "09/25",
                SecurityCode = 123
            };

            //Act
            var result = await new PurchaseService(mock.Object).CreatePurchaseOrder(purchase);

            //Assert
            Assert.True(result.Status == "Created");
        }

        [Fact(DisplayName = "Should search payment by Id")]
        [Trait("Service", "Purchase")]
        public void GetById_ShouldSearchPaymentById()
        {
            //Arrange
            Mock<IPurchaseRepository> mock = new Mock<IPurchaseRepository>();
            mock.Setup(m => m.GetById(It.IsAny<string>())).Returns((
                new Purchase()
                {
                    Id = Guid.NewGuid().ToString(),
                    Status = "Created",
                    TaxNumber = "45906689066",
                    Code = "909",
                    CardNumber = "0000-0000-0000-0000",
                    ValidThru = "09/25",
                    SecurityCode = 123
                }));

            string id = Guid.NewGuid().ToString();

            //Act
            var result = new PurchaseService(mock.Object).GetById(id);

            //Assert
            Assert.NotNull(result);
        }

        [Fact(DisplayName = "Should search all apps")]
        [Trait("Service", "Application")]
        public void GetAllApps_ShouldSearchAllApps()
        {
            //Arrange
            Mock<IApplicationRepository> mock = new Mock<IApplicationRepository>();
            mock.Setup(m => m.GetAll()).Returns(Task.FromResult((
                new List<Application>() 
                {
                    new Application()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Code = "001",
                        Name = "Application A",
                        Value = 12.00M
                    }, 
                    new Application()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Code = "002",
                        Name = "Application B",
                        Value = 13.00M
                    }
                })));

            string id = Guid.NewGuid().ToString();

            //Act
            var result = new ApplicationService(mock.Object).GetAllApps();

            //Assert
            Assert.NotNull(result);
            Assert.True(result.Result.Count > 0);
        }

        [Fact(DisplayName = "Should search by code")]
        [Trait("Service", "Application")]
        public void GetByCode_ShouldSearchByCode()
        {
            //Arrange
            Mock<IApplicationRepository> mock = new Mock<IApplicationRepository>();
            mock.Setup(m => m.GetByCode(It.IsAny<string>())).Returns(Task.FromResult(
                new Application()
                {
                    Id = Guid.NewGuid().ToString(),
                    Code = "001",
                    Name = "Application A",
                    Value = 12.00M
                }));

            string code = "001";

            //Act
            var result = new ApplicationService(mock.Object).GetByCode(code);

            //Assert
            Assert.NotNull(result);
            Assert.True(result.Result.Code == code);
        }

        [Fact(DisplayName = "Should registered new app")]
        [Trait("Service", "Application")]
        public void RegisterApp_ShouldRegisteredNewApp()
        {
            //Arrange
            Mock<IApplicationRepository> mock = new Mock<IApplicationRepository>();
            mock.Setup(m => m.Insert(It.IsAny<Application>())).Returns(Task.FromResult(
                new Application()
                {
                    Id = Guid.NewGuid().ToString(),
                    Code = "001",
                    Name = "Application A",
                    Value = 12.00M
                }));

            Application application = new Application()
            {
                Id = Guid.NewGuid().ToString(),
                Code = "001",
                Name = "Application A",
                Value = 12.00M
            };

            //Act
            var result = new ApplicationService(mock.Object).RegisterApp(application);

            //Assert
            Assert.NotNull(result);            
        }
    }
}