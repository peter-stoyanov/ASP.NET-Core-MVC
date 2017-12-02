using AutoMapper;

namespace LanguageBuilder.Services.Mapping
{
    public interface IHaveCustomMapping
    {
        void ConfigureMapping(Profile mapper);
    }
}
