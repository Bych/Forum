using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Forum.Documents;
using MongoDB.Bson;

namespace Forum.Services
{
    public interface IThemeService
    {
        void AddTheme(ThemeDocument theme);

        void DeleteTheme(ObjectId themeId);

        IEnumerable<ThemeDocument> GetThemes(int pageIndex, int pageSize);

        int GetThemesCount();

        ThemeDocument GetTheme(ObjectId themeId);

        ThemeDocument GetTheme(string url);

    }
}
