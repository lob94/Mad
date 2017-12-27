using System.Configuration;
using Ninject;
using Ninject.Extensions.Interception;
using Es.Udc.DotNet.MiniPortal.Model.UserProfileDao;
using Es.Udc.DotNet.MiniPortal.Model.UserService;
using System.Data.Entity;
using System.Reflection;
using Es.Udc.DotNet.MiniPortal.Model.EventDao;
using Es.Udc.DotNet.MiniPortal.Model.CategoryDao;
using Es.Udc.DotNet.MiniPortal.Model.EventService;
using Es.Udc.DotNet.MiniPortal.Model.CommentDao;
using Es.Udc.DotNet.MiniPortal.Model.UserGroup1Dao;
using Es.Udc.DotNet.MiniPortal.Model.LabelDao;
using Es.Udc.DotNet.MiniPortal.Model.RecommendationDao;

namespace Es.Udc.DotNet.MiniPortal.Test
{
    public class TestManager
    {

        /// <summary>
        /// Configures and populates the Ninject kernel
        /// </summary>
        /// <returns>The NInject kernel</returns>
        public static IKernel ConfigureNInjectKernel()
        {
            NinjectSettings settings = new NinjectSettings() { LoadExtensions = true };

            IKernel kernel = new StandardKernel(settings);

            kernel.Bind<IUserProfileDao>().
                To<UserProfileDaoEntityFramework>();

            kernel.Bind<IUserService>().
                To<UserService>();

            kernel.Bind<IEventService>().
                To<EventService>();

            kernel.Bind<IUserGroup1Dao>().
                To<UserGroup1DaoEntityFramework>();

            kernel.Bind<IEventDao>().
                To<EventDaoEntityFramework>();

            kernel.Bind<ICommentDao>().
                To<CommentDaoEntityFramework>();

            kernel.Bind<ICategoryDao>().
                To<CategoryDaoEntityFramework>();

            kernel.Bind<ILabelDao>().
                To<LabelDaoEntityFramework>();

            kernel.Bind<IRecommendationDao>().
                To<RecommendationDaoEntityFramework>();

            string connectionString =
                ConfigurationManager.ConnectionStrings["MiniPortalEntities"].ConnectionString;

            kernel.Bind<DbContext>().
                ToSelf().
                InSingletonScope().
                WithConstructorArgument("nameOrConnectionString", connectionString);
                                                    
            return kernel;
        }


        /// <summary>
        /// Configures the Ninject kernel from an external module file.
        /// </summary>
        /// <param name="moduleFilename">The module filename.</param>
        /// <returns>The NInject kernel</returns>
        public static IKernel ConfigureNInjectKernel(string moduleFilename)
        {
            IKernel kernel = new StandardKernel();
            kernel.Load(moduleFilename);

            return kernel;
        }


        public static void ClearNInjectKernel(IKernel kernel) {

            kernel.Dispose();
        }

    }
}
