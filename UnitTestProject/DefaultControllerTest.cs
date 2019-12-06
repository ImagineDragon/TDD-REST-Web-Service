using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TDD_REST_Web_Service.Controllers;
using TDD_REST_Web_Service.Models;

namespace DefaultControllerTests
{
    public class DefaultControllerTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task InsertRandomData()
        {
            //Arrange
            var dbSet = Substitute.For<DbSet<DefaultModel>>();
            var context = Substitute.For<DefaultContext>();

            context.DefaultModels = dbSet;

            var controller = new DefaultController(context);

            //Act
            await controller.GenerateData();

            //Assert
            dbSet.Received(1).Add(Arg.Any<DefaultModel>());
            await context.Received(1).SaveChangesAsync();
        }

        [Test]
        public void GetAllData()
        {
            //Arrange
            var data = new List<DefaultModel>()
            {
                new DefaultModel {Id = Guid.NewGuid(), Field1 = "default", Field2 = 1, Field3 = "default2", Field4 = 2},
                new DefaultModel {Id = Guid.NewGuid(), Field1 = "default", Field2 = 2, Field3 = "default2", Field4 = 3}
            };

            var dbSet = NSubstituteUtils.CreateMockDbSet(data);
            var context = Substitute.For<DefaultContext>();

            context.DefaultModels = dbSet;

            var controller = new DefaultController(context);

            //Act
            var result = controller.GetAllData().ToList();

            //Assert
            Assert.AreEqual(data, result);
        }

        [Test]
        public async Task GetDataById_NotFoundResultAsync()
        {
            //Arrange
            var data = new List<DefaultModel>()
            {
                new DefaultModel {Id = Guid.NewGuid(), Field1 = "default", Field2 = 1, Field3 = "default2", Field4 = 2},
                new DefaultModel {Id = Guid.NewGuid(), Field1 = "default", Field2 = 2, Field3 = "default2", Field4 = 3}
            };

            var dbSet = NSubstituteUtils.CreateMockDbSet(data);
            var context = Substitute.For<DefaultContext>();

            context.DefaultModels = dbSet;

            var controller = new DefaultController(context);

            //Act
            var result = await controller.GetDataByIdAsync(Guid.Empty);

            //Assert
            Assert.IsInstanceOf(typeof(NotFoundResult), result);
        }

        [Test]
        public async Task GetDataByIdAsync()
        {
            //Arrange
            var data = new List<DefaultModel>()
            {
                new DefaultModel {Id = Guid.NewGuid(), Field1 = "default", Field2 = 1, Field3 = "default2", Field4 = 2},
                new DefaultModel {Id = Guid.NewGuid(), Field1 = "default", Field2 = 2, Field3 = "default2", Field4 = 3}
            };

            var dbSet = NSubstituteUtils.CreateMockDbSet(data);
            var context = Substitute.For<DefaultContext>();

            context.DefaultModels = dbSet;

            var controller = new DefaultController(context);

            //Act
            var result = await controller.GetDataByIdAsync(data[1].Id);

            //Assert
            Assert.IsInstanceOf(typeof(OkObjectResult), result);
            var resultValue = result as OkObjectResult;
            Assert.AreEqual(data[1], resultValue.Value);
        }

        [Test]
        public async Task UpdateDataById_ModelStateError()
        {
            //Arrange
            var data = new List<DefaultModel>()
            {
                new DefaultModel {Id = Guid.NewGuid(), Field1 = "default", Field2 = 1, Field3 = "default2", Field4 = 2},
                new DefaultModel {Id = Guid.NewGuid(), Field1 = "default", Field2 = 2, Field3 = "default2", Field4 = 3}
            };

            var dbSet = NSubstituteUtils.CreateMockDbSet(data);
            var context = Substitute.For<DefaultContext>();

            context.DefaultModels = dbSet;

            var controller = new DefaultController(context);

            controller.ModelState.AddModelError("test", "Error");

            var newValue = new DefaultModel { Id = data[0].Id, Field1 = "default", Field2 = 5, Field3 = "default2", Field4 = 7 };

            //Act
            var result = await controller.UpdateDataByIdAsync(newValue);

            //Assert
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), result);
        }
        
        [Test]
        public async Task UpdateDataById_NotFoundError()
        {
            //Arrange
            var data = new List<DefaultModel>()
            {
                new DefaultModel {Id = Guid.NewGuid(), Field1 = "default", Field2 = 1, Field3 = "default2", Field4 = 2},
                new DefaultModel {Id = Guid.NewGuid(), Field1 = "default", Field2 = 2, Field3 = "default2", Field4 = 3}
            };

            var dbSet = NSubstituteUtils.CreateMockDbSet(data);
            var context = Substitute.For<DefaultContext>();

            context.DefaultModels = dbSet;

            var controller = new DefaultController(context);

            var newValue = new DefaultModel { Id = Guid.NewGuid(), Field1 = "default", Field2 = 5, Field3 = "default2", Field4 = 7 };

            //Act
            var result = await controller.UpdateDataByIdAsync(newValue);

            //Assert
            Assert.IsInstanceOf(typeof(NotFoundResult), result);
        }

        [Test]
        public async Task UpdateDataById()
        {
            //Arrange
            var data = new List<DefaultModel>()
            {
                new DefaultModel {Id = Guid.NewGuid(), Field1 = "default", Field2 = 1, Field3 = "default2", Field4 = 2},
                new DefaultModel {Id = Guid.NewGuid(), Field1 = "default", Field2 = 2, Field3 = "default2", Field4 = 3}
            };

            var dbSet = NSubstituteUtils.CreateMockDbSet(data);
            var context = Substitute.For<DefaultContext>();

            context.DefaultModels = dbSet;

            var controller = new DefaultController(context);

            var newValue = new DefaultModel { Id = data[0].Id, Field1 = "default", Field2 = 5, Field3 = "default2", Field4 = 7 };

            //Act
            var result = await controller.UpdateDataByIdAsync(newValue);

            //Assert
            Assert.IsInstanceOf(typeof(NoContentResult), result);
            dbSet.Received(1).Update(Arg.Any<DefaultModel>());
            await context.Received(1).SaveChangesAsync();
        }

        [Test]
        public async Task DeleteDataById_NotFoundError()
        {
            //Arrange
            var data = new List<DefaultModel>()
            {
                new DefaultModel {Id = Guid.NewGuid(), Field1 = "default", Field2 = 1, Field3 = "default2", Field4 = 2},
                new DefaultModel {Id = Guid.NewGuid(), Field1 = "default", Field2 = 2, Field3 = "default2", Field4 = 3}
            };

            var dbSet = NSubstituteUtils.CreateMockDbSet(data);
            var context = Substitute.For<DefaultContext>();

            context.DefaultModels = dbSet;

            var controller = new DefaultController(context);

            //Act
            var result = await controller.DeleteDataById(Guid.NewGuid());

            //Assert
            Assert.IsInstanceOf(typeof(NotFoundResult), result);
        }

        [Test]
        public async Task DeleteDataById()
        {
            //Arrange
            var data = new List<DefaultModel>()
            {
                new DefaultModel {Id = Guid.NewGuid(), Field1 = "default", Field2 = 1, Field3 = "default2", Field4 = 2},
                new DefaultModel {Id = Guid.NewGuid(), Field1 = "default", Field2 = 2, Field3 = "default2", Field4 = 3}
            };

            var dbSet = NSubstituteUtils.CreateMockDbSet(data);
            var context = Substitute.For<DefaultContext>();

            context.DefaultModels = dbSet;

            var controller = new DefaultController(context);

            //Act
            var result = await controller.DeleteDataById(data[0].Id);

            //Assert
            Assert.IsInstanceOf(typeof(OkObjectResult), result);
            dbSet.Received(1).Remove(Arg.Any<DefaultModel>());
            await context.Received(1).SaveChangesAsync();
        }
    }
}