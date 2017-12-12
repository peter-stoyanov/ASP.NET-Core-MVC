using LanguageBuilder.Data.Models;
using LanguageBuilder.Services.Models.Search;

namespace LanguageBuilder.Services.Models.UsersSearch
{
    public class Request : BaseRequest<User>
    {
        public string Keywords { get; set; }
    }
}
