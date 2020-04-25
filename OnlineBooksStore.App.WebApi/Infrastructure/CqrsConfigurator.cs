using Microsoft.Extensions.DependencyInjection;
using OnlineBooksStore.App.Handlers.Command;
using OnlineBooksStore.App.Handlers.Query;

namespace OnlineBooksStore.App.WebApi.Infrastructure
{
    internal static class CqrsConfigurator
    {
        internal static IServiceCollection AddCommandsAndQueries(this IServiceCollection services)
        {
            services.AddTransient<BookQueryHandler>();
            services.AddTransient<CategoryQueryHandler>();
            services.AddTransient<PublisherQueryHandler>();
            services.AddTransient<OrderQueryHandler>();

            services.AddTransient<BookCommandHandler>();
            services.AddTransient<CategoryCommandHandler>();
            services.AddTransient<PublisherCommandHandler>();
            services.AddTransient<OrderCommandHandler>();
            services.AddTransient<TablesCommandHandlers>();

            return services;
        }
    }
}
