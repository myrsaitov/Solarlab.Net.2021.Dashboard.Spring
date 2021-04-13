﻿using WidePictBoard.Application.Services.Category.Contracts;
using WidePictBoard.Application.Services.Category.Contracts.Exceptions;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using AutoFixture.Xunit2;

namespace WidePictBoard.Tests.Category
{
    public partial class CategoryServiceV1Test
    {
        [Theory]
        [AutoData]
        public async Task GetById_Returns_Response_Success(
            GetById.Request request, 
            CancellationToken cancellationToken)
        {
            // Arrange
            ConfigureMoqForGetByIdMethod();

            // Act
            var response = await _categoryServiceV1.GetById(
                request, 
                cancellationToken);

            // Assert
            _categoryRepositoryMock.Verify();
            Assert.NotNull(response);
            Assert.NotEqual(default, response.Id);
        }
        [Theory]
        [InlineAutoData(null)]
        public async Task GetById_Throws_Exception_When_Request_Is_Null(
            GetById.Request request, 
            CancellationToken cancellationToken)
        {
            // Arrange
            ConfigureMoqForGetByIdMethod();

            // Act
            await Assert.ThrowsAsync<CategoryGetByIdRequestIsNullException>(
                async () => await _categoryServiceV1.GetById(
                    request, 
                    cancellationToken));

        }
        private void ConfigureMoqForGetByIdMethod()
        {
            var category = new Domain.Category();

            _categoryRepositoryMock
                .Setup(_ => _.FindByIdWithParentAndChilds(
                    It.IsAny<int>(), 
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(category)
                .Callback((int _categoryId, CancellationToken ct) => category.Id = _categoryId)
                .Verifiable();
        }
    }
}
