using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web.Mvc;
using Forum.Documents;
using Forum.Helpers;
using Forum.Services;
using Forum.Models;
using MongoDB.Bson;
using System.IO;
using MvcPaging;

[assembly: InternalsVisibleTo("Tests")]

namespace Forum.Controllers
{
    public class ThemeController : AuthenticationController
    {
        private const int DefaultPageSize = 10;
        private const string AnonymousName = "Anonymous";

        private ThemeDocument _themeDoc;

        private readonly IThemeService _themeService;
        private readonly IPostService _postService;


        public ThemeController(IThemeService themeService, IPostService postService)
        {
            _themeService = themeService;
            _postService = postService;
        }


        //
        // GET: /Theme/

        public ActionResult Index(int? page)
        {
            int currentPageIndex =
                page.HasValue ? page.Value - 1 : 0;

            var model = new ThemeListModel
                            {
                                Themes =
                                    _themeService.GetThemes(currentPageIndex, DefaultPageSize).Select(MapTheme).
                                    ToPagedList(currentPageIndex, DefaultPageSize, _themeService.GetThemesCount()),
                            };

            return View(model);
        }


        public ActionResult Detail(string id, int? page)
        {
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            _themeDoc = _themeService.GetTheme(id);
            if (_themeDoc != null)
            {
                var model = new PostListModel
                                {
                                    Posts =
                                        _postService.GetPosts(_themeDoc.ThemeId, currentPageIndex, DefaultPageSize).
                                        Select(MapPost).ToPagedList(currentPageIndex, DefaultPageSize,
                                                                    _postService.GetPostsCount(_themeDoc.ThemeId)),
                                };

                return View(model);
            }

            return View("Index");
        }


        [Authorize]
        [HttpPost]
        public ActionResult AddPost(PostModel postModel)
        {
            ObjectId themeId = ObjectId.TryParse(postModel.ThemeId, out themeId) ? themeId : ObjectId.Empty;
            _themeDoc = _themeService.GetTheme(themeId);

            if (ModelState.IsValid)
            {
                var newPost = new PostDocument
                                  {
                                      PostId = ObjectId.GenerateNewId(),
                                      Author = User == null ? AnonymousName : User.Identity.Name,
                                      Date = DateTime.Now,
                                      Details = postModel.Details,
                                  };

                _postService.AddPost(ObjectId.Parse(postModel.ThemeId), newPost);

                PostListItemModel postListItemModel = MapPost(newPost);

                return new JsonResult
                           {
                               Data = new
                                          {
                                              result = 1,
                                              postHtml = RenderPartialHelper.RenderPartialViewToString("PostListItem", postListItemModel, this),
                                              addHtml = RenderPartialHelper.RenderPartialViewToString("AddPost", new PostModel { ThemeId = postModel.ThemeId }, this),
                                          }
                           };
            }

            return new JsonResult
                       {
                           Data = new
                                      {
                                          result = 0,
                                          addHtml = RenderPartialHelper.RenderPartialViewToString("AddPost", postModel, this),
                                      }
                       };
        }

        [Authorize]
        [HttpPost]
        public ActionResult DeletePost(string themeId, string postId)
        {
            ObjectId tId = ObjectId.TryParse(themeId, out tId) ? tId : ObjectId.Empty;
            ObjectId pId = ObjectId.TryParse(postId, out pId) ? pId : ObjectId.Empty;
            _themeDoc = _themeService.GetTheme(tId);

            if (tId != ObjectId.Empty && pId != ObjectId.Empty)
            {
                _postService.DeletePost(tId, pId);
            }

            return new EmptyResult();
        }

        #region Helpers

        public ThemeListItemModel MapTheme(ThemeDocument themeDocument)
        {
            return new ThemeListItemModel
                       {
                           ThemeId = themeDocument.ThemeId,
                           Title = themeDocument.Title,
                           Url = themeDocument.Url,
                           Author = themeDocument.Author == String.Empty ? AnonymousName : themeDocument.Author,
                           TotalPosts = themeDocument.TotalPosts,
                           LastPostInfo = themeDocument.LastPostInfo,
                       };
        }

        private PostListItemModel MapPost(PostDocument postDocument)
        {
            return new PostListItemModel
                       {
                           PostId = postDocument.PostId,
                           Author = postDocument.Author,
                           Date = postDocument.Date,
                           Details = postDocument.Details,
                           ThemeId = _themeDoc != null ? _themeDoc.ThemeId : ObjectId.Empty,
                           ThemeTitle = _themeDoc != null ? _themeDoc.Title : String.Empty,
                       };
        }

        /*
        // taken from http://craftycodeblog.com/2010/05/15/asp-net-mvc-render-partial-view-to-string/
        protected string RenderPartialViewToString(string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = ControllerContext.RouteData.GetRequiredString("action");

            ViewData.Model = model;

            using (StringWriter sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                ViewContext viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }
         */

        #endregion

        //
        // GET: /Theme/Create
        [Authorize]
        public ActionResult Create()
        {
            return View(new ThemeModel());
        }

        //
        // POST: /Theme/Create
        [Authorize]
        [HttpPost]
        public ActionResult Create(ThemeModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var theme = new ThemeDocument
                                    {
                                        Title = model.Title,
                                        Details = model.Details,
                                        Author = User == null ? AnonymousName : User.Identity.Name,
                                        Date = DateTime.Now,
                                    };

                    _themeService.AddTheme(theme);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult DeleteTheme(string themeId)
        {
            ObjectId tId = ObjectId.TryParse(themeId, out tId) ? tId : ObjectId.Empty;

            if (tId != ObjectId.Empty)
            {
                _themeService.DeleteTheme(tId);
            }

            return new EmptyResult();
        }

        //
        // GET: /Theme/About
        public ActionResult About()
        {
            return View();
        }
    }
}