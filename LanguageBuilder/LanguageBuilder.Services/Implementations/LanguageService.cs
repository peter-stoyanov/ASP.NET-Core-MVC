using System;
using System.Collections.Generic;
using System.Text;
using LanguageBuilder.Data.Models;
using LanguageBuilder.Data;
using LanguageBuilder.Services.Contracts;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LanguageBuilder.Services.Implementations
{
    public class LanguageService : ILanguageService
    {
        private readonly LanguageBuilderDbContext _db;

        public LanguageService(LanguageBuilderDbContext context)
        {
            _db = context;
        }

        public async Task<IEnumerable<Language>> GetAllAsync()
        {
            return await _db
                .Languages
                .ToListAsync();
        }

    }
}