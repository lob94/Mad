using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Es.Udc.DotNet.MiniPortal.Model.EventDao;
using System.Transactions;
using Es.Udc.DotNet.MiniPortal.Model;
using Es.Udc.DotNet.MiniPortal.Model.UserService.Util;
using Ninject;
using System.Collections.Generic;
using Es.Udc.DotNet.MiniPortal.Model.CategoryDao;
using Es.Udc.DotNet.MiniPortal.Model.EventService;
using Es.Udc.DotNet.MiniPortal.Model.UserProfileDao;
using Es.Udc.DotNet.MiniPortal.Model.LabelDao;
using Es.Udc.DotNet.MiniPortal.Model.RecommendationDao;
using Es.Udc.DotNet.MiniPortal.Model.UserGroup1Dao;

namespace Es.Udc.DotNet.MiniPortal.Test
{
    [TestClass]
    public class IRecommendationDaoTest
    {

        private static IKernel kernel;
        private static IRecommendationDao recommendationDao;
        private static ICategoryDao categoryDao;
        private static IEventDao eventDao;
        private static IUserProfileDao userProfileDao;
        private static ILabelDao labelDao;
        private static IUserGroup1Dao userGroupDao;
        private static IEventService eventService;
        private static Event myEvent;
        private static Category category;
        private static UserProfile userProfile;
        private static UserGroup userGroup;
        private static Recommendation recommendation1;
        private static Recommendation recommendation2;

        // Variables used in several tests are initialized here
        private const long categoryId = 3;
        private const String name = "Liga BBVA";
        private const String review = "La liga más valorada a nivel mundial";
        private DateTime date = DateTime.Now;
        private const long NON_EXISTENT_Event_ID = -1;

        private const String categoryName = "Futbol";

        private const String userLoginName = "loginNameTest";
        private const String userLoginName2 = "loginNameTest2";
        private const String clearPassword = "password";
        private const String firstName = "name";
        private const String lastName = "lastName";
        private const String email = "user@udc.es";
        private const String language = "es";
        private const String country = "ES";
        private const long NON_EXISTENT_USER_ID = -1;

        private const String recom = "Buenisima";

        private const String groupName = "Grupo";
        private const String groupDescription = "Description";


        private TransactionScope transaction;

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes

        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            kernel = TestManager.ConfigureNInjectKernel();
            eventDao = kernel.Get<IEventDao>();
            categoryDao = kernel.Get<ICategoryDao>();
            userProfileDao = kernel.Get<IUserProfileDao>();
            labelDao = kernel.Get<ILabelDao>();
            eventService = kernel.Get<IEventService>();
            userGroupDao = kernel.Get<IUserGroup1Dao>();
            recommendationDao = kernel.Get<IRecommendationDao>();

        }

        //Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            TestManager.ClearNInjectKernel(kernel);
        }

        //Use TestInitialize to run code before running each test
        [TestInitialize()]
        public void MyTestInitialize()
        {
            transaction = new TransactionScope();

            userProfile = new UserProfile();
            userProfile.loginName = userLoginName;
            userProfile.enPassword = PasswordEncrypter.Crypt(clearPassword);
            userProfile.firstName = firstName;
            userProfile.lastName = lastName;
            userProfile.email = email;
            userProfile.language = language;
            userProfile.country = country;

            userProfileDao.Create(userProfile);

            category = new Category();
            category.name = categoryName;

            categoryDao.Create(category);

            myEvent = new Event();
            myEvent.categoryId = categoryId;
            myEvent.name = name;
            myEvent.eventDate = date;
            myEvent.review = review;
            myEvent.Category = category;
            myEvent.Comments = new List<Comment>();
            myEvent.Recommendations = new List<Recommendation>();

            eventDao.Create(myEvent);

            userGroup = new UserGroup();
            userGroup.name = groupName;
            userGroup.description = groupDescription;
            userGroup.UserProfiles = new List<UserProfile>();
            userGroup.UserProfiles.Add(userProfile);
            userGroup.Recommendations = new List<Recommendation>();

            userGroupDao.Create(userGroup);


            recommendation1 = new Recommendation();
            recommendation1.reason = recom;
            recommendation1.Event = myEvent;
            recommendation1.UserProfile = userProfile;
            recommendation1.UserGroup = userGroup;
            recommendation1.created = DateTime.Now;

            recommendationDao.Create(recommendation1);

            recommendation2 = new Recommendation();
            recommendation2.Event = myEvent;
            recommendation2.reason = recom;
            recommendation2.UserProfile = userProfile;
            recommendation2.UserGroup = userGroup;
            recommendation2.created = DateTime.Now;

            recommendationDao.Create(recommendation2);
        }

        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            transaction.Dispose();
        }

        #endregion

        /// <summary>
        ///A test for FindByGroupId
        ///</summary>
        [TestMethod()]
        public void DAO_FindByGroupId()
        {
            try
            {
                ICollection<Recommendation> recommendations = new List<Recommendation>();
                recommendations.Add(recommendation1);
                recommendations.Add(recommendation2);
                ICollection<Recommendation> recomms = recommendationDao.FindByGroupId(userGroup.groupId, 0, 3);

                Assert.AreEqual(recommendations.Count, recomms.Count, "Event found does not correspond with the original one.");
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

        }
    }
}
