using ApplicationLayer.partonair.DTOs;
using ApplicationLayer.partonair.MediatR.Queries.Users;

using DomainLayer.partonair.Exceptions;
using DomainLayer.partonair.Exceptions.Enums;

using Moq;

using TestingLayer.partonair.UserTest.Constants;

namespace TestingLayer.partonair.UserTest.MediatR.Queries
{
    public class GetByNameUserQueryHandlerTest : BaseUserApplicationTestFixture<GetByNameUserQueryHandler>
    {
        [Fact]
        public async Task GetByNameUserQueryHandler_ShouldReturn_UsersList()
        {

            ICollection<UserViewDTO> usersList =
            [
            new (Guid.NewGuid(), UserConstants.NAME,UserConstants.EMAIL,false,DateTime.Now,DateTime.Now,UserConstants.ROLE_VISITOR,null),
            new (Guid.NewGuid(), UserConstants.NAME,UserConstants.EMAIL,false,DateTime.Now,DateTime.Now,UserConstants.ROLE_VISITOR,null),
            ];

            _mockUserService.Setup(s => s.GetByNameAsyncService(UserConstants.NAME)).ReturnsAsync(usersList);

            var result = await _handler.Handle(new GetByNameUserQuery(UserConstants.NAME), CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(usersList.Count, result.Count);
            Assert.IsType<List<UserViewDTO>>(result);

            _mockUserService.Verify(v => v.GetByNameAsyncService(UserConstants.NAME), Times.Once);
        }

        [Fact]
        public async Task GetByNameUserQueryHandler_ShouldThrow_ResourceNotFoundException()
        {
            _mockUserService.Setup(s => s.GetByNameAsyncService("notFound"))
                .ThrowsAsync(new InfrastructureLayerException(InfrastructureLayerErrorType.ResourceNotFoundException));

            var exception = await Assert.ThrowsAsync<InfrastructureLayerException>(() =>
            _handler.Handle(new GetByNameUserQuery("notFound"), CancellationToken.None));

            Assert.Equal(InfrastructureLayerErrorType.ResourceNotFoundException, exception.ErrorType);

            _mockUserService.Verify(v => v.GetByNameAsyncService("notFound"), Times.Once);
        }

    }
}
