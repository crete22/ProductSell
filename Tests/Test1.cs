using Microsoft.VisualStudio.TestTools.UnitTesting;
using Product_sell;
using System.Linq;

namespace Tests
{
    [TestClass]
    public class DatabaseFacadeTests
    {
        [TestMethod]
        public void AddEntity_AddsEntityToDatabase()
        {
            // Arrange
            var facade = new DatabaseFacade();
            var product = new Product { Name = "Test", Price = 100 };

            // Act
            facade.AddEntity(product);

            // Assert
            using (var context = new ApplicationDbContext())
            {
                Assert.IsTrue(context.Products.Any(p => p.Name == "Test" && p.Price == 100));
            }
        }

        [TestMethod]
        public void UpdateEntity_UpdatesEntityInDatabase()
        {
            // Arrange
            var facade = new DatabaseFacade();
            var product = new Product { Name = "Old", Price = 100 };
            facade.AddEntity(product);

            // Act
            product.Name = "New";
            facade.UpdateEntity(product);

            // Assert
            using (var context = new ApplicationDbContext())
            {
                Assert.IsTrue(context.Products.Any(p => p.Name == "New"));
            }
        }

        [TestMethod]
        public void DeleteEntity_RemovesEntityFromDatabase()
        {
            // Arrange
            var facade = new DatabaseFacade();
            var product = new Product { Name = "ToDelete", Price = 100 };
            facade.AddEntity(product);

            // Act
            facade.DeleteEntity(product);

            // Assert
            using (var context = new ApplicationDbContext())
            {
                Assert.IsFalse(context.Products.Any(p => p.Name == "ToDelete"));
            }
        }
    }
}
