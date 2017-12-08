using LanguageBuilder.Data.Models;

namespace LanguageBuilder.Services.Contracts
{
    public interface ISubscriptionService : IRepository<Subscription, int>, IAsyncRepository<Subscription, int>
    {
    }
}
