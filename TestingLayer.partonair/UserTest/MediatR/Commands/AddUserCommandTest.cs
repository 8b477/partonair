using ApplicationLayer.partonair.DTOs;
using ApplicationLayer.partonair.MediatR.Commands.Users;

using DomainLayer.partonair.Exceptions.Enums;
using DomainLayer.partonair.Exceptions;

using Moq;

using TestingLayer.partonair.UserTest.Constants;


namespace TestingLayer.partonair.UserTest.MediatR.Commands
{
    public class AddUserCommandTest : BaseUserApplicationTestFixture<AddUserCommandHandler>
    {
        [Fact]
        public async Task AddUserCommandHandler_ShouldReturnCreatedUser()
        {
            // Arrange
            var userCreateDto = new UserCreateDTO
                (
                UserConstants.NAME,
                UserConstants.EMAIL,
                UserConstants.PASSWORD
                );

            var expectedUser = new UserViewDTO
                (
                    new Guid(),
                   userCreateDto.UserName,
                   userCreateDto.Email,
                   false,
                   DateTime.Now,
                   DateTime.Now,
                   "Visitor",
                   null
                );

            // Act
            _mockUserService.Setup(s => s.CreateAsyncService(userCreateDto))
                            .ReturnsAsync(expectedUser);

            var result = await _handler.Handle(new AddUserCommand(userCreateDto), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<UserViewDTO>(result);

            _mockUserService.Verify(v => v.CreateAsyncService(userCreateDto), Times.Once());
        }

        [Fact]
        public async Task AddUserCommandHandler_WhenEmailNotAvailable_ShouldThrowApplicationLayerException()
        {
            // Arrange
            var userCreateDto = new UserCreateDTO(UserConstants.NAME, UserConstants.EMAIL, UserConstants.PASSWORD);

            _mockUserService.Setup(s => s.CreateAsyncService(It.IsAny<UserCreateDTO>()))
                .ThrowsAsync(new ApplicationLayerException(ApplicationLayerErrorType.ConstraintViolationErrorException));

            // Act && Assert
            var exception = await Assert.ThrowsAsync<ApplicationLayerException>(() =>
                _handler.Handle(new AddUserCommand(userCreateDto), CancellationToken.None));

            Assert.Equal(ApplicationLayerErrorType.ConstraintViolationErrorException, exception.ErrorType);
        }

        [Fact]
        public async Task AddUserCommandHandler_WhenDatabaseOperationCanceled_ShouldThrowInfrastructureLayerException()
        {
            // Arrange
            var userCreateDto = new UserCreateDTO(UserConstants.NAME, UserConstants.EMAIL, UserConstants.PASSWORD);

            _mockUserService.Setup(s => s.CreateAsyncService(It.IsAny<UserCreateDTO>()))
                .ThrowsAsync(new InfrastructureLayerException(InfrastructureLayerErrorType.CancelationDatabaseException, "Database operation canceled"));

            // Act && Assert
            var exception = await Assert.ThrowsAsync<InfrastructureLayerException>(() =>
                _handler.Handle(new AddUserCommand(userCreateDto), CancellationToken.None));

            Assert.Equal(InfrastructureLayerErrorType.CancelationDatabaseException, exception.ErrorType);
        }


    }
}

