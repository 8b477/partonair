using ApplicationLayer.partonair.DTOs;
using ApplicationLayer.partonair.MediatR.Queries.Users;

using DomainLayer.partonair.Exceptions;
using DomainLayer.partonair.Exceptions.Enums;

using Moq;

using TestingLayer.partonair.UserTest.Abstracts;
using TestingLayer.partonair.UserTest.Constants;

namespace TestingLayer.partonair.UserTest.MediatR.Queries
{
    public class GetByNameUserQueryHandlerTest : UserBaseClassTest
    {
        private readonly GetByNameUserQueryHandler _handler;
        private readonly ICollection<UserViewDTO> _usersList;

        public GetByNameUserQueryHandlerTest() : base()
        {
            _handler = new GetByNameUserQueryHandler(_mockUserService.Object);
            _usersList =
[
new (Guid.NewGuid(), UserConstants.NAME,UserConstants.EMAIL,false,DateTime.Now,DateTime.Now,UserConstants.ROLE_VISITOR,null),
                new (Guid.NewGuid(), UserConstants.NAME,UserConstants.EMAIL,false,DateTime.Now,DateTime.Now,UserConstants.ROLE_VISITOR,null),
            ];
        }


        [Fact]
        public async Task GetByNameUserQueryHandler_ShouldReturn_UsersList()
        {
            _mockUserService.Setup(s => s.GetByNameAsyncService(UserConstants.NAME)).ReturnsAsync(_usersList);

            var result = await _handler.Handle(new GetByNameUserQuery(UserConstants.NAME), CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
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
