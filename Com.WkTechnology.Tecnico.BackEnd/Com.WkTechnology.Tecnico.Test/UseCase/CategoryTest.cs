using AutoMapper;
using Com.WkTechnology.Tecnico.Domain.DTO.Category;
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
    public class CategoryTest
    {
        private ICategoryService _categoryService;
        private readonly Mock<ICategoryRepository> _mockCategoryRepositoryMock;
        private readonly CategoryValidation validations;
        private readonly IMapper _mockMapper;

        

        public CategoryTest()
        {
            _mockCategoryRepositoryMock = new Mock<ICategoryRepository>();
            validations = new CategoryValidation();
            _mockMapper = MapperFactory.Create();
            this._categoryService = new CategoryService(_mockCategoryRepositoryMock.Object, validations,_mockMapper);
            _mockCategoryRepositoryMock.Setup(x => x.InsertAsync(It.IsAny<CategoryEntity>())).ReturnsAsync(new CategoryFake().GenerateOne());
        }

        [Fact]
        public void IncludedSuccessfully()
        {
            //Arrange
            CategoryDTO category = _mockMapper.Map<CategoryDTO>(new CategoryFake().GenerateOne());

            //Act
            var result = _categoryService.InsertAsync(category).Result;

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
            CategoryDTO category = _mockMapper.Map<CategoryDTO>(new CategoryFake().GenerateOne());
            category.Name = "jasbhdflkjhasdfkjhasdlkfjkasdjkljadsflkjadsfkljhsdafkljhsadklfjhsadkfjkljadshfkljadshfkljsdahfkljhsdafkljhsadkfjhsadklfhkljadshfkljsdahfkljhsadkfjhasdlkjfkljadshfkljasdhfkjadhsflkjadhsflkjasdhfkljasdhflkjadhsflkjahsdkljfasdf";

            //Act
            var result = _categoryService.InsertAsync(category).Result;

            //Assert
            Assert.False(result.Successfully);
            Assert.Equal(400, result.HTTPStatusCode);
        }

        [Fact]
        public void Exception()
        {
            //Arrange
            CategoryDTO category = _mockMapper.Map<CategoryDTO>(new CategoryFake().GenerateOne());
            this._categoryService = new CategoryService(null, validations, _mockMapper);

            //Act

            //Assert
            Assert.Throws<AggregateException>(() => _categoryService.InsertAsync(category).Result);
        }
    }
}