using System;
using System.Dynamic;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Sat.Recruitment.Api;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Api.Data;
using Xunit;

namespace Sat.Recruitment.Test
{

    public class SetupFixture : IDisposable
    {
        public SetupFixture()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IListUserService, ListUserService>();
            var serviceProvider = services.BuildServiceProvider();
            ListUserService = serviceProvider.GetService<IListUserService>();
            
        }

        public void Dispose()
        {
            
        }

        public IListUserService ListUserService { get; private set; }
    }

    [CollectionDefinition("Tests collection", DisableParallelization = true)]
    public class SetupCollection : ICollectionFixture<SetupFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }

    //[CollectionDefinition("Tests", DisableParallelization = true)]
    [Collection("Tests collection")]
    public class UnitTest :  IDisposable
    {

        //protected IListUserService _listUserService;
        readonly SetupFixture fixture;
        public UnitTest(SetupFixture fixture)
        {
            this.fixture = fixture;
        }



        [Fact]
        public void CreateNormalUserOK()
        {
            
            var userController = new UsersController(this.fixture.ListUserService);

            UserDTO user = new UserDTO() { Name = "Mike", Email = "mike@gmail.com", Address = "Av. Juan G", Phone = "+349 1122354215", UserType = "Normal", Money = 124 };
            var result = userController.Create(user);
            var objectResult = result as ObjectResult;
            Assert.Equal(201, objectResult.StatusCode);
            var userResult = objectResult.Value as User;
            Console.WriteLine(userResult.Money);
        }

        [Fact]
        public void CreateUserDuplicatedByEmailKO()
        {
            
            var userController = new UsersController(this.fixture.ListUserService);

            UserDTO user = new UserDTO() { Name = "Agustina", Email = "Agustina@gmail.com", Address = "Av. Juan G", Phone = "+349 1122354215", UserType = "Normal", Money = 124.0M };
            var result = userController.Create(user);
            var objectResult = result as ObjectResult;
            Assert.Equal(400, objectResult.StatusCode);
            Assert.Equal("The user is duplicated", objectResult.Value);

        }
        [Fact]
        public void CreateUserDuplicatedByPhoneKO()
        {

            var userController = new UsersController(this.fixture.ListUserService);

            UserDTO user = new UserDTO() { Name = "Agustina", Email = "Agustina2@gmail.com", Address = "Av. Juan G", Phone = "+534645213542", UserType = "Normal", Money = 124.0M };
            var result = userController.Create(user);
            var objectResult = result as ObjectResult;
            Assert.Equal(400, objectResult.StatusCode);
            Assert.Equal("The user is duplicated", objectResult.Value);
        }

        [Fact]
        public void CreateUserDuplicatedByNameAndAddressKO()
        {

            var userController = new UsersController(this.fixture.ListUserService);

            UserDTO user = new UserDTO() { Name = "Agustina", Email = "Agustina2@gmail.com", Address = "Garay y Otra Calle", Phone = "+534645213542", UserType = "Normal", Money = 124.0M };
            var result = userController.Create(user);
            var objectResult = result as ObjectResult;
            Assert.Equal(400, objectResult.StatusCode);
            Assert.Equal("The user is duplicated", objectResult.Value);
        }
        [Fact]
        public void CreateUserWithoutNameKO()
        {
            var userController = new UsersController(this.fixture.ListUserService);
            UserDTO user = new UserDTO() { Email = "Agustina@gmail.com", Address = "Av. Juan G", Phone = "+349 1122354215", UserType = "Normal", Money = 124.0M };
            userController.ModelState.AddModelError("Name", "The name is required");
            var result = userController.Create(user);
            var objectResult = result as ObjectResult;
            Assert.Equal("The name is required", objectResult.Value);

        }
        [Fact]
        public void CheckBalanceNormalUserMore100OK()
        {
            var userController = new UsersController(this.fixture.ListUserService);
            
            UserDTO user = new UserDTO() { Name = "HGari_001", Email = "hgar_001i@gmail.com", Address = "Monroe_001", Phone = "+54911001", UserType = "Normal", Money = 1000 };
            var result = userController.Create(user);
            var objectResult = result as ObjectResult;
            Assert.Equal(201, objectResult.StatusCode);
            var userResult = objectResult.Value as User;
            Assert.Equal(1120,userResult.Money);
        }
        [Fact]
        public void CheckBalanceNormalUserLess100OK()
        {
            var userController = new UsersController(this.fixture.ListUserService);

            UserDTO user = new UserDTO() { Name = "HGari_002", Email = "hgar_002i@gmail.com", Address = "Monroe_002", Phone = "+54911002", UserType = "Normal", Money = 90 };
            var result = userController.Create(user);
            var objectResult = result as ObjectResult;
            Assert.Equal(201, objectResult.StatusCode);
            var userResult = objectResult.Value as User;
            Assert.Equal(162, userResult.Money);
        }

        [Fact]
        public void CheckBalanceSuperUserMore100OK()
        {
            var userController = new UsersController(this.fixture.ListUserService);

            UserDTO user = new UserDTO() { Name = "HGari_003", Email = "hgar_003i@gmail.com", Address = "Monroe_003", Phone = "+54911003", UserType = "SuperUser", Money = 1000 };
            var result = userController.Create(user);
            var objectResult = result as ObjectResult;
            Assert.Equal(201, objectResult.StatusCode);
            var userResult = objectResult.Value as User;
            Assert.Equal(1200, userResult.Money);

        }

        [Fact]
        public void CheckBalancePremiumMore100OK()
        {
            var userController = new UsersController(this.fixture.ListUserService);

            UserDTO user = new UserDTO() { Name = "HGari_004", Email = "hgar_004i@gmail.com", Address = "Monroe_004", Phone = "+54911004", UserType = "Premium", Money = 1000 };
            var result = userController.Create(user);
            var objectResult = result as ObjectResult;
            Assert.Equal(201, objectResult.StatusCode);
            var userResult = objectResult.Value as User;
            Assert.Equal(3000, userResult.Money);
        }

        

        public void Dispose()
        {
            
        }
    }
}
