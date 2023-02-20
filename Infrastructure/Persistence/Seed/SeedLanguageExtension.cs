using Domain.Entities.Languages;

namespace Infrastructure.Persistence.Seed;

public static class SeedLanguageExtension
{
    public static async Task SeedLanguage(this AppDbContext dbContext)
    {
        var languages = new List<Language>()
        {
            new()
            {
                Code = "en-us",
            },
            new()
            {
                Code = "ru-ru",
            },
            new()
            {
                Code = "ky-kg",
            }
        };

        foreach (var language in languages)
        {
            var entity = dbContext.Languages
                .FirstOrDefault(y => y.Code == language.Code);
            if (entity is null)
            {
                await dbContext.Languages.AddAsync(language);
            }
        }
    }
    
}
