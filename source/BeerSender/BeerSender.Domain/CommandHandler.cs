namespace BeerSender.Domain;

public abstract class CommandHandler<TCommand>()
{
    public abstract void Handle(TCommand command);
}