using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Es.Udc.DotNet.MiniPortal.Model.EventDao;
using System.Transactions;
using Es.Udc.DotNet.MiniPortal.Model;
using Es.Udc.DotNet.MiniPortal.Model.UserService.Util;
using Ninject;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using Es.Udc.DotNet.MiniPortal.Model.CategoryDao;
using Es.Udc.DotNet.MiniPortal.Model.CommentDao;
using Es.Udc.DotNet.MiniPortal.Model.EventService;
using Es.Udc.DotNet.MiniPortal.Model.UserProfileDao;
using Es.Udc.DotNet.MiniPortal.Model.LabelDao;

namespace Es.Udc.DotNet.MiniPortal.Test
{
    [TestClass]
    public class ICommentDaoTest
    {

        private static IKernel kernel;
        private static ICommentDao commentDao;
        private static ICategoryDao categoryDao;
        private static IEventDao eventDao;
        private static IUserProfileDao userProfileDao;
        private static ILabelDao labelDao;
        private static IEventService eventService;
        private static Event myEvent;
        private static Comment comment;
        private static Comment comment2;
        private static Category category;
        private static Label label;
        private static UserProfile userProfile;

        // Variables used in several tests are initialized here
        private const long categoryId = 3;
        private const String name = "Liga BBVA";
        private const String review = "La liga más valorada a nivel mundial";
        private DateTime date = DateTime.Now;
        private const long NON_EXISTENT_Event_ID = -1;

        private const String content = "La liga con los mejores jugadores";

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
            commentDao = kernel.Get<ICommentDao>();
            userProfileDao = kernel.Get<IUserProfileDao>();
            labelDao = kernel.Get<ILabelDao>();
            eventService = kernel.Get<IEventService>();

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

            label = new Label();
            label.name = categoryName;

            labelDao.Create(label);

            comment = new Comment();
            comment.content = content;
            comment.Event = myEvent;
            comment.Labels = new List<Label>();
            comment.loginName = userProfile.loginName;
            comment.UserProfile = userProfile;
            comment.commentDate = DateTime.Now;

            commentDao.Create(comment);

            comment2 = new Comment();
            comment2.content = content;
            comment2.Event = myEvent;
            comment2.Labels = new List<Label>();
            comment2.Labels.Add(label);
            comment2.loginName = userProfile.loginName;
            comment2.UserProfile = userProfile;
            comment2.commentDate = date;

            commentDao.Create(comment2);

        }

        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            transaction.Dispose();
        }

        #endregion

        /// <summary>
        ///A test for FindCommentsOrderByDate
        ///</summary>
        [TestMethod()]
        public void DAO_FindCommentsOrderByDateTest()
        {
            try
            {
                ICollection<Comment> comments = new List<Comment>();
                comments.Add(comment);
                comments.Add(comment2);
                ICollection<Comment> commentsActual = commentDao.FindCommentsOrderByDate(myEvent.eventId,0,2);

                Assert.AreEqual(2,commentsActual.Count, "Event found does not correspond with the original one.");
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

        }
    }
}
