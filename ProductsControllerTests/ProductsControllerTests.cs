using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Net;
using VetIt.Controllers;
using VetIt.Models;
using Xunit;

namespace ProductsControllerTests
{
    public class ProductsControllerTests
    {
        private ProductsController _productsController;

        public ProductsControllerTests()
        {
            //var options = new DbContextOptionsBuilder<ProductContext>()
            //    .UseInMemoryDatabase(databaseName: "ProductsTest")
            //    .Options;

            //using (var context = new ProductContext(options))
            //{
            //    context.Products.Add(new Product { ProductId = 1, DeleteDate = new DateTime(2019, 11, 1) });
            //    context.Products.Add(new Product { ProductId = 2, DeleteDate = new DateTime(2022, 11, 1) });
            //    context.Products.Add(new Product { ProductId = 3, DeleteDate = null });
            //    context.Products.Add(new Product { ProductId = 4, DeleteDate = null, InactiveDate = new DateTime(2019, 11, 1) });
            //    context.Products.Add(new Product { ProductId = 5, DeleteDate = null, InactiveDate = new DateTime(2022, 11, 1) });
            //    context.Products.Add(new Product { ProductId = 6, DeleteDate = new DateTime(2022, 11, 1), InactiveDate = new DateTime(2022, 11, 1) });
            //    context.Products.Add(new Product { ProductId = 7 });
            //    context.Products.Add(new Product { ProductId = 8, Dangerous = true });
            //    context.Products.Add(new Product { ProductId = 9, DeleteDate = new DateTime(2019, 11, 1), Dangerous = true });
            //    context.Products.Add(new Product { ProductId = 10, DeleteDate = new DateTime(2019, 11, 1), InactiveDate = new DateTime(2019, 11, 1), Dangerous = true });
            //    context.Products.Add(new Product { ProductId = 11, DeleteDate = new DateTime(2022, 11, 1), InactiveDate = new DateTime(2022, 11, 1), Dangerous = true });
            //    context.Products.Add(new Product { ProductId = 12, ProductDescription = "hello" });

            //    context.SaveChanges();
            //    _productsController = new ProductsController(_productContext);
            //}
        }

        [Fact]
        public async System.Threading.Tasks.Task GetActiveAndNonDeletedProducts_ReturnNothingWhenDeleteDateIsInPast()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ProductContext>()
        .UseInMemoryDatabase(databaseName: "Products Test 1")
        .Options;

            using (var context = new ProductContext(options))
            {
                context.Products.Add(new Product { ProductId = 1, DeleteDate = new DateTime(2019, 11, 1)});
                context.SaveChanges();
            }

            using (var context = new ProductContext(options))
            {

                // Act
                _productsController = new ProductsController(context);
                var response = await _productsController.GetActiveAndNonDeletedProducts();

                // Assert
                Assert.Null(response.Value);
            }
        }

        [Fact]
        public async System.Threading.Tasks.Task GetActiveAndNonDeletedProducts_ReturnRecordWhenDeleteDateIsNull()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ProductContext>()
        .UseInMemoryDatabase(databaseName: "Products Test 2")
        .Options;

            using (var context = new ProductContext(options))
            {
                context.Products.Add(new Product { ProductId = 1, DeleteDate = null});
                context.SaveChanges();
            }

            using (var context = new ProductContext(options))
            {

                // Act
                _productsController = new ProductsController(context);
                var response = await _productsController.GetActiveAndNonDeletedProducts();

                // Assert
                Assert.NotNull(response.Value);
                Assert.Single(response.Value);
            }
        }

        [Fact]
        public async System.Threading.Tasks.Task GetActiveAndNonDeletedProducts_ReturnRecordWhenDeleteDateHasNotPast()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ProductContext>()
        .UseInMemoryDatabase(databaseName: "Products Test 3")
        .Options;

            using (var context = new ProductContext(options))
            {
                context.Products.Add(new Product { ProductId = 1, DeleteDate = new DateTime(2022, 11, 1) });
                context.SaveChanges();
            }

            using (var context = new ProductContext(options))
            {

                // Act
                _productsController = new ProductsController(context);
                var response = await _productsController.GetActiveAndNonDeletedProducts();

                // Assert
                Assert.NotNull(response.Value);
                Assert.Single(response.Value);
            }
        }

        [Fact]
        public async System.Threading.Tasks.Task GetActiveAndNonDeletedProducts_ReturnNothingWhenInactiveDateIsInPast()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ProductContext>()
        .UseInMemoryDatabase(databaseName: "Products Test 4")
        .Options;

            using (var context = new ProductContext(options))
            {
                context.Products.Add(new Product { ProductId = 4, DeleteDate = null, InactiveDate = new DateTime(2019, 11, 1) });
                context.SaveChanges();
            }

            using (var context = new ProductContext(options))
            {

                // Act
                _productsController = new ProductsController(context);
                var response = await _productsController.GetActiveAndNonDeletedProducts();

                // Assert
                Assert.Null(response.Value);
            }
        }

        [Fact]
        public async System.Threading.Tasks.Task GetActiveAndNonDeletedProducts_ReturnRecordWhenInactiveDateHasNotPast()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ProductContext>()
        .UseInMemoryDatabase(databaseName: "Products Test 5")
        .Options;

            using (var context = new ProductContext(options))
            {
                context.Products.Add(new Product { ProductId = 5, DeleteDate = null, InactiveDate = new DateTime(2022, 11, 1) });
                context.SaveChanges();
            }

            using (var context = new ProductContext(options))
            {

                // Act
                _productsController = new ProductsController(context);
                var response = await _productsController.GetActiveAndNonDeletedProducts();

                // Assert
                Assert.NotNull(response.Value);
                Assert.Single(response.Value);
            }
        }

        [Fact]
        public async System.Threading.Tasks.Task GetActiveAndNonDeletedProducts_ReturnRecordWhenInactiveDateIsNull()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ProductContext>()
        .UseInMemoryDatabase(databaseName: "Products Test 6")
        .Options;

            using (var context = new ProductContext(options))
            {
                context.Products.Add(new Product { ProductId = 6, DeleteDate = null, InactiveDate = null});
                context.SaveChanges();
            }

            using (var context = new ProductContext(options))
            {

                // Act
                _productsController = new ProductsController(context);
                var response = await _productsController.GetActiveAndNonDeletedProducts();

                // Assert
                Assert.NotNull(response.Value);
                Assert.Single(response.Value);
            }
        }

        [Fact]
        public async System.Threading.Tasks.Task GetActiveAndNonDeletedProducts_ReturnRecordWhenNeitherDateHasComeToPass()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ProductContext>()
        .UseInMemoryDatabase(databaseName: "Products Test 7")
        .Options;

            using (var context = new ProductContext(options))
            {
                context.Products.Add(new Product {ProductId = 7, DeleteDate = new DateTime(2022, 11, 1), InactiveDate = new DateTime(2022, 11, 1) });
                context.SaveChanges();
            }

            using (var context = new ProductContext(options))
            {

                // Act
                _productsController = new ProductsController(context);
                var response = await _productsController.GetActiveAndNonDeletedProducts();

                // Assert
                Assert.NotNull(response.Value);
                Assert.Single(response.Value);
            }
        }

        [Fact]
        public async System.Threading.Tasks.Task GetActiveAndNonDeletedProducts_ReturnRecordWhenBothFieldsAreNull()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ProductContext>()
        .UseInMemoryDatabase(databaseName: "Products Test 8")
        .Options;

            using (var context = new ProductContext(options))
            {
                context.Products.Add(new Product { ProductId = 8 });
                context.SaveChanges();
            }

            using (var context = new ProductContext(options))
            {

                // Act
                _productsController = new ProductsController(context);
                var response = await _productsController.GetActiveAndNonDeletedProducts();

                // Assert
                Assert.NotNull(response.Value);
                Assert.Single(response.Value);
            }
        }

        [Fact]
        public async System.Threading.Tasks.Task GetActiveAndNonDeletedProducts_ReturnNothingWhenBothFieldDatesHavePast()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ProductContext>()
        .UseInMemoryDatabase(databaseName: "Products Test 9")
        .Options;

            using (var context = new ProductContext(options))
            {
                context.Products.Add(new Product { ProductId = 9 ,DeleteDate = new DateTime(2011, 11, 1), InactiveDate = new DateTime(2011, 11, 1) });
                context.SaveChanges();
            }

            using (var context = new ProductContext(options))
            {

                // Act
                _productsController = new ProductsController(context);
                var response = await _productsController.GetActiveAndNonDeletedProducts();

                // Assert
                Assert.Null(response.Value);
            }
        }

        [Fact]
        public async System.Threading.Tasks.Task GetActiveAndNonDeletedProducts_ReturnMultipleRecords()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ProductContext>()
        .UseInMemoryDatabase(databaseName: "Products Test 10")
        .Options;

            using (var context = new ProductContext(options))
            {
                context.Products.Add(new Product { ProductId = 10 });
                context.Products.Add(new Product { ProductId = 11 });
                context.SaveChanges();
            }

            using (var context = new ProductContext(options))
            {
                // Act
                _productsController = new ProductsController(context);
                var response = await _productsController.GetActiveAndNonDeletedProducts();

                // Assert
                Assert.NotNull(response.Value);
                Assert.Equal(2, response.Value.Count);
            }
        }

        [Fact]
        public async System.Threading.Tasks.Task GetActiveAndNonDeletedProducts_ReturnMultipleRecordsInDescendingOrderOfCreateDate()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ProductContext>()
        .UseInMemoryDatabase(databaseName: "Products Test 11")
        .Options;

            using (var context = new ProductContext(options))
            {
                context.Products.Add(new Product { ProductId = 10, CreateDate = new DateTime(2011, 11, 1) });
                context.Products.Add(new Product { ProductId = 11, CreateDate = new DateTime(2012, 11, 1) });
                context.SaveChanges();
            }

            using (var context = new ProductContext(options))
            {
                // Act
                _productsController = new ProductsController(context);
                var response = await _productsController.GetActiveAndNonDeletedProducts();

                // Assert
                Assert.NotNull(response.Value);
                Assert.Equal(2, response.Value.Count);
                Assert.Equal(new DateTime(2012, 11, 1), response.Value[0].CreateDate);
                Assert.Equal(new DateTime(2011, 11, 1), response.Value[1].CreateDate);
            }
        }






        [Fact]
        public async System.Threading.Tasks.Task GetActiveDangerousDrugs_ReturnNothingWhenDeleteDateIsInPast()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ProductContext>()
        .UseInMemoryDatabase(databaseName: "Products Test 12")
        .Options;

            using (var context = new ProductContext(options))
            {
                context.Products.Add(new Product { ProductId = 12, DeleteDate = new DateTime(2019, 11, 1), Dangerous = true });
                context.SaveChanges();
            }

            using (var context = new ProductContext(options))
            {

                // Act
                _productsController = new ProductsController(context);
                var response = await _productsController.GetActiveDangerousDrugs();

                // Assert
                Assert.Null(response.Value);
            }
        }

        [Fact]
        public async System.Threading.Tasks.Task GetActiveDangerousDrugs_ReturnRecordWhenDeleteDateIsNull()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ProductContext>()
        .UseInMemoryDatabase(databaseName: "Products Test 13")
        .Options;

            using (var context = new ProductContext(options))
            {
                context.Products.Add(new Product { ProductId = 1, DeleteDate = null, Dangerous = true  });
                context.SaveChanges();
            }

            using (var context = new ProductContext(options))
            {

                // Act
                _productsController = new ProductsController(context);
                var response = await _productsController.GetActiveDangerousDrugs();

                // Assert
                Assert.NotNull(response.Value);
                Assert.Single(response.Value);
            }
        }

        [Fact]
        public async System.Threading.Tasks.Task GetActiveDangerousDrugs_ReturnRecordWhenDeleteDateHasNotPast()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ProductContext>()
        .UseInMemoryDatabase(databaseName: "Products Test 14")
        .Options;

            using (var context = new ProductContext(options))
            {
                context.Products.Add(new Product { ProductId = 1, DeleteDate = new DateTime(2022, 11, 1), Dangerous = true });
                context.SaveChanges();
            }

            using (var context = new ProductContext(options))
            {

                // Act
                _productsController = new ProductsController(context);
                var response = await _productsController.GetActiveDangerousDrugs();

                // Assert
                Assert.NotNull(response.Value);
                Assert.Single(response.Value);
            }
        }

        [Fact]
        public async System.Threading.Tasks.Task GetActiveDangerousDrugs_ReturnNothingWhenInactiveDateIsInPast()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ProductContext>()
        .UseInMemoryDatabase(databaseName: "Products Test 15")
        .Options;

            using (var context = new ProductContext(options))
            {
                context.Products.Add(new Product { ProductId = 4, DeleteDate = null, InactiveDate = new DateTime(2019, 11, 1), Dangerous = true });
                context.SaveChanges();
            }

            using (var context = new ProductContext(options))
            {

                // Act
                _productsController = new ProductsController(context);
                var response = await _productsController.GetActiveDangerousDrugs();

                // Assert
                Assert.Null(response.Value);
            }
        }

        [Fact]
        public async System.Threading.Tasks.Task GetActiveDangerousDrugs_ReturnRecordWhenInactiveDateHasNotPast()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ProductContext>()
        .UseInMemoryDatabase(databaseName: "Products Test 16")
        .Options;

            using (var context = new ProductContext(options))
            {
                context.Products.Add(new Product { ProductId = 5, DeleteDate = null, InactiveDate = new DateTime(2022, 11, 1), Dangerous = true });
                context.SaveChanges();
            }

            using (var context = new ProductContext(options))
            {

                // Act
                _productsController = new ProductsController(context);
                var response = await _productsController.GetActiveDangerousDrugs();

                // Assert
                Assert.NotNull(response.Value);
                Assert.Single(response.Value);
            }
        }

        [Fact]
        public async System.Threading.Tasks.Task GetActiveDangerousDrugs_ReturnRecordWhenInactiveDateIsNull()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ProductContext>()
        .UseInMemoryDatabase(databaseName: "Products Test 17")
        .Options;

            using (var context = new ProductContext(options))
            {
                context.Products.Add(new Product { ProductId = 6, DeleteDate = null, InactiveDate = null, Dangerous = true });
                context.SaveChanges();
            }

            using (var context = new ProductContext(options))
            {

                // Act
                _productsController = new ProductsController(context);
                var response = await _productsController.GetActiveDangerousDrugs();

                // Assert
                Assert.NotNull(response.Value);
                Assert.Single(response.Value);
            }
        }

        [Fact]
        public async System.Threading.Tasks.Task GetActiveDangerousDrugs_ReturnRecordWhenNeitherDateHasComeToPass()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ProductContext>()
        .UseInMemoryDatabase(databaseName: "Products Test 18")
        .Options;

            using (var context = new ProductContext(options))
            {
                context.Products.Add(new Product { ProductId = 7, DeleteDate = new DateTime(2022, 11, 1), InactiveDate = new DateTime(2022, 11, 1), Dangerous = true});
                context.SaveChanges();
            }

            using (var context = new ProductContext(options))
            {

                // Act
                _productsController = new ProductsController(context);
                var response = await _productsController.GetActiveDangerousDrugs();

                // Assert
                Assert.NotNull(response.Value);
                Assert.Single(response.Value);
            }
        }

        [Fact]
        public async System.Threading.Tasks.Task GetActiveDangerousDrugs_ReturnRecordWhenBothFieldsAreNull()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ProductContext>()
        .UseInMemoryDatabase(databaseName: "Products Test 19")
        .Options;

            using (var context = new ProductContext(options))
            {
                context.Products.Add(new Product { ProductId = 8, Dangerous = true });
                context.SaveChanges();
            }

            using (var context = new ProductContext(options))
            {

                // Act
                _productsController = new ProductsController(context);
                var response = await _productsController.GetActiveDangerousDrugs();

                // Assert
                Assert.NotNull(response.Value);
                Assert.Single(response.Value);
            }
        }


        [Fact]
        public async System.Threading.Tasks.Task GetActiveDangerousDrugs_ReturnNothingIfDangerousIsFalse()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ProductContext>()
        .UseInMemoryDatabase(databaseName: "Products Test 20")
        .Options;

            using (var context = new ProductContext(options))
            {
                context.Products.Add(new Product { ProductId = 8, Dangerous = false });
                context.SaveChanges();
            }

            using (var context = new ProductContext(options))
            {

                // Act
                _productsController = new ProductsController(context);
                var response = await _productsController.GetActiveDangerousDrugs();

                // Assert
                Assert.Null(response.Value);
            }
        }

        [Fact]
        public async System.Threading.Tasks.Task GetActiveDangerousDrugs_ReturnNothingWhenBothFieldDatesHavePast()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ProductContext>()
        .UseInMemoryDatabase(databaseName: "Products Test 21")
        .Options;

            using (var context = new ProductContext(options))
            {
                context.Products.Add(new Product { ProductId = 9, DeleteDate = new DateTime(2011, 11, 1), InactiveDate = new DateTime(2011, 11, 1), Dangerous = true });
                context.SaveChanges();
            }

            using (var context = new ProductContext(options))
            {

                // Act
                _productsController = new ProductsController(context);
                var response = await _productsController.GetActiveDangerousDrugs();

                // Assert
                Assert.Null(response.Value);
            }
        }

        [Fact]
        public async System.Threading.Tasks.Task GetActiveDangerousDrugs_ReturnMultipleRecords()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ProductContext>()
        .UseInMemoryDatabase(databaseName: "Products Test 22")
        .Options;

            using (var context = new ProductContext(options))
            {
                context.Products.Add(new Product { ProductId = 10, Dangerous = true });
                context.Products.Add(new Product { ProductId = 11, Dangerous = true });
                context.SaveChanges();
            }

            using (var context = new ProductContext(options))
            {
                // Act
                _productsController = new ProductsController(context);
                var response = await _productsController.GetActiveDangerousDrugs();

                // Assert
                Assert.NotNull(response.Value);
                Assert.Equal(2, response.Value.Count);
            }
        }

        [Fact]
        public async System.Threading.Tasks.Task PutProduct_ChangeDescriptionOfNullField()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ProductContext>()
        .UseInMemoryDatabase(databaseName: "Products Test 23")
        .Options;

            using (var context = new ProductContext(options))
            {
                context.Products.Add(new Product { ProductId = 10, Dangerous = true });

                context.SaveChanges();
            }

            using (var context = new ProductContext(options))
            {
                // Act
                _productsController = new ProductsController(context);
                var response = await _productsController.PutProduct(10,"test");

                var productToInspect = await context.Products.FirstAsync(x => x.ProductId == 10);

                // Assert
                Assert.Equal("test", productToInspect.ProductDescription);

            }
        }

        [Fact]
        public async System.Threading.Tasks.Task PutProduct_ChangeDescriptionField()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ProductContext>()
        .UseInMemoryDatabase(databaseName: "Products Test 24")
        .Options;

            using (var context = new ProductContext(options))
            {
                context.Products.Add(new Product { ProductId = 10, Dangerous = true , ProductDescription = "blah"});

                context.SaveChanges();
            }

            using (var context = new ProductContext(options))
            {
                // Act
                _productsController = new ProductsController(context);
                var response = await _productsController.PutProduct(10, "test");

                var productToInspect = await context.Products.FirstAsync(x => x.ProductId == 10);

                // Assert
                Assert.Equal("test", productToInspect.ProductDescription);

            }
        }

        [Fact]
        public async System.Threading.Tasks.Task PutProduct_Return400IfIdNotFound()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ProductContext>()
        .UseInMemoryDatabase(databaseName: "Products Test 25")
        .Options;

            using (var context = new ProductContext(options))
            {
                context.Products.Add(new Product { ProductId = 10, Dangerous = true, ProductDescription = "blah" });

                context.SaveChanges();
            }

            using (var context = new ProductContext(options))
            {
                // Act
                _productsController = new ProductsController(context);
                var response = await _productsController.PutProduct(110, "test");

                var productToInspect = await context.Products.FirstAsync(x => x.ProductId == 10);
                
                // Assert
                // TODO assert http status code
                Assert.Equal("blah", productToInspect.ProductDescription);

            }
        }

    }
}