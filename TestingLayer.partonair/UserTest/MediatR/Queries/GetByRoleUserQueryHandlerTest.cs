using ApplicationLayer.partonair.DTOs;
using ApplicationLayer.partonair.MediatR.Queries.Users;

using DomainLayer.partonair.Exceptions.Enums;
using DomainLayer.partonair.Exceptions;

using Moq;

using TestingLayer.partonair.UserTest.Constants;


namespace TestingLayer.partonair.UserTest.MediatR.Queries
{
    public class GetByRoleUserQueryHandlerTest : BaseUserApplicationTestFixture<GetByRoleUserQueryHandler>
    {
        [Fact]
        public async Task GetByRoleUserQueryHandler_ShouldReturn_UsersListWithRoleVisitor()
        {
            ICollection<UserViewDTO> usersListRoleVisitor =
            [
            new (Guid.NewGuid(), UserConstants.NAME,UserConstants.EMAIL,false,DateTime.Now,DateTime.Now,UserConstants.ROLE_VISITOR,null),
            new (Guid.NewGuid(), UserConstants.NAME,UserConstants.EMAIL,false,DateTime.Now,DateTime.Now,UserConstants.ROLE_VISITOR,null),
            ];

            _mockUserService.Setup(s => s.GetByRoleAsyncService(UserConstants.ROLE_VISITOR)).ReturnsAsync(usersListRoleVisitor);

            var result = await _handler.Handle(new GetByRoleUserQuery(UserConstants.ROLE_VISITOR), CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(2, usersListRoleVisitor.Count);
            Assert.IsType<List<UserViewDTO>>(result);

            _mockUserService.Verify(v => v.GetByRoleAsyncService(UserConstants.ROLE_VISITOR), Times.Once);
        }

        [Fact]
        public async Task GetByRoleUserQueryHandler_ShouldReturn_UsersListWithRoleEmployee()
        {
        ICollection<UserViewDTO> usersListRoleEmployee =
        [
        new (Guid.NewGuid(), UserConstants.NAME,UserConstants.EMAIL,false,DateTime.Now,DateTime.Now,UserConstants.ROLE_VISITOR,null),
        new (Guid.NewGuid(), UserConstants.NAME,UserConstants.EMAIL,false,DateTime.Now,DateTime.Now,UserConstants.ROLE_EMPLOYEE,null),
        ];


            _mockUserService.Setup(s => s.GetByRoleAsyncService(UserConstants.ROLE_VISITOR)).ReturnsAsync(usersListRoleEmployee);

            var result = await _handler.Handle(new GetByRoleUserQuery(UserConstants.ROLE_VISITOR), CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(usersListRoleEmployee.Count, result.Count);
            Assert.IsType<List<UserViewDTO>>(result);

            _mockUserService.Verify(v => v.GetByRoleAsyncService(UserConstants.ROLE_VISITOR), Times.Once);
        }

        [Fact]
        public async Task GetByRoleUserQueryHandler_ShouldReturn_UsersListWithRoleCompany()
        {
           ICollection<UserViewDTO> usersListRoleCompany =
            [
            new (Guid.NewGuid(), UserConstants.NAME,UserConstants.EMAIL,false,DateTime.Now,DateTime.Now,UserConstants.ROLE_VISITOR,null),
            new (Guid.NewGuid(), UserConstants.NAME,UserConstants.EMAIL,false,DateTime.Now,DateTime.Now,UserConstants.ROLE_COMPANY,null),
            ];
            _mockUserService.Setup(s => s.GetByRoleAsyncService(UserConstants.ROLE_VISITOR)).ReturnsAsync(usersListRoleCompany);

            var result = await _handler.Handle(new GetByRoleUserQuery(UserConstants.ROLE_VISITOR), CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(usersListRoleCompany.Count, result.Count);
            Assert.IsType<List<UserViewDTO>>(result);

            _mockUserService.Verify(v => v.GetByRoleAsyncService(UserConstants.ROLE_VISITOR), Times.Once);
        }

        [Fact]
        public async Task GetByRoleUserQueryHandler_ShouldThrow_ResourceNotFoundException()
        {
            _mockUserService.Setup(s => s.GetByRoleAsyncService("notFound"))
                .ThrowsAsync(new InfrastructureLayerException(InfrastructureLayerErrorType.ResourceNotFoundException));

            var exception = await Assert.ThrowsAsync<InfrastructureLayerException>(() =>
                _handler.Handle(new GetByRoleUserQuery("notFound"), CancellationToken.None));

            Assert.Equal(InfrastructureLayerErrorType.ResourceNotFoundException, exception.ErrorType);

            _mockUserService.Verify(v => v.GetByRoleAsyncService("notFound"), Times.Once);
        }

    }
}
