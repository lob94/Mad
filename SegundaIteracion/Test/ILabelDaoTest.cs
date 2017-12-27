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
    public class ILabelDaoTest
    {

        private static IKernel kernel;
        private static ILabelDao labelDao;
        private static Label label;

        // Variables used in several tests are initialized here
        private const String labelName = "Etiqueta";

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
            labelDao = kernel.Get<ILabelDao>();

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
            
            label = new Label();
            label.name = labelName;

            labelDao.Create(label);
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
        public void DAO_FindByNameTest()
        {
            try
            {
                Label l = labelDao.Find(label.labelId);
                Label l2 = labelDao.FindByName("Etiqueta");
                Assert.AreEqual(l.name,l2.name, "Label found does not correspond with the original one.");
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

        }
    }
}
