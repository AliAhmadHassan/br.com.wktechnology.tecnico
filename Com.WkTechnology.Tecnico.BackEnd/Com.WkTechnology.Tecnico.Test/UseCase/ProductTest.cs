using AutoMapper;
using Com.WkTechnology.Tecnico.Domain.DTO.Product;
using Com.WkTechnology.Tecnico.Domain.Entities;
using Com.WkTechnology.Tecnico.Domain.Interfaces.Repository;
using Com.WkTechnology.Tecnico.Domain.Interfaces.Services;
using Com.WkTechnology.Tecnico.Service.Services;
using Com.WkTechnology.Tecnico.Service.Validation;
using Com.WkTechnology.Tecnico.Test.Fakes;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Com.WkTechnology.Tecnico.Test.UseCase
{
    public class ProductTest
    {
        private IProductService _productService;
        private readonly Mock<IProductRepository> _mockProductRepositoryMock;
        private readonly ProductValidation validations;
        private readonly IMapper _mockMapper;

        

        public ProductTest()
        {
            _mockProductRepositoryMock = new Mock<IProductRepository>();
            validations = new ProductValidation();
            _mockMapper = MapperFactory.Create();
            this._productService = new ProductService(_mockProductRepositoryMock.Object, validations,_mockMapper);
            _mockProductRepositoryMock.Setup(x => x.InsertAsync(It.IsAny<ProductEntity>())).ReturnsAsync(new ProductFake().GenerateOne());
        }

        [Fact]
        public void IncludedSuccessfully()
        {
            //Arrange
            ProductDTO product = _mockMapper.Map<ProductDTO>(new ProductFake().GenerateOne());

            //Act
            var result = _productService.InsertAsync(product).Result;

            //Assert
            Assert.True(result.Successfully);
            Assert.Equal(1, result.TotalRows);
            Assert.Equal("1 row inserted", result.Message);
            Assert.Equal(200, result.HTTPStatusCode);
        }

        [Fact]
        public void BadRequest()
        {
            //Arrange
            ProductDTO product = _mockMapper.Map<ProductDTO>(new ProductFake().GenerateOne());
            product.Name = "jasbhdflkjhasdfkjhasdlkfjkasdjkljadsflkjadsfkljhsdafkljhsadklfjhsadkfjkljadshfkljadshfkljsdahfkljhsdafkljhsadkfjhsadklfhkljadshfkljsdahfkljhsadkfjhasdlkjfkljadshfkljasdhfkjadhsflkjadhsflkjasdhfkljasdhflkjadhsflkjahsdkljfasdf";

            //Act
            var result = _productService.InsertAsync(product).Result;

            //Assert
            Assert.False(result.Successfully);
            Assert.Equal(400, result.HTTPStatusCode);
        }

        [Fact]
        public void Exception()
        {
            //Arrange
            ProductDTO product = _mockMapper.Map<ProductDTO>(new ProductFake().GenerateOne());
            this._productService = new ProductService(null, validations, _mockMapper);

            //Act

            //Assert
            Assert.Throws<AggregateException>(() => _productService.InsertAsync(product).Result);
        }
    }
}