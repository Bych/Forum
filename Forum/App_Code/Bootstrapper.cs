using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Forum.Helpers;
using StructureMap;
using Forum.Services;
using System.Web.Mvc;

namespace Forum.App_Code
{
    public class Bootstrapper
    {
        public static void Bootstrap()
        {
            IContainer container = new Container(x =>
            {
                x.For<IThemeService>().Use<ThemeService>();
                x.For<IPostService>().Use<PostService>();
                x.For<IMongoHelper>().Use<MongoHelper>().Ctor<String>().Is("Mongo");
                x.For<IResumeService>().Use<ResumeService>();
            });
         
            DependencyResolver.SetResolver(new StructureMapDependencyResolver(container));
        }
    }
}