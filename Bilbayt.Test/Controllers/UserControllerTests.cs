using System.Threading.Tasks;
using Bilbayt.Controllers;
using Bilbayt.Core.Exceptions;
using Bilbayt.Infrastructure.Identity.Models.Authentication;
using Bilbayt.Models.Token;
using MediatR;
using Moq;
using Xunit;

namespace Bilbayt.Test.Controllers
{
    public class UserControllerTests
    {
        private readonly Mock<IMediator> _mediator;

        public UserControllerTests()
        {
            _mediator = new Mock<IMediator>();
        }

        [Fact]
        public async Task AuthenticateAsync_ValidCredentials_LoginSuccessful()
        {
            // Arrange
            var command = new Authenticate.AuthenticateCommand
            {
                Username = "mahmudovanilufar@gmail.com",
                Password = "123456"
            };
            var mockResult = new Authenticate.CommandResponse()
            {
                Resource = new TokenResponse()
                {
                    Id = "mahmudovanilufar@gmail.com:someOtherString",
                    EmailAddress = "mahmudovanilufar@gmail.com",
                    FullName = "Nilufar Mahmud",
                    Token = "Some token string"
                }
            };

            _mediator.Setup(x => x.Send(It.IsAny<Authenticate.AuthenticateCommand>(), It.IsAny<System.Threading.CancellationToken>()))
                .ReturnsAsync(mockResult);

            var controller = new UserController(_mediator.Object);
            // Act
            var result = await controller.AuthenticateAsync(command);


            // Assert
            Assert.NotNull(result);
            Assert.Equal(mockResult.Resource.Id, result.Id);
            Assert.Equal(mockResult.Resource.EmailAddress, result.EmailAddress);
            Assert.Equal(mockResult.Resource.FullName, result.FullName);
            Assert.Equal(mockResult.Resource.Token, result.Token);
        }

        [Fact]
        public async Task AuthenticateAsync_InvalidCredentials_InvalidCredentialsExceptionThrown()
        {
            // Arrange
            var command = new Authenticate.AuthenticateCommand
            {
                Username = "mahmudovanilufar@gmail.com",
                Password = "123456"
            };

            _mediator.Setup(x => x.Send(It.IsAny<Authenticate.AuthenticateCommand>(), It.IsAny<System.Threading.CancellationToken>())).ThrowsAsync(new InvalidCredentialsException());

            var controller = new UserController(_mediator.Object);

            // Act && Assert
            await Assert.ThrowsAsync<InvalidCredentialsException>(async () => await controller.AuthenticateAsync(command));
        }
    }
}
