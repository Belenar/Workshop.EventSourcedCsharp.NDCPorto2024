using BeerSender.Domain;
using BeerSender.Domain.Boxes;
using BeerSender.Domain.Boxes.CommandHandlers;
using BeerSender.Web.EventPersistence;

namespace BeerSender.Web.Extensions
{
    public static class DomainExtensions
    {
        public static void RegisterDomain(this IServiceCollection services)
        {
            // TODO: Scan handlers with reflection instead of registering them manually
            services.AddTransient<CommandHandler<CreateBox>, BoxCreator>();
            services.AddTransient<CommandHandler<AddBeerBottle>, BeerBottleAdder>();
            services.AddTransient<CommandHandler<CloseBox>, BoxCloser>();
            services.AddTransient<CommandHandler<AddShippingLabel>, ShippingLabelAdder>();
            services.AddTransient<CommandHandler<SendBox>, BoxSender>();

            services.AddScoped<IEventStore, EventStore>();
            services.AddScoped<CommandRouter>();
        }
    }
}
