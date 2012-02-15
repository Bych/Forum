using Forum.Helpers;
using Forum.Services;
using StructureMap;

namespace Tests
{
    /// <summary>
    /// Bootstrapper for Test server
    /// </summary>
    public class Bootstrapper : IBootstrapper
    {
        /// <summary>
        /// StructureMap container
        /// </summary>
        public IContainer Container { get; private set; }

        /// <summary>
        /// Run bootstrapping logic
        /// </summary>
        public void BootstrapStructureMap()
        {
            Container = new Container();

            Container.Configure(config =>
                                     {
                                         config.For<IThemeService>().Use<ThemeService>();
                                         config.For<IPostService>().Use<PostService>();
                                         config.For<IMongoHelper>().Use<MongoHelperTest>();
                                     });
        }

        /// <summary>
        /// Convenient method to create new context
        /// </summary>
        public static IContainer Bootstrap()
        {
            var bootstrapper = new Bootstrapper();
            bootstrapper.BootstrapStructureMap();

            return bootstrapper.Container;
        }
    }
}