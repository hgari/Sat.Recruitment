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
            listUserService = serviceProvider.GetService<IListUserService>();
            
        }

        public void Dispose()
        {
            
        }

        public IListUserService listUserService { get; private set; }
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
        SetupFixture fixture;
        public UnitTest(SetupFixture fixture)
        {
            this.fixture = fixture;
        }



        [Fact]
        public void CreateNormalUserOK()
        {
            
            var userController = new UsersController(this.fixture.listUserService);

            UserDTO user = new UserDTO() { Name = "Mike", Email = "mike@gmail.com", Address = "Av. Juan G", Phone = "+349 1122354215", UserType = "Normal", Money = 124 };
            var result = userController.Create(user);
            var objectResult = result as ObjectResult;
            Assert.Equal(201, objectResult.StatusCode);
            var userResp = objectResult.Value as User;
            Console.WriteLine(userResp.Money);
        }

        [Fact]
        public void CreateUserDuplicatedByEmailKO()
        {
            
            var userController = new UsersController(this.fixture.listUserService);

            UserDTO user = new UserDTO() { Name = "Agustina", Email = "Agustina@gmail.com", Address = "Av. Juan G", Phone = "+349 1122354215", UserType = "Normal", Money = 124.0M };
            var result = userController.Create(user);
            var objectResult = result as ObjectResult;
            Assert.Equal(400, objectResult.StatusCode);
            Assert.Equal("The user is duplicated", objectResult.Value);

        }
        [Fact]
        public void CreateUserDuplicatedByPhoneKO()
        {

            var userController = new UsersController(this.fixture.listUserService);

            UserDTO user = new UserDTO() { Name = "Agustina", Email = "Agustina2@gmail.com", Address = "Av. Juan G", Phone = "+534645213542", UserType = "Normal", Money = 124.0M };
            var result = userController.Create(user);
            var objectResult = result as ObjectResult;
            Assert.Equal(400, objectResult.StatusCode);
            Assert.Equal("The user is duplicated", objectResult.Value);
        }

        [Fact]
        public void CreateUserDuplicatedByNameAndAddressKO()
        {

            var userController = new UsersController(this.fixture.listUserService);

            UserDTO user = new UserDTO() { Name = "Agustina", Email = "Agustina2@gmail.com", Address = "Garay y Otra Calle", Phone = "+534645213542", UserType = "Normal", Money = 124.0M };
            var result = userController.Create(user);
            var objectResult = result as ObjectResult;
            Assert.Equal(400, objectResult.StatusCode);
            Assert.Equal("The user is duplicated", objectResult.Value);
        }
        [Fact]
        public void CreateUserWithoutNameKO()
        {
            var userController = new UsersController(this.fixture.listUserService);
            UserDTO user = new UserDTO() { Email = "Agustina@gmail.com", Address = "Av. Juan G", Phone = "+349 1122354215", UserType = "Normal", Money = 124.0M };
            userController.ModelState.AddModelError("Name", "The name is required");
            var result = userController.Create(user);
            var objectResult = result as ObjectResult;
            Assert.Equal("The name is required", objectResult.Value);

        }
        [Fact]
        public void Test4()
        {
            var userController = new UsersController(this.fixture.listUserService);
            
            UserDTO user = new UserDTO() { Name = "HGari", Email = "hgari@gmail.com", Address = "Monroe", Phone = "+5491132319984", UserType = "Normal", Money = 124 };
            var result = userController.Create(user);
            var objectResult = result as ObjectResult;
            Assert.Equal(201, objectResult.StatusCode);

            


        }
        [Fact]
        public void Test5()
        {
            var userController = new UsersController(this.fixture.listUserService);
            UserDTO user = new UserDTO() { Name = "LGari", Email = "lolo@gmail.com", Address = "Monroe", Phone = "+5491132319983",UserType="Normal",Money=124.0M };
            var result = userController.Create(user);
            var objectResult = result as ObjectResult;
            Assert.Equal(201, objectResult.StatusCode);

        }

        [Fact]
        public void Test6()
        {
            var userController = new UsersController(this.fixture.listUserService);
            UserDTO user = new UserDTO();
            user.Name = "ale";
            user.Email = "ale@mail.com";
            user.Address = "madrid";
            var result = userController.Create(user);
            var objectResult = result as ObjectResult;
            Assert.Equal(201, objectResult.StatusCode);
        }

        public void Dispose()
        {
            
        }
    }
}
