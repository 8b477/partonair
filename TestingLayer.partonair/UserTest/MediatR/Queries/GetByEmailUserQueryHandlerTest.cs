using ApplicationLayer.partonair.DTOs;
using ApplicationLayer.partonair.MediatR.Queries.Users;

using DomainLayer.partonair.Exceptions;
using DomainLayer.partonair.Exceptions.Enums;

using Moq;

using TestingLayer.partonair.UserTest.Constants;


namespace TestingLayer.partonair.UserTest.MediatR.Queries
{
    public class GetByEmailUserQueryHandlerTest : BaseUserApplicationTestFixture<GetByEmailUserQueryHandler>
    {
        [Fact]
        public async Task GetByEmailUserQueryHandler_ShouldReturn_UserViewDTO()
        {
            UserViewDTO dto = new (Guid.NewGuid(),UserConstants.NAME,UserConstants.EMAIL,false,DateTime.Now,DateTime.Now,UserConstants.ROLE_VISITOR,null);
            _mockUserService.Setup(s => s.GetByEmailAsyncService(UserConstants.EMAIL)).ReturnsAsync(dto);

            var result = await _handler.Handle(new GetByEmailUserQuery(UserConstants.EMAIL), CancellationToken.None);

            Assert.NotNull(result);
            Assert.IsType<UserViewDTO>(result);

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
