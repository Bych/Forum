using System;
using System.Collections.Generic;
using System.Linq;
using Forum.Controllers;
using Forum.Documents;
using Forum.Models;
using Forum.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using Moq;
using MvcPaging;

namespace Tests
{
    [TestClass]
    public class ForumTest
    {
        private ThemeController _themeController;
        private IThemeService _themeService;
        private IPostService _postService;

        [TestInitialize]
        public void Init()
        {
            var container = Bootstrapper.Bootstrap();

            _themeService = container.GetInstance<IThemeService>();
            _postService = container.GetInstance<IPostService>();
            _themeController = new ThemeController(_themeService, _postService);
        }

        [TestMethod]
        public void ThemeCreateTest()
        {
            var model = new ThemeModel
                            {
                                Details = "Theme details",
                                Title = "Theme title",
                            };

            
            // count base records number
            var baseThemesCount = _themeService.GetThemesCount();
            _themeController.Create(model);

            // demand a recount
            var newThemesCount = _themeService.GetThemesCount();
            Assert.AreEqual(newThemesCount, baseThemesCount + 1); // verify the expected number after new record insert
        }

        [TestMethod]
        public void ThemeListModelTest()
        {
            const int currentPageIndex = 0;
            const int defaultPageSize = 10;
            const string anonymousName = "Anonymous";

            var themeService = GetThemeService();

            var model = new ThemeListModel
            {
                Themes =
                    themeService.GetThemes(currentPageIndex, defaultPageSize).Select(_themeController.MapTheme).
                    ToPagedList(currentPageIndex, defaultPageSize, themeService.GetThemesCount()),
            };

            Assert.AreNotEqual(model.Themes[0].Author, anonymousName);
            Assert.AreEqual(model.Themes[1].Author, anonymousName);
        }


        private IThemeService GetThemeService()
        {
            #region themeDocumentsInit
            var themeDocuments = new List<ThemeDocument>
                                     {
                                         new ThemeDocument
                                             {
                                                 Author = "T_Author1",
                                                 Date = DateTime.Now,
                                                 Details = "Theme Details 1",
                                                 LastPostInfo = String.Format("{0}, {1}", "Author1", DateTime.Now),
                                                 Posts = new List<PostDocument>(),
                                                 ThemeId = ObjectId.GenerateNewId(),
                                                 Title = "Title 1",
                                                 TotalPosts = 1,
                                                 Url = "title-1",
                                             },
                                         new ThemeDocument
                                             {
                                                 Author = String.Empty,
                                                 Date = DateTime.Now,
                                                 Details = "Theme Details 2",
                                                 LastPostInfo = String.Format("{0}, {1}", "Author2", DateTime.Now),
                                                 Posts = new List<PostDocument>(),
                                                 ThemeId = ObjectId.GenerateNewId(),
                                                 Title = "Title 2",
                                                 TotalPosts = 1,
                                                 Url = "title-2",
                                             }
                                     };
            #endregion

            var mockThemeService = new Mock<IThemeService>();
            mockThemeService.Setup(themeService => themeService.GetThemes(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(themeDocuments);
            mockThemeService.Setup(themeService => themeService.GetThemesCount()).Returns(themeDocuments.Count);

            return mockThemeService.Object;
        }
    }

}