using DomainLayer.partonair.Exceptions;
using DomainLayer.partonair.Exceptions.Enums;

using Moq;


namespace TestingLayer.partonair.UserTest.Services
{
    public class DeleteServiceTest : BaseUserApplicationServiceTestFixture
    {
        private readonly Guid _id;
        public DeleteServiceTest() => _id = Guid.NewGuid();


        [Fact]
        public async Task DeleteService_ShouldReturn_SuccessVoid()
        {
            _mockUserRepo.Setup(s => s.Delete(_id)).Returns(Task.CompletedTask);
            _mockUoW.Setup(s => s.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);

            await _userService.DeleteService(_id);

            _mockUserRepo.Verify(v => v.Delete(_id),Times.Once);
            _mockUoW.Verify(v => v.SaveChangesAsync(CancellationToken.None),Times.Once);
        }

        [Fact]
        public async Task DeleteService_ShouldThrow_Infrastructure_CancelationDatabaseException()
        {
            _mockUserRepo.Setup(s => s.Delete(_id))
                .Throws(new InfrastructureLayerException(InfrastructureLayerErrorType.CancelationDatabaseException));

            var exception = await Assert.ThrowsAsync<InfrastructureLayerException>(()
                =>_userService.DeleteService(_id));

            Assert.Equal(InfrastructureLayerErrorType.CancelationDatabaseException, exception.ErrorType);

            _mockUserRepo.Verify(v => v.Delete(_id), Times.Once);
        }

        [Fact]
        public async Task DeleteService_ShouldThrow_Infrastructure_UnexpectedDatabaseException()
        {
            _mockUserRepo.Setup(s => s.Delete(_id)).Returns(Task.CompletedTask);

            _mockUoW.Setup(s => s.SaveChangesAsync(CancellationToken.None))
                .ThrowsAsync(new InfrastructureLayerException(InfrastructureLayerErrorType.UnexpectedDatabaseException));

            var exception = await Assert.ThrowsAsync<InfrastructureLayerException>(()
                => _userService.DeleteService(_id));

            Assert.Equal(InfrastructureLayerErrorType.UnexpectedDatabaseException, exception.ErrorType);

            _mockUserRepo.Verify(v => v.Delete(_id), Times.Once);
            _mockUoW.Verify(v => v.SaveChangesAsync(CancellationToken.None),Times.Once);
        }
    }
}
