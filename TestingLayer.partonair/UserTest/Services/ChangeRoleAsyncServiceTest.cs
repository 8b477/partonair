using DomainLayer.partonair.Exceptions.Enums;
using DomainLayer.partonair.Exceptions;
using ApplicationLayer.partonair.DTOs;

using Moq;


namespace TestingLayer.partonair.UserTest.Services
{
    public class ChangeRoleAsyncServiceTest : ExtendBaseUserApplicationServiceTestFixture
    {
        private readonly Guid _id;
        private readonly UserChangeRoleDTO _userVisitor;

        public ChangeRoleAsyncServiceTest()
        {
            _id = Guid.NewGuid();
            _userVisitor = new UserChangeRoleDTO("Visitor");
        }


        [Fact]
        public async Task ChangeRoleAsyncService_ShoudlReturn_True()
        {
            _mockUserRepo.Setup(s => s.IsUserWithoutProfil(_id)).ReturnsAsync(true);
            _mockUserRepo.Setup(s => s.ChangeRoleAsync(_id, _userVisitor.Role)).ReturnsAsync(true);
            _mockUoW.Setup(s => s.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);

            var result = await _userService.ChangeRoleAsyncService(_id, _userVisitor);

            Assert.True(result);

            _mockUserRepo.Verify(v => v.IsUserWithoutProfil(_id), Times.Once);
            _mockUserRepo.Verify(v => v.ChangeRoleAsync(_id, _userVisitor.Role), Times.Once);
            _mockUoW.Verify(v => v.SaveChangesAsync(CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task ChangeRoleAsyncService_ShoudlThrow_WhenUserIsNotWithoutProfile_Application_ConstraintViolationErrorException()
        {
            _mockUserRepo.Setup(s => s.IsUserWithoutProfil(_id)).ReturnsAsync(false);

            var exception = await Assert.ThrowsAsync<ApplicationLayerException>(() 
                =>_userService.ChangeRoleAsyncService(_id, _userVisitor));

            Assert.Equal(ApplicationLayerErrorType.ConstraintViolationErrorException,exception.ErrorType);

            _mockUserRepo.Verify(v => v.IsUserWithoutProfil(_id), Times.Once);
        }

        [Fact]
        public async Task ChangeRoleAsyncService_ShouldThrow_Infrastructure_EntityIsNullException()
        {
            _mockUserRepo.Setup(s => s.IsUserWithoutProfil(_id)).ReturnsAsync(true);

            _mockUserRepo.Setup(s => s.ChangeRoleAsync(_id,_userVisitor.Role))
                .ThrowsAsync(new InfrastructureLayerException(InfrastructureLayerErrorType.EntityIsNullException));

            var exception = await Assert.ThrowsAsync<InfrastructureLayerException>(() 
                => _userService.ChangeRoleAsyncService(_id, _userVisitor));

            Assert.Equal(InfrastructureLayerErrorType.EntityIsNullException, exception.ErrorType);

            _mockUserRepo.Verify(v => v.IsUserWithoutProfil(_id), Times.Once);
            _mockUserRepo.Verify(v => v.ChangeRoleAsync(_id, _userVisitor.Role),Times.Once);
        }

        [Fact]
        public async Task ChangeRoleAsyncService_ShouldThrow_Infrastructure_UnexpectedDatabaseException()
        {
            _mockUserRepo.Setup(s => s.IsUserWithoutProfil(_id)).ReturnsAsync(true);
            _mockUserRepo.Setup(s => s.ChangeRoleAsync(_id, _userVisitor.Role)).ReturnsAsync(true);

            _mockUoW.Setup(s => s.SaveChangesAsync(CancellationToken.None))
                .ThrowsAsync(new InfrastructureLayerException(InfrastructureLayerErrorType.UnexpectedDatabaseException));

            var exception = await Assert.ThrowsAsync<InfrastructureLayerException>( ()
                => _userService.ChangeRoleAsyncService(_id,_userVisitor) );

            Assert.Equal(InfrastructureLayerErrorType.UnexpectedDatabaseException, exception.ErrorType);

            _mockUserRepo.Verify(v => v.IsUserWithoutProfil(_id), Times.Once);
            _mockUserRepo.Verify(v => v.ChangeRoleAsync(_id, _userVisitor.Role), Times.Once);
            _mockUoW.Verify(v => v.SaveChangesAsync(CancellationToken.None),Times.Once);
        }

    }
}