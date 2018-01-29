using System.Collections.Generic;
using Es.Udc.DotNet.MiniPortal.Model.UserService;
using Es.Udc.DotNet.MiniPortal.Model.UserService.Exceptions;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Es.Udc.DotNet.MiniPortal.Model;
using Es.Udc.DotNet.MiniPortal.Model.UserProfileDao;
using System;
using System.Transactions;
using Es.Udc.DotNet.MiniPortal.Model.UserService.Util;
using Ninject;
using Es.Udc.DotNet.MiniPortal.Model.EventService;
using Es.Udc.DotNet.MiniPortal.Model.EventDao;
using Es.Udc.DotNet.MiniPortal.Model.CategoryDao;
using Es.Udc.DotNet.MiniPortal.Model.UserGroup1Dao;
using Es.Udc.DotNet.MiniPortal.Model.CommentDao;

namespace Es.Udc.DotNet.MiniPortal.Test
{

    /// <summary>
    ///This is a test class for IUserServiceTest and is intended
    ///to contain all IUserServiceTest Unit Tests
    ///</summary>
    [TestClass()]
    public class IUserServiceTest
    {

        private static IKernel kernel;
        private static IUserService userService;
        private static IUserProfileDao userProfileDao;
        private static IEventDao eventDao;
        private static IEventService eventService;
        private static ICategoryDao categoryDao;
        private static IUserGroup1Dao userGroupDao;

        // Variables used in several tests are initialized here
        private const String loginName = "loginNameTest";
        private const String clearPassword = "password";
        private const String firstName = "name";
        private const String lastName = "lastName";
        private const String email = "user@udc.es";
        private const String language = "es";
        private const String country = "ES";
        private const long NON_EXISTENT_USER_ID = -1;

        private const String name = "Liga BBVA";
        private const String description = "Grupo de fútbol de la liga BBVA";
        private const String review = "La liga más valorada a nivel mundial";
        private DateTime date = DateTime.Now;
        private const String content = "La liga con los mejores jugadores";
        private const long NON_EXISTENT_Event_ID = -1;
        private const String categoryName = "Futbol total";
        private const String keywords = "myEv";
        private const String label = "jajajaja muy bueno!";

        TransactionScope transaction;

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

            userProfileDao = kernel.Get<IUserProfileDao>();
            userService = kernel.Get<IUserService>();
            eventService = kernel.Get<IEventService>();
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

            /*category = new Category();
            category.name = categoryName;

            categoryDao.Create(category);

            myEvent = new Event();
            myEvent.categoryId = category.categoryId;
            myEvent.name = name;
            myEvent.eventDate = date;
            myEvent.review = review;
            myEvent.Category = category;
            //myEvent.Recommendations = new List<Recommendation>();

            eventDao.Create(myEvent);

            comment = new Comment();
            comment.content = content;
            comment.Event = myEvent;
            //comment.Labels = new List<Label>();
            comment.UserProfile = userProfile;
            comment.loginName = loginName;

            commentDao.Create(comment);*/
        }

        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
        }

        #endregion

        /// <summary>
        ///A test for RegisterUser
        ///</summary>
        [TestMethod()]
        public void RegisterUserTest()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // Register user and find profile
                long userId =
                    userService.RegisterUser(loginName, clearPassword,
                    new UserProfileDetails(firstName, lastName, email, language, country));

                UserProfile userProfile = userProfileDao.Find(userId);

                // Check data
                Assert.AreEqual(userId, userProfile.usrId);
                Assert.AreEqual(loginName, userProfile.loginName);
                Assert.AreEqual(PasswordEncrypter.Crypt(clearPassword), userProfile.enPassword);
                Assert.AreEqual(firstName, userProfile.firstName);
                Assert.AreEqual(lastName, userProfile.lastName);
                Assert.AreEqual(email, userProfile.email);
                Assert.AreEqual(language, userProfile.language);
                Assert.AreEqual(country, userProfile.country);

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        /// <summary>
        ///A test for registering a user that already exists in the database
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(DuplicateInstanceException))]
        public void RegisterDuplicatedUserTest()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // Register user
                userService.RegisterUser(loginName, clearPassword,
                    new UserProfileDetails(firstName, lastName, email, language, country));

                // Register the same user
                userService.RegisterUser(loginName, clearPassword,
                    new UserProfileDetails(firstName, lastName, email, language, country));

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        ///// <summary>
        /////A test for Login with clear password
        /////</summary>
        [TestMethod()]
        public void LoginClearPasswordTest()
        {

            using (TransactionScope scope = new TransactionScope())
            {
                // Register user
                long userId = userService.RegisterUser(loginName, clearPassword,
                    new UserProfileDetails(firstName, lastName, email, language, country));

                LoginResult expected = new LoginResult(userId, firstName,
                    PasswordEncrypter.Crypt(clearPassword), language, country);

                // Login with clear password
                LoginResult actual =
                       userService.Login(loginName,
                       clearPassword, false);

                // Check data
                Assert.AreEqual(expected, actual);

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        ///// <summary>
        /////A test for Login with encrypted password
        /////</summary>
        [TestMethod()]
        public void LoginEncryptedPasswordTest()
        {

            using (TransactionScope scope = new TransactionScope())
            {
                // Register user
                long userId = userService.RegisterUser(loginName, clearPassword,
                    new UserProfileDetails(firstName, lastName, email, language, country));

                LoginResult expected = new LoginResult(userId, firstName,
                    PasswordEncrypter.Crypt(clearPassword), language, country);

                // Login with encrypted password
                LoginResult obtained =
                       userService.Login(loginName,
                       PasswordEncrypter.Crypt(clearPassword), true);

                // Check data
                Assert.AreEqual(expected, obtained);

                // transaction.Complete() is not called, so Rollback is executed.

            }
        }

        ///// <summary>
        /////A test for Login with incorrect password
        /////</summary>
        [TestMethod()]
        [ExpectedException(typeof(IncorrectPasswordException))]
        public void LoginIncorrectPasswordTest()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // Register user
                long userId = userService.RegisterUser(loginName, clearPassword,
                    new UserProfileDetails(firstName, lastName, email, language, country));

                // Login with incorrect (clear) password
                LoginResult actual =
                       userService.Login(loginName, clearPassword + "X", false);

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        ///// <summary>
        /////A test for Login with a non-existing user
        /////</summary>
        [TestMethod()]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void LoginNonExistingUserTest()
        {
            // Login for a user that has not been registered
            LoginResult actual =
                   userService.Login(loginName, clearPassword, false);
        }

        /// <summary>
        ///A test for FindUserProfileDetails
        ///</summary>
        [TestMethod()]
        public void FindUserProfileDetailsTest()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                UserProfileDetails expected =
                    new UserProfileDetails(firstName, lastName, email, language, country);

                long userId =
                    userService.RegisterUser(loginName, clearPassword, expected);

                UserProfileDetails obtained =
                       userService.FindUserProfileDetails(userId);

                // Check data
                Assert.AreEqual(expected, obtained);

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        /// <summary>
        ///A test for FindUserProfileDetails when the user does not exist
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void FindUserProfileDetailsForNonExistingUserTest()
        {
            userService.FindUserProfileDetails(NON_EXISTENT_USER_ID);
        }

        /// <summary>
        ///A test for UpdateUserProfileDetails
        ///</summary>
        [TestMethod()]
        public void UpdateUserProfileDetailsTest()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // Register user and update profile details
                long userId = userService.RegisterUser(loginName, clearPassword,
                    new UserProfileDetails(firstName, lastName, email, language, country));

                UserProfileDetails expected =
                        new UserProfileDetails(firstName + "X", lastName + "X",
                            email + "X", "XX", "XX");

                userService.UpdateUserProfileDetails(userId, expected);

                UserProfileDetails obtained =
                    userService.FindUserProfileDetails(userId);

                // Check changes
                Assert.AreEqual(expected, obtained);

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        /// <summary>
        ///A test for UpdateUserProfileDetails when the user does not exist
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void UpdateUserProfileDetailsForNonExistingUserTest()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                userService.UpdateUserProfileDetails(NON_EXISTENT_USER_ID,
                    new UserProfileDetails(firstName, lastName, email, language, country));

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        /// <summary>
        ///A test for ChangePassword
        ///</summary>
        [TestMethod()]
        public void ChangePasswordTest()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // Register user
                long userId = userService.RegisterUser(loginName, clearPassword,
                    new UserProfileDetails(firstName, lastName, email, language, country));

                // Change password
                String newClearPassword = clearPassword + "X";
                userService.ChangePassword(userId, clearPassword, newClearPassword);

                // Try to login with the new password. If the login is correct, then
                // the password was successfully changed.
                userService.Login(loginName, newClearPassword, false);

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        /// <summary>
        ///A test for ChangePassword entering a wrong old password
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(IncorrectPasswordException))]
        public void ChangePasswordWithIncorrectPasswordTest()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // Register user
                long userId = userService.RegisterUser(loginName, clearPassword,
                    new UserProfileDetails(firstName, lastName, email, language, country));

                // Change password
                String newClearPassword = clearPassword + "X";
                userService.ChangePassword(userId, clearPassword + "Y", newClearPassword);

                // transaction.Complete() is not called, so Rollback is executed.
            }
        }

        /// <summary>
        ///A test for ChangePassword when the user does not exist
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void ChangePasswordForNonExistingUserTest()
        {
            userService.ChangePassword(NON_EXISTENT_USER_ID,
                clearPassword, clearPassword + "X");
        }

        /// <summary>
        ///A test for AddRecommendation when the event does not exist
        ///</summary>
        [TestMethod()]
        public void AddRecommendationTest()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                ICollection<long> groups = new List<long>();
                long a = userService.RegisterUser("Patata", clearPassword,
                    new UserProfileDetails(firstName, lastName, email, language, country));

                Category category = new Category();
                category.name = categoryName;
                categoryDao.Create(category);

                Event myEvent = new Event();
                myEvent.categoryId = category.categoryId;
                myEvent.name = name;
                myEvent.eventDate = date;
                myEvent.review = review;
                myEvent.Category = category;
                eventDao.Create(myEvent);

                ICollection<Recommendation> r = userService.AddRecommendation(myEvent.eventId, groups, a, "hola");
                Assert.AreEqual(r.Count, 0);
            }
        }

        /// <summary>
        /// A test for AddGroup
        /// </summary>
        [TestMethod()]
        public void Service_AddGroupTest()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                long userId =
                    userService.RegisterUser(loginName, clearPassword,
                    new UserProfileDetails(firstName, lastName, email, language, country));

                UserGroup userGroupAdded = userService.AddGroup(name, description, userId);
                long groupId = userGroupAdded.groupId;

                Assert.AreEqual(name, userGroupAdded.name);
                Assert.AreEqual(description, userGroupAdded.description);
                Assert.AreEqual(groupId, userGroupAdded.groupId);

            }

        }

        /// <summary>
        /// A test for JoinGroup
        /// </summary>
        [TestMethod()]
        public void Service_JoinGroupTest()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                long userId =
                    userService.RegisterUser(loginName, clearPassword,
                    new UserProfileDetails(firstName, lastName, email, language, country));

                UserGroup userGroup = userService.AddGroup(name, description, userId);
                long groupId = userGroup.groupId;
                UserProfile userProfile = userProfileDao.Find(userId);

                UserGroup userGroupJoined = userService.JoinGroup(userId, groupId);

                Assert.AreEqual(1, userGroupJoined.UserProfiles.Count);

            }

        }

        /*/// <summary>
        /// A test for UnJoinGroup
        /// </summary>
        [TestMethod()]
        public void Service_UnJoinGroupTest()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                long userId =
                    userService.RegisterUser(loginName, clearPassword,
                    new UserProfileDetails(firstName, lastName, email, language, country));

                UserGroup userGroup = userService.AddGroup(name, description, userId);
                UserGroup userGroupFound = userGroupDao.FindByName(name);
                UserProfile userProfile = userProfileDao.Find(userId);

                UserGroup userGroupJoined = userService.JoinGroup(userId, userGroupFound.groupId);

                Assert.AreEqual(1, userGroupJoined.UserProfiles.Count);

                UserGroup userGroupUnJoined = userService.UnJoinGroup(userId, userGroupFound.groupId);

                Assert.AreEqual(0, userGroupUnJoined.UserProfiles.Count);
            }
            
        }*/

        /// <summary>
        /// A test for FindGroupsByUserId
        /// </summary>
       [TestMethod()]
        public void Service_FindGroupsByUserIdTest()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                long userId =
                    userService.RegisterUser(loginName, clearPassword,
                    new UserProfileDetails(firstName, lastName, email, language, country));

                userService.AddGroup(name, description, userId);
                UserProfile userProfile = userProfileDao.Find(userId);

                ICollection<UserGroupDto> userGroupFound = userService.FindGroupsByUserId(userId);

                Assert.AreEqual(1, userGroupFound.Count);
            }

        }

        /// <summary>
        /// A test for FindGroupsByKeywords
        /// </summary>
        [TestMethod()]
        public void Service_FindGroupsByKeywordsTest()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                long userId =
                    userService.RegisterUser(loginName, clearPassword,
                    new UserProfileDetails(firstName, lastName, email, language, country));

                userService.AddGroup(name, description, userId);

                ICollection<UserGroupDto> userGroupFound = userService.FindGroupsByKeywords(name,0,10);

                Assert.AreEqual(1, userGroupFound.Count);
            }

        }

        /// <summary>
        /// A test for FindGroupsByName
        /// </summary>
        [TestMethod()]
        public void Service_FindGroupsByNameTest()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                long userId =
                    userService.RegisterUser(loginName, clearPassword,
                    new UserProfileDetails(firstName, lastName, email, language, country));

                userService.AddGroup(name, description, userId);

                UserGroupDto userGroupFound = userService.FindGroupsByName(name);

                Assert.AreEqual(name, userGroupFound.name);
            }

        }

        /// <summary>
        /// A test for FindGroupsById
        /// </summary>
        [TestMethod()]
        public void Service_FindGroupsByIdTest()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                long userId =
                    userService.RegisterUser(loginName, clearPassword,
                    new UserProfileDetails(firstName, lastName, email, language, country));

                UserGroup userGroup = userService.AddGroup(name, description, userId);
                long groupId = userGroup.groupId;

                UserGroupDto userGroupFound = userService.FindGroupById(groupId);

                Assert.AreEqual(name, userGroupFound.name);
            }

        }

        /// <summary>
        /// A test for FindAllGroups
        /// </summary>
        [TestMethod()]
        public void Service_FindAllGroupsTest()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                long userId =
                    userService.RegisterUser(loginName, clearPassword,
                    new UserProfileDetails(firstName, lastName, email, language, country));

                userService.AddGroup(name, description, userId);

                ICollection<UserGroupDto> userGroupFound = userService.FindAllGroups();

                Assert.AreEqual(1, userGroupFound.Count);
            }

        }

        /// <summary>
        /// A test for FindUserByLoginName
        /// </summary>
        [TestMethod()]
        public void Service_FindUserByLoginNameTest()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                long userId =
                    userService.RegisterUser(loginName, clearPassword,
                    new UserProfileDetails(firstName, lastName, email, language, country));

                UserProfile userProfile = userService.FindUserByLoginName(loginName);

                Assert.AreEqual(loginName, userProfile.loginName);
            }

        }

        /// <summary>
        /// A test for FindUserByEmail
        /// </summary>
        [TestMethod()]
        public void Service_FindUserByEmailTest()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                long userId =
                    userService.RegisterUser(loginName, clearPassword,
                    new UserProfileDetails(firstName, lastName, email, language, country));

                UserProfile userProfile = userService.FindUserByEmail(email);

                Assert.AreEqual(loginName, userProfile.loginName);
            }

        }

        /// <summary>
        ///A test for FindGroupRecommendations
        ///</summary>
        [TestMethod()]
        public void FindGroupRecommendationsTest()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                ICollection<long> groups = new List<long>();
                long a = userService.RegisterUser("Patata", clearPassword,
                    new UserProfileDetails(firstName, lastName, email, language, country));

                UserGroup userGroupAdded = userService.AddGroup(name, description, a);
                long groupId = userGroupAdded.groupId;
                UserProfile userProfile = userService.FindUserByLoginName("Patata");
                userProfile.UserGroups.Add(userGroupAdded);
                groups.Add(groupId);

                Category category = new Category();
                category.name = categoryName;
                categoryDao.Create(category);

                Event myEvent = new Event();
                myEvent.categoryId = category.categoryId;
                myEvent.name = name;
                myEvent.eventDate = date;
                myEvent.review = review;
                myEvent.Category = category;
                eventDao.Create(myEvent);

                userService.AddRecommendation(myEvent.eventId, groups, a, "hola");
                ICollection<RecommendationDto> r = userService.FindGroupRecommendations(groupId,a,0,10);


                Assert.AreEqual(r.Count, 1);
            }
        }

        /// <summary>
        ///A test for FindAllRecommendations
        ///</summary>
        [TestMethod()]
        public void FindAllRecommendationsTest()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                ICollection<long> groups = new List<long>();
                long a = userService.RegisterUser("Patata", clearPassword,
                    new UserProfileDetails(firstName, lastName, email, language, country));

                UserGroup userGroupAdded = userService.AddGroup(name, description, a);
                long groupId = userGroupAdded.groupId;
                UserProfile userProfile = userService.FindUserByLoginName("Patata");
                userProfile.UserGroups.Add(userGroupAdded);
                groups.Add(groupId);

                Category category = new Category();
                category.name = categoryName;
                categoryDao.Create(category);

                Event myEvent = new Event();
                myEvent.categoryId = category.categoryId;
                myEvent.name = name;
                myEvent.eventDate = date;
                myEvent.review = review;
                myEvent.Category = category;
                eventDao.Create(myEvent);

                userService.AddRecommendation(myEvent.eventId, groups, a, "hola");
                ICollection<RecommendationDto> r = userService.FindAllRecommendations();


                Assert.AreEqual(r.Count, 1);
            }
        }

    }
}