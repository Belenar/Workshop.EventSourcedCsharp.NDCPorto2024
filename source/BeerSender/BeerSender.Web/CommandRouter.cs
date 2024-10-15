using BeerSender.Domain;

namespace BeerSender.Web;

public class CommandRouter(IServiceProvider serviceProvider, IEventStore store)
{
    public void HandleCommand(object command)
    {
        var commandType = command.GetType();

        var handlerType = typeof(CommandHandler<>).MakeGenericType(commandType);

        var handler = serviceProvider.GetService(handlerType);

        var methodInfo = handlerType.GetMethod("Handle");

        methodInfo.Invoke(handler, new object[] { command });

        store.SaveChanges();
    }
    
}