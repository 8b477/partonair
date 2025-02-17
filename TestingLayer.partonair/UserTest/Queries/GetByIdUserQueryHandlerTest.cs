using ApplicationLayer.partonair.DTOs;
using ApplicationLayer.partonair.MediatR.Queries.Users;

using DomainLayer.partonair.Exceptions.Enums;
using DomainLayer.partonair.Exceptions;

using Moq;

using TestingLayer.partonair.UserTest.Abstracts;
using TestingLayer.partonair.UserTest.Constants;


namespace TestingLayer.partonair.UserTest.Queries
{
    public class GetByIdUserQueryHandlerTest : UserBaseClassTest
    {
        private readonly GetByIdUserQueryHandler _handler;
        private readonly UserViewDTO _user;
        public GetByIdUserQueryHandlerTest() :base()
        {
            _handler = new GetByIdUserQueryHandler(_mockUserService.Object);
            _user = new UserViewDTO
                (
                    new Guid(),
                   UserConstants.NAME,
                   UserConstants.EMAIL,
                   false,
                   DateTime.Now,
                   DateTime.Now,
                   UserConstants.ROLE_VISITOR,
                   null
                );
        }

        [Fact]
        public async Task GetByIdUserQueryHandler_ShouldReturn_UserViewDTO()
        {
            _mockUserService.Setup(s => s.GetByIdAsyncService(_user.Id)).ReturnsAsync(_user);

            var result = await _handler.Handle(new GetByIdUserQuery(_user.Id), CancellationToken.None);

            Assert.NotNull(result);
            Assert.IsType<UserViewDTO>(result);
            Assert.Equal(_user.UserName, result.UserName);
            Assert.Equal(_user.Email, result.Email);

            _mockUserService.Verify(v => v.GetByIdAsyncService(_user.Id), Times.Once);
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
