using ApplicationLayer.partonair.MediatR.Commands.Users;

using TestingLayer.partonair.User.Abstracts;


namespace TestingLayer.partonair.User.Commands
{
    internal class AddUserCommandTest : UserBaseClassTest
    {
        private readonly AddUserCommandHandler _handler;
        public AddUserCommandTest() : base()
        {
            _handler = new AddUserCommandHandler(_mockUserRepo.Object);
        }


    }
}
