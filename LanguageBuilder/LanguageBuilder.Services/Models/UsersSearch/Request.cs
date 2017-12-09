using LanguageBuilder.Data.Models;
using LanguageBuilder.Services.Models.Search;
using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageBuilder.Services.Models.UsersSearch
{ 
    public class Request : BaseRequest<User>
    {
        public string Keywords { get; set; }
    }
}
