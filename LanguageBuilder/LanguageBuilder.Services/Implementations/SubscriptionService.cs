using LanguageBuilder.Data;
using LanguageBuilder.Data.Models;
using LanguageBuilder.Data.Services;
using LanguageBuilder.Services.Contracts;

namespace LanguageBuilder.Services.Implementations
{
    public class SubscriptionService : BaseRepository<Subscription, int>, ISubscriptionService
    {
        private readonly LanguageBuilderDbContext _db;

        public SubscriptionService(LanguageBuilderDbContext context)
            : base(context)
        {
            _db = context;
        }
    }
}
