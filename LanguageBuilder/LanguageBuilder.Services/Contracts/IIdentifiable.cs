using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageBuilder.Services
{
    public interface BaseEntity<Tkey>
    {
        Tkey Id { get; }
    }
}
