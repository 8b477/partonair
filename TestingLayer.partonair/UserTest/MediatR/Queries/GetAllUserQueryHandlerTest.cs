using ApplicationLayer.partonair.DTOs;
using ApplicationLayer.partonair.MediatR.Queries.Users;

using DomainLayer.partonair.Exceptions.Enums;

using DomainLayer.partonair.Exceptions;

using Moq;

using TestingLayer.partonair.UserTest.Constants;


namespace TestingLayer.partonair.UserTest.MediatR.Queries
{
    public class GetAllUserQueryHandlerTest : BaseUserApplicationTestFixture<GetAllUserQueryHandler>
    {
        private readonly ICollection<UserViewDTO> _usersList;

        public GetAllUserQueryHandlerTest()
        {
            _usersList =
            [
    new (Guid.NewGuid(), UserConstants.NAME,UserConstants.EMAIL,false,DateTime.Now,DateTime.Now,UserConstants.ROLE_VISITOR,null),
                new (Guid.NewGuid(), UserConstants.NAME,UserConstants.EMAIL,false,DateTime.Now,DateTime.Now,UserConstants.ROLE_VISITOR,null),
            ];
        }

        [Fact]
        public async Task GetAllUserQueryHandler_ShoudlReturn_ListUserViewDTO()
        {
            _mockUserService.Setup(s => s.GetAllAsyncService()).ReturnsAsync(_usersList);

            var result = await _handler.Handle(new GetAllUserQuery(), CancellationToken.None);

            Assert.NotNull(result);
            Assert.IsType<List<UserViewDTO>>(result);
            Assert.Equal(2, _usersList.Count);
            Assert.Equal(_usersList, result);

            _mockUserService.Verify(v => v.GetAllAsyncService(), Times.Once);
        }

        [Fact]
        public async Task GetAllUserQueryHandler_ShoudlReturn_EmptyListUserViewDTO()
        {
            ICollection<UserViewDTO> usersListEmpty = [];

            _mockUserService.Setup(s => s.GetAllAsyncService()).ReturnsAsync(usersListEmpty);

            var result = await _handler.Handle(new GetAllUserQuery(), CancellationToken.None);

            Assert.NotNull(result);
            Assert.IsType<List<UserViewDTO>>(result);
            Assert.Empty(result);
            Assert.Equal(usersListEmpty, result);

            _mockUserService.Verify(v => v.GetAllAsyncService(), Times.Once);
        }

        [Fact]
        public async Task GetAllUserQueryHandler_WhenDatabaseOperationCanceled_ShouldThrow_InfrastructureOperationCanceledException()
        {
            // Arrange
            _mockUserService.Setup(s => s.GetAllAsyncService())
                .ThrowsAsync(new InfrastructureLayerException(InfrastructureLayerErrorType.CancelationDatabaseException));

            // Act && Assert
            var exception = await Assert.ThrowsAsync<InfrastructureLayerException>(() =>
                _handler.Handle(new GetAllUserQuery(), CancellationToken.None));

            Assert.Equal(InfrastructureLayerErrorType.CancelationDatabaseException, exception.ErrorType);

            _mockUserService.Verify(v => v.GetAllAsyncService(), Times.Once);
        }

    }
}
