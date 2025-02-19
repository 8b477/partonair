using ApplicationLayer.partonair.DTOs;
using ApplicationLayer.partonair.MediatR.Queries.Users;

using DomainLayer.partonair.Exceptions;
using DomainLayer.partonair.Exceptions.Enums;

using Moq;

using TestingLayer.partonair.UserTest.Constants;


namespace TestingLayer.partonair.UserTest.MediatR.Queries
{
    public class GetByEmailUserQueryHandlerTest : BaseUserApplicationTestFixture
    {
        private readonly GetByEmailUserQueryHandler _handler;
        private readonly UserViewDTO _user;
        public GetByEmailUserQueryHandlerTest()
        {
            _handler = new GetByEmailUserQueryHandler(_mockUserService.Object);

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
        public async Task GetByEmailUserQueryHandler_ShouldReturn_UserViewDTO()
        {
            _mockUserService.Setup(s => s.GetByEmailAsyncService(UserConstants.EMAIL)).ReturnsAsync(_user);

            var result = await _handler.Handle(new GetByEmailUserQuery(UserConstants.EMAIL), CancellationToken.None);

            Assert.NotNull(result);
            Assert.IsType<UserViewDTO>(result);
            Assert.Equal(UserConstants.EMAIL, result.Email);
            Assert.Equal(UserConstants.NAME, result.UserName);

            _mockUserService.Verify(v => v.GetByEmailAsyncService(UserConstants.EMAIL), Times.Once);
        }

        [Fact]
        public async Task GetByEmailUserQueryHandler_ShouldThrow_ResourceNotFoundException()
        {
            _mockUserService.Setup(s => s.GetByEmailAsyncService("error@mail.kc"))
                .ThrowsAsync(new InfrastructureLayerException(InfrastructureLayerErrorType.ResourceNotFoundException));

            var exception = await Assert.ThrowsAsync<InfrastructureLayerException>(() =>
                _handler.Handle(new GetByEmailUserQuery("error@mail.kc"), CancellationToken.None));

            Assert.Equal(InfrastructureLayerErrorType.ResourceNotFoundException, exception.ErrorType);

            _mockUserService.Verify(v => v.GetByEmailAsyncService("error@mail.kc"), Times.Once);
        }

    }
}
