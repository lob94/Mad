using Ninject;

using System.Data.Entity;
using System.Configuration;
using Es.Udc.DotNet.ModelUtil.IoC;

using Es.Udc.DotNet.MiniPortal.Model.UserProfileDao;
using Es.Udc.DotNet.MiniPortal.Model.UserService;
using Es.Udc.DotNet.MiniPortal.Model.EventDao;
using Es.Udc.DotNet.MiniPortal.Model.EventService;
using Es.Udc.DotNet.MiniPortal.Model.CategoryDao;
using Es.Udc.DotNet.MiniPortal.Model.CommentDao;
using Es.Udc.DotNet.MiniPortal.Model.LabelDao;
using Es.Udc.DotNet.MiniPortal.Model.UserGroup1Dao;
using Es.Udc.DotNet.MiniPortal.Model.RecommendationDao;

namespace Es.Udc.DotNet.MiniPortal.HTTP.Util.IoC
{
    class IoCManagerNinject : IIoCManager
    {
        private static IKernel kernel;
        private static NinjectSettings settings;
        public void Configure()
        {
            settings = new NinjectSettings() { LoadExtensions = true };
            kernel = new StandardKernel(settings);

            /* UserProfileDao */
            kernel.Bind<IUserProfileDao>().
                To<UserProfileDaoEntityFramework>();

            /* UserService */
            kernel.Bind<IUserService>().
                To<UserService>();

            kernel.Bind<IEventDao>().
                To<EventDaoEntityFramework>();

            kernel.Bind<IEventService>().
                To<EventService>();

            kernel.Bind<ICategoryDao>().
                To<CategoryDaoEntityFramework>();

            kernel.Bind<ICommentDao>().
                To<CommentDaoEntityFramework>();

            kernel.Bind<ILabelDao>().
                To<LabelDaoEntityFramework>();

            kernel.Bind<IUserGroup1Dao>().
                To<UserGroup1DaoEntityFramework>();

            kernel.Bind<IRecommendationDao>().
                To<RecommendationDaoEntityFramework>();

            kernel.Bind<ICachingProvider>().
                To<CachingProvider>();

            /* DbContext */
            string connectionString =
                ConfigurationManager.ConnectionStrings["MiniPortalEntities"].ConnectionString;

            kernel.Bind<DbContext>().
                ToSelf().
                InSingletonScope().
                WithConstructorArgument("nameOrConnectionString", connectionString);
        }

        public T Resolve<T>()
        {
            return kernel.Get<T>();
        }
    }
}
