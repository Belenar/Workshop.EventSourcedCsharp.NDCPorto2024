using BeerSender.Domain;

namespace BeerSender.Tests;

public abstract class CommandHandlerTest<TCommand> : TestBase
{
    protected abstract CommandHandler<TCommand> Handler { get; }

    protected void When(TCommand command)
    {
        Handler.Handle(command);
    }
}