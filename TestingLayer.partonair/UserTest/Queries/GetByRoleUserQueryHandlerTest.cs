using ApplicationLayer.partonair.DTOs;
using ApplicationLayer.partonair.MediatR.Queries.Users;

using DomainLayer.partonair.Exceptions.Enums;
using DomainLayer.partonair.Exceptions;

using Moq;

using TestingLayer.partonair.UserTest.Abstracts;
using TestingLayer.partonair.UserTest.Constants;

namespace TestingLayer.partonair.UserTest.Queries
{
    public class GetByRoleUserQueryHandlerTest : UserBaseClassTest
    {
        private readonly GetByRoleUserQueryHandler _handler;
        private readonly ICollection<UserViewDTO> _usersListRoleVisitor;
        private readonly ICollection<UserViewDTO> _usersListRoleEmployee;
        private readonly ICollection<UserViewDTO> _usersListRoleCompany;
        public GetByRoleUserQueryHandlerTest():base()
        {
            _handler = new GetByRoleUserQueryHandler(_mockUserService.Object);
            _usersListRoleVisitor =
[
new (Guid.NewGuid(), UserConstants.NAME,UserConstants.EMAIL,false,DateTime.Now,DateTime.Now,UserConstants.ROLE_VISITOR,null),
                new (Guid.NewGuid(), UserConstants.NAME,UserConstants.EMAIL,false,DateTime.Now,DateTime.Now,UserConstants.ROLE_VISITOR,null),
            ];
            _usersListRoleEmployee =
[
new (Guid.NewGuid(), UserConstants.NAME,UserConstants.EMAIL,false,DateTime.Now,DateTime.Now,UserConstants.ROLE_VISITOR,null),
                new (Guid.NewGuid(), UserConstants.NAME,UserConstants.EMAIL,false,DateTime.Now,DateTime.Now,UserConstants.ROLE_EMPLOYEE,null),
            ];
            _usersListRoleCompany =
[
new (Guid.NewGuid(), UserConstants.NAME,UserConstants.EMAIL,false,DateTime.Now,DateTime.Now,UserConstants.ROLE_VISITOR,null),
                new (Guid.NewGuid(), UserConstants.NAME,UserConstants.EMAIL,false,DateTime.Now,DateTime.Now,UserConstants.ROLE_COMPANY,null),
            ];
        }

        [Fact]
        public async Task GetByRoleUserQueryHandler_ShouldReturn_UsersListWithRoleVisitor()
        {
            _mockUserService.Setup(s => s.GetByRoleAsyncService(UserConstants.ROLE_VISITOR)).ReturnsAsync(_usersListRoleVisitor);

            var result = await _handler.Handle(new GetByRoleUserQuery(UserConstants.ROLE_VISITOR), CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(2, _usersListRoleVisitor.Count);
            Assert.IsType<List<UserViewDTO>>(result);

            _mockUserService.Verify(v => v.GetByRoleAsyncService(UserConstants.ROLE_VISITOR),Times.Once);
        }

        [Fact]
        public async Task GetByRoleUserQueryHandler_ShouldReturn_UsersListWithRoleEmployee()
        {
            _mockUserService.Setup(s => s.GetByRoleAsyncService(UserConstants.ROLE_VISITOR)).ReturnsAsync(_usersListRoleEmployee);

            var result = await _handler.Handle(new GetByRoleUserQuery(UserConstants.ROLE_VISITOR), CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(2, _usersListRoleEmployee.Count);
            Assert.IsType<List<UserViewDTO>>(result);

            _mockUserService.Verify(v => v.GetByRoleAsyncService(UserConstants.ROLE_VISITOR), Times.Once);
        }

        [Fact]
        public async Task GetByRoleUserQueryHandler_ShouldReturn_UsersListWithRoleCompany()
        {
            _mockUserService.Setup(s => s.GetByRoleAsyncService(UserConstants.ROLE_VISITOR)).ReturnsAsync(_usersListRoleCompany);

            var result = await _handler.Handle(new GetByRoleUserQuery(UserConstants.ROLE_VISITOR), CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(2, _usersListRoleCompany.Count);
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
