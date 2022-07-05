using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ShopifyInventory.Controllers;
using ShopifyInventory.Data;

namespace ShopifyInventory.Tests.Controller
{
    public class InverntoryControllerTests
    {
        private readonly IServiceProvider _services = DIMocker.GetCollection().BuildServiceProvider();
        private readonly ShopifyDbContext _dbcontext;
        private readonly ILogger<InventoryController> _logger;
        public InverntoryControllerTests()
        {
            _dbcontext = _services.GetRequiredService<ShopifyDbContext>();
            _logger = _services.GetRequiredService<ILogger<InventoryController>>();
        }

        [Fact]
        public void Test1()
        {
            //Arrange
            var inventory = new InventoryController(_dbcontext, _logger);

            //Act
            var result = inventory.Index();

            //Assert
            Assert.IsType<ViewResult>(result);
        }
    }
}