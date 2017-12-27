using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Es.Udc.DotNet.MiniPortal.Model.EventDao;
using System.Transactions;
using Es.Udc.DotNet.MiniPortal.Model;
using Ninject;
using System.Collections.Generic;
using Es.Udc.DotNet.MiniPortal.Model.CategoryDao;

namespace Es.Udc.DotNet.MiniPortal.Test
{
    [TestClass]
    public class IEventDaoTest
    {

        private static IKernel kernel;
        private static IEventDao eventDao;
        private static ICategoryDao categoryDao;
        private static Event myEvent;
        private static Category category;

        // Variables used in several tests are initialized here
        private const long categoryId = 3;
        private const String name = "Liga BBVA";
        private const String review = "La liga más valorada a nivel mundial";
        private DateTime date = DateTime.Now;
        private const long NON_EXISTENT_Event_ID = -1;

        private const String categoryName = "Futbol";

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
        }

        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            transaction.Dispose();
        }

        #endregion

        /// <summary>
        ///A test for FindByName
        ///</summary>
        [TestMethod()]
        public void DAO_FindByKeywordsAndCategoryTest()
        {
            try
            {
                String name1 = "li";
                String[] keywords = name1.Split();
                ICollection<Event> events = eventDao.FindByKeywordsAndCategory(keywords, myEvent.Category.categoryId, 0, 10);
                ICollection<Event> events2 = eventDao.FindByKeywordsAndCategory(keywords, -1, 0, 10);

                Assert.AreEqual(1, events.Count, "Event found does not correspond with the original one.");
                Assert.AreEqual(1, events2.Count, "Event found does not correspond with the original one.");
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

        }
    }
}
