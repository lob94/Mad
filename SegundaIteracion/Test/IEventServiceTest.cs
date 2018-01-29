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
using Es.Udc.DotNet.MiniPortal.Model.EventService;
using Es.Udc.DotNet.MiniPortal.Model.UserService;
using Es.Udc.DotNet.MiniPortal.Model.CommentDao;
using Es.Udc.DotNet.MiniPortal.Model.UserProfileDao;
using Es.Udc.DotNet.MiniPortal.Model.UserGroup1Dao;
using Es.Udc.DotNet.MiniPortal.Model.LabelDao;

namespace Es.Udc.DotNet.MiniPortal.Test
{
   [TestClass]
    public class IEventServiceTest
    {

        private static IKernel kernel;
        private static IEventService eventService;
        private static IUserService userService;
        private static IUserProfileDao userProfileDao;
        private static IEventDao eventDao;
        private static ICategoryDao categoryDao;
        private static ICommentDao commentDao;
        private static IUserGroup1Dao userGroupDao;
        private static ILabelDao labelDao;
        private static Event myEvent;
        private static Category category;
        private static Comment comment;
        private static UserProfile userProfile;
       

        // Variables used in several tests are initialized here
        private const String name = "Liga BBVA";
        private const String review = "La liga más valorada a nivel mundial";
        private DateTime date = DateTime.Now;
        private const String content = "La liga con los mejores jugadores";
        private const String content2 = "La liga peor organizada";
        private const String contentEdited = "La liga de Messi";
        private const long NON_EXISTENT_Event_ID = -1;
        private const String categoryName = "Futbol";
        private const String keywords = "myEv";
        private const String label = "jajajaja muy bueno!";
        private const String labelEdited = "jaja éste sí!";

        // UserProfile attributes
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
            eventService = kernel.Get<IEventService>();
            userService = kernel.Get<IUserService>();
            userProfileDao = kernel.Get<IUserProfileDao>();
            eventDao = kernel.Get<IEventDao>();
            categoryDao = kernel.Get<ICategoryDao>();
            commentDao = kernel.Get<ICommentDao>();
            userGroupDao = kernel.Get<IUserGroup1Dao>();
        }

        //Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            eventService.cleanCache();
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
            //userProfile.Recommendations = new List<Recommendation>();
            //userProfile.UserGroups = new List<UserGroup1>();
            //userProfile.Comments = new List<Comment>();

            userProfileDao.Create(userProfile);          

            category = new Category();
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
            comment.loginName = userLoginName;
            
            commentDao.Create(comment);

        }

        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            transaction.Dispose();
        }

        #endregion

        /// <summary>
        ///A test for FindEventById
        ///</summary>
        [TestMethod()]
        public void Service_FindEventByIdTest()
        {
            try
            {
                Event actual = eventService.FindEventById(myEvent.eventId);

                Assert.AreEqual(myEvent, actual, "Event found does not correspond with the original one.");
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

        }

        /// <summary>
        ///A test for FindEventByName
        ///</summary>
        [TestMethod()]
        public void Service_FindByKeywordsTest()
        {
            try
            {
                Category category2 = new Category();
                category2.name = "Basket";

                categoryDao.Create(category2);

                Event myEvent2 = new Event();
                myEvent2.categoryId = category2.categoryId;
                myEvent2.name = "Liga EuroBasket";
                myEvent2.eventDate = DateTime.Now;
                myEvent2.review = review;
                myEvent2.Category = category2;
                //myEvent.Recommendations = new List<Recommendation>();

                eventDao.Create(myEvent2);

                ICollection<EventDto> actual = eventService.FindEventsByKeywords(myEvent.name, 0, 10);

                Assert.AreEqual(actual.Count, 1, "Event found does not correspond with the original one.");

                actual = eventService.FindEventsByKeywords("Ba Eu", 0, 10);

                Assert.AreEqual(actual.Count, 1, "Event found does not correspond with the original one.");

                actual = eventService.FindEventsByKeywords("", 0, 10);

                Assert.AreEqual(actual.Count, 2, "Event found does not correspond with the original one.");

                actual = eventService.FindEventsByKeywords("BB", 0, 10);

                Assert.AreEqual(actual.Count, 1, "Event found does not correspond with the original one.");
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

        }

        /// <summary>
        ///A test for FindEventByCategory
        ///</summary>
        [TestMethod()]
        public void Service_FindByCategoryTest()
        {
            try
            {
                Category category2 = new Category();
                category2.name = "Basket";

                categoryDao.Create(category2);

                Event myEvent2 = new Event();
                myEvent2.categoryId = category2.categoryId;
                myEvent2.name = "Liga EuroBasket";
                myEvent2.eventDate = DateTime.Now;
                myEvent2.review = review;
                myEvent2.Category = category2;
                //myEvent.Recommendations = new List<Recommendation>();

                eventDao.Create(myEvent2);


                Event myEvent3 = new Event();
                myEvent3.categoryId = category.categoryId;
                myEvent3.name = "Liga EuroBasket";
                myEvent3.eventDate = DateTime.Now;
                myEvent3.review = review;
                myEvent3.Category = category;
                //myEvent.Recommendations = new List<Recommendation>();

                eventDao.Create(myEvent3);


                ICollection<EventDto> actual = eventService.FindEventsByCategory(category.categoryId, 0, 10);

                Assert.AreEqual(actual.Count, 2, "Event found does not correspond with the original one.");

                actual = eventService.FindEventsByCategory(category2.categoryId, 0, 10);

                Assert.AreEqual(actual.Count, 1, "Event found does not correspond with the original one.");


            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

        }

        /// <summary>
        ///A test for FindEventByName
        ///</summary>
        [TestMethod()]
        public void Service_FindEventsByKeywordsAndCategoryTest()
        {
            try
            {
                Category category2 = new Category();
                category2.name = "Basket";

                categoryDao.Create(category2);

                Event myEvent2 = new Event();
                myEvent2.categoryId = category2.categoryId;
                myEvent2.name = "Liga EuroBasket";
                myEvent2.eventDate = DateTime.Now;
                myEvent2.review = review;
                myEvent2.Category = category2;
                //myEvent.Recommendations = new List<Recommendation>();

                eventDao.Create(myEvent2);


                Event myEvent3 = new Event();
                myEvent3.categoryId = category.categoryId;
                myEvent3.name = "Liga EuroBasket";
                myEvent3.eventDate = DateTime.Now;
                myEvent3.review = review;
                myEvent3.Category = category;
                //myEvent.Recommendations = new List<Recommendation>();

                eventDao.Create(myEvent3);


                ICollection<EventDto> actual = eventService.FindEventsByKeywordsAndCategory("Liga", category.categoryId, 0, 10);

                Assert.AreEqual(actual.Count, 2, "Event found does not correspond with the original one.");

                actual = eventService.FindEventsByKeywordsAndCategory("Euro", category2.categoryId, 0, 10);

                Assert.AreEqual(actual.Count, 1, "Event found does not correspond with the original one.");


            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

        }

        /// <summary>
        ///A test for FindAllEvents
        ///</summary>
        [TestMethod()]
        public void Service_FindAllEvents()
        {
            try
            {
                ICollection<EventDto> events = eventService.FindAllEvents();
                int count = events.Count;

                Assert.AreEqual(1, count, "FindAllEvents doesn't work propperly");

            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

        }


        /// <summary>
        ///A test for FindAllComments
        ///</summary>
        [TestMethod()]
        public void Service_FindAllCommentsTest()
        {
            try
            {
                ICollection < Comment > comentarios = myEvent.Comments;
                Assert.AreEqual(1, comentarios.Count, "FindAllComments doesn't work propperly.");
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

        }

        /// <summary>
        ///A test for FindAllCategories
        ///</summary>
        [TestMethod()]
        public void Service_FindAllCategoriesTest()
        {
            try
            {
                ICollection<Category> categories = eventService.FindAllCategories();
                Assert.AreEqual(1, categories.Count, "FindAllCategories doesn't work propperly.");
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

        }

        /// <summary>
        ///A test for AddComment
        ///</summary>
        [TestMethod()]
        public void Service_AddComment()
        {
            try
            {
                long userId =
                    userService.RegisterUser(userLoginName2, clearPassword,
                    new UserProfileDetails(firstName, lastName, email, language, country));
                eventService.AddComment(content2, myEvent.eventId, userId);


                Event e1 = eventService.FindEventById(myEvent.eventId);
                ICollection<Comment> comentarios = e1.Comments;
                ;
                Assert.AreEqual(2, comentarios.Count, "Comment has not been add.");

            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

        }

        /// <summary>
        ///A test for EditComment
        ///</summary>
        [TestMethod()]
        public void Service_EditComment()
        {
            try
            {
                long userId =
                    userService.RegisterUser(userLoginName2, clearPassword,
                    new UserProfileDetails(firstName, lastName, email, language, country));
                Comment commentToEdit = eventService.AddComment(content2, myEvent.eventId, userId);

                eventService.EditComment(commentToEdit.commentId, userId, contentEdited);
                
                Assert.AreEqual(contentEdited, commentToEdit.content, "Comment has not been edited.");

            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

        }

        /// <summary>
        /// A test for Create
        /// </summary>
        [TestMethod()]
        public void Service_CreateTest()
        {
            try
            {
                ICollection<Comment> comments = myEvent.Comments;
                String label1 = "label1";
                Label label = new Label();
                label.commentsNum = 1;
                label.Comments = comments;
                label.name = label1;

                Label label2 = eventService.Create(label);

                Assert.AreEqual(label, label2, "Create doesn't work properly.");


            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

        }

        /// <summary>
        /// A test for AddLabel
        /// </summary>
        [TestMethod()]
        public void Service_AddLabel()
        {
            try
            {
                long userId =
                    userService.RegisterUser(userLoginName2, clearPassword,
                    new UserProfileDetails(firstName, lastName, email, language, country));
                Comment comment = eventService.AddComment(content2, myEvent.eventId, userId);
                ICollection<Comment> comments = myEvent.Comments;
                ICollection <Label> labels = comment.Labels;
                String labelname = "label1";
                Label label1 = new Label();
                label1.commentsNum = 1;
                label1.Comments = comments;
                label1.name = labelname;
                labels.Add(label1);

                eventService.AddLabel(comment.commentId, labels);

                Assert.AreEqual(1, comment.Labels.Count, "Label has not been edited.");

            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

        }

        /// <summary>
        /// A test for FindLabel
        /// </summary>
        [TestMethod()]
        public void Service_FindTest()
        {
            try
            {
                long userId =
                    userService.RegisterUser(userLoginName2, clearPassword,
                    new UserProfileDetails(firstName, lastName, email, language, country));
                Comment comment = eventService.AddComment(content2, myEvent.eventId, userId);
                ICollection<Comment> labelComments=myEvent.Comments;
                String label1 = "label1";
                Label label = new Label();
                label.name = label1;
                eventService.Create(label);
                ICollection<CommentDto> labelCommentsDto = new List<CommentDto>();

                LabelDto labelFound = eventService.Find(label.labelId);

                Assert.AreEqual(label1, labelFound.name, "Label doesn't work properly.");

            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

        }

        /// <summary>
        /// A test for FindLabelByName
        /// </summary>
        [TestMethod()]
        public void Service_FindLabelByNameTest()
        {
            try
            {
                long userId =
                    userService.RegisterUser(userLoginName2, clearPassword,
                    new UserProfileDetails(firstName, lastName, email, language, country));
                Comment comment = eventService.AddComment(content2, myEvent.eventId, userId);
                ICollection<Comment> comments = myEvent.Comments;
                ICollection<Label> labels = comment.Labels;
                String labelname = "label1";
                Label label1 = new Label();
                label1.name = labelname;
                eventService.Create(label1);
                labels.Add(label1);

                eventService.AddLabel(comment.commentId, labels);

                Label labelFound = eventService.FindLabelByName(labelname);

                Assert.AreEqual(label1, labelFound, "FindLabelByName doesn't work properly.");

            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

        }

        /// <summary>
        /// A test for GetNumberOfLabels
        /// </summary>
        [TestMethod()]
        public void Service_GetNumberOfLabelsTest()
        {
            try
            {
                long userId =
                    userService.RegisterUser(userLoginName2, clearPassword,
                    new UserProfileDetails(firstName, lastName, email, language, country));
                Comment comment = eventService.AddComment(content2, myEvent.eventId, userId);
                ICollection<Comment> comments = myEvent.Comments;
                ICollection<Label> labels = comment.Labels;
                String labelname = "label1";
                Label label1 = new Label();
                label1.name = labelname;
                eventService.Create(label1);
                labels.Add(label1);

                eventService.AddLabel(comment.commentId, labels);

                int count = eventService.GetNumberOfLabels();

                Assert.AreEqual(1, count, "GetNumberOfLabels doesn't work properly.");

            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

        }

        /// <summary>
        /// A test for GetReferences
        /// </summary>
        [TestMethod()]
        public void Service_GetReferencesTest()
        {
            try
            {
                long userId =
                    userService.RegisterUser(userLoginName2, clearPassword,
                    new UserProfileDetails(firstName, lastName, email, language, country));
                Comment comment = eventService.AddComment(content2, myEvent.eventId, userId);
                ICollection<Comment> comments = myEvent.Comments;
                ICollection<Label> labels = new List<Label>();
                String labelname = "label1";
                Label label1 = new Label();
                label1.name = labelname;
                eventService.Create(label1);
                labels.Add(label1);

                eventService.AddLabel(comment.commentId, labels);

                int count = eventService.GetReferences(label1);

                Assert.AreEqual(1, count, "GetNumberOfLabels doesn't work properly.");

            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

        }

        /// <summary>
        /// A test for GetLabelsDtos
        /// </summary>
        [TestMethod()]
        public void Service_GetLabelsDtosTest()
        {
            try
            {
                long userId =
                    userService.RegisterUser(userLoginName2, clearPassword,
                    new UserProfileDetails(firstName, lastName, email, language, country));
                Comment comment = eventService.AddComment(content2, myEvent.eventId, userId);
                ICollection<Comment> comments = myEvent.Comments;
                ICollection<Label> labels = new List<Label>();
                String labelname = "label1";
                Label label1 = new Label();
                label1.name = labelname;
                eventService.Create(label1);
                labels.Add(label1);

                eventService.AddLabel(comment.commentId, labels);

                ICollection<LabelDto> labelsDtos = eventService.GetLabelsDtos();

                Assert.AreEqual(1, labelsDtos.Count, "GetNumberOfLabels doesn't work properly.");

            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        /// <summary>
        /// A test for GetTotalReferences
        /// </summary>
        [TestMethod()]
        public void Service_GetTotalReferencesTest()
        {
            try
            {
                long userId =
                    userService.RegisterUser(userLoginName2, clearPassword,
                    new UserProfileDetails(firstName, lastName, email, language, country));
                Comment comment = eventService.AddComment(content2, myEvent.eventId, userId);
                Comment comment2 = eventService.AddComment(content, myEvent.eventId, userId);
                ICollection<Comment> comments = myEvent.Comments;;
                ICollection<Label> labels = new List<Label>(); ;
                ICollection<Label> labels2 = new List<Label>(); ;
                String labelname = "label1";
                Label label1 = new Label();
                label1.name = labelname;
                eventService.Create(label1);
                labels.Add(label1);
                String labelname2 = "label2";
                Label label2 = new Label();
                label2.name = labelname2;
                eventService.Create(label2);
                labels2.Add(label2);

                eventService.AddLabel(comment.commentId, labels);
                eventService.AddLabel(comment2.commentId, labels2);

                int count = eventService.GetTotalReferences();

                Assert.AreEqual(2, count, "GetTotalReferences doesn't work properly.");

            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

        }

        /// <summary>
        /// A test for GetAllLabels
        /// </summary>
        [TestMethod()]
        public void Service_GetAllLabelsTest()
        {
            try
            {
                long userId =
                    userService.RegisterUser(userLoginName2, clearPassword,
                    new UserProfileDetails(firstName, lastName, email, language, country));
                Comment comment = eventService.AddComment(content2, myEvent.eventId, userId);
                ICollection<Comment> comments = myEvent.Comments;
                ICollection<Label> labels = comment.Labels;
                String labelname = "label1";
                Label label1 = new Label();
                label1.commentsNum = 1;
                label1.Comments = comments;
                label1.name = labelname;
                labels.Add(label1);

                eventService.AddLabel(comment.commentId, labels);

                ICollection<Label> labelsFound = eventService.GetAllLabels();

                Assert.AreEqual(1, labelsFound.Count, "GetNumberOfLabels doesn't work properly.");

            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

        }

        /// <summary>
        /// A test for RemoveLabel
        /// </summary>
        [TestMethod()]
        public void Service_GetCommentLabelsTest()
        {
            try
            {
                long userId =
                    userService.RegisterUser(userLoginName2, clearPassword,
                    new UserProfileDetails(firstName, lastName, email, language, country));
                Comment comment = eventService.AddComment(content2, myEvent.eventId, userId);
                ICollection<Comment> comments = myEvent.Comments;
                ICollection<Label> labels = comment.Labels;
                String labelname = "label1";
                Label label1 = new Label();
                label1.commentsNum = 1;
                label1.Comments = comments;
                label1.name = labelname;
                labels.Add(label1);

                Comment commentObtained = eventService.AddLabel(comment.commentId, labels);
                ICollection<LabelDto> commentLabels = eventService.GetCommentLabels(commentObtained.commentId);

                Assert.AreEqual(1, commentLabels.Count, "GetCommentLabels doesn't work properly.");

            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

        }

    }
    
}
