using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Transactions;
using Es.Udc.DotNet.MiniPortal.Model;
using Ninject;
using System.Collections.Generic;
using Es.Udc.DotNet.MiniPortal.Model.UserGroup1Dao;

namespace Es.Udc.DotNet.MiniPortal.Test
{
    [TestClass]
    public class IUserGroupDaoTest
    {

        private static IKernel kernel;
        private static IUserGroup1Dao userGroupDao;

        private UserGroup userGroup;

        // Variables used in several tests are initialized here
        private const String name = "Group Fifa";
        private const String description = "La liga más valorada a nivel mundial";
        private const long NON_EXISTENT_Group_ID = -1;

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
            userGroupDao = kernel.Get<IUserGroup1Dao>();
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

            userGroup = new UserGroup();
            userGroup.name = name;
            userGroup.description = description;
            userGroup.UserProfiles = new List<UserProfile>();
            userGroup.Recommendations = new List<Recommendation>();

            userGroupDao.Create(userGroup);
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
        public void UserGroupDAO_FindByNameTest()
        {
            try
            {
                UserGroup groupActual = userGroupDao.FindByName(name);

                Assert.AreEqual(groupActual, userGroup, "Group found does not correspond with the original one.");
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

        }
    }
}
