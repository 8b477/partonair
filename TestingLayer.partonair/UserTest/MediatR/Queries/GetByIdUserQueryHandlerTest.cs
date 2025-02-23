using ApplicationLayer.partonair.DTOs;
using ApplicationLayer.partonair.MediatR.Queries.Users;

using DomainLayer.partonair.Exceptions.Enums;
using DomainLayer.partonair.Exceptions;

using Moq;
using TestingLayer.partonair.UserTest.Constants;


namespace TestingLayer.partonair.UserTest.MediatR.Queries
{
    public class GetByIdUserQueryHandlerTest : BaseUserApplicationTestFixture<GetByIdUserQueryHandler>
    {
        [Fact]
        public async Task GetByIdUserQueryHandler_ShouldReturn_UserViewDTO()
        {
            UserViewDTO dto = new(Guid.NewGuid(), UserConstants.NAME, UserConstants.EMAIL, false, DateTime.Now, DateTime.Now, UserConstants.ROLE_VISITOR, null);

            _mockUserService.Setup(s => s.GetByIdAsyncService(It.IsAny<Guid>()))
                .ReturnsAsync(dto);

            var result = await _handler.Handle(new GetByIdUserQuery(It.IsAny<Guid>()), CancellationToken.None);

            Assert.NotNull(result);
            Assert.IsType<UserViewDTO>(result);

            _mockUserService.Verify(v => v.GetByIdAsyncService(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task GetByIdUserQueryHandler_ShouldThrow_ResourceNotFoundException()
        {
            _mockUserService.Setup(s => s.GetByIdAsyncService(Guid.Empty))
                .ThrowsAsync(new InfrastructureLayerException(InfrastructureLayerErrorType.ResourceNotFoundException));

            var exception = await Assert.ThrowsAsync<InfrastructureLayerException>(() =>
                _handler.Handle(new GetByIdUserQuery(Guid.Empty), CancellationToken.None));

            Assert.Equal(InfrastructureLayerErrorType.ResourceNotFoundException, exception.ErrorType);

            _mockUserService.Verify(v => v.GetByIdAsyncService(Guid.Empty), Times.Once);
        }

        [Fact]
        public async Task GetByIdUserQueryHandler_ShouldThrow_CancelationDatabaseExceptionn()
        {
            _mockUserService.Setup(s => s.GetByIdAsyncService(Guid.Empty))
                .ThrowsAsync(new InfrastructureLayerException(InfrastructureLayerErrorType.CancelationDatabaseException));

            var exception = await Assert.ThrowsAsync<InfrastructureLayerException>(() =>
                _handler.Handle(new GetByIdUserQuery(Guid.Empty), CancellationToken.None));

            Assert.Equal(InfrastructureLayerErrorType.CancelationDatabaseException, exception.ErrorType);

            _mockUserService.Verify(v => v.GetByIdAsyncService(Guid.Empty), Times.Once);
        }

    }
}
