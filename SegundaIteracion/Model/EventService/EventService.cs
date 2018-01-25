using System;
using Es.Udc.DotNet.MiniPortal.Model.EventDao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System.Collections.Generic;
using System.Runtime.Caching;
using Ninject;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.MiniPortal.Model.UserGroup1Dao;
using Es.Udc.DotNet.MiniPortal.Model.UserProfileDao;
using Es.Udc.DotNet.MiniPortal.Model.RecommendationDao;
using Es.Udc.DotNet.MiniPortal.Model.CommentDao;
using Es.Udc.DotNet.MiniPortal.Model.LabelDao;
using Es.Udc.DotNet.MiniPortal.Model.UserService;
using Es.Udc.DotNet.MiniPortal.Model;
using Es.Udc.DotNet.MiniPortal.Model.CategoryDao;
using Es.Udc.DotNet.MiniPortal.Model.Caching;

using Microsoft.Practices.EnterpriseLibrary.Caching;

namespace Es.Udc.DotNet.MiniPortal.Model.EventService
{
    public class EventService : IEventService
    {
        [Inject]
        public IEventDao EventDao { private get; set; }
        [Inject]
        public IUserProfileDao UserProfileDao { private get; set; }
        [Inject]
        public ICommentDao CommentDao { private get; set; }
        [Inject]
        public ILabelDao LabelDao { private get; set; }
        [Inject]
        public ICategoryDao CategoryDao { private get; set; }
        [Inject]
        public ICachingProvider cachingProvider { private get; set; }


        #region IEventService Members

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public Event FindEventById(long eventId)
        {
            Event myEvent = EventDao.Find(eventId);
            return myEvent;
        }

        /// <exception cref="InstanceNotFoundException"/>     
        [Transactional]
        public ICollection<EventDto> FindEventsByKeywords(String name, int startIndex, int count)
        {
            String[] keywords = name.Split(' ');
            ICollection<Event> events = EventDao.FindByKeywordsAndCategory(keywords, -1, startIndex, count);
            ICollection<EventDto> eventsDto = new HashSet<EventDto>();
            foreach (Event e in events)
            {
                eventsDto.Add(new EventDto(e));
            }
            return eventsDto;
        }

        /// <exception cref="InstanceNotFoundException"/>     
        [Transactional]
        public ICollection<EventDto> FindEventsByCategory(long categoryId, int startIndex, int count)
        {
            String name = "";
            String[] keywords = name.Split(' ');
            ICollection<Event> events = EventDao.FindByKeywordsAndCategory(keywords, categoryId, startIndex, count);
            ICollection<EventDto> eventsDto = new HashSet<EventDto>();
            foreach (Event e in events)
            {
                eventsDto.Add(new EventDto(e));
            }
            return eventsDto;
        }

        /// <exception cref="InstanceNotFoundException"/>     
        [Transactional]
        public ICollection<EventDto> FindEventsByKeywordsAndCategory(String name, long categoryId, int startIndex, int count)
        {
            if (cachingProvider.GetItem(name,false) != null)
            {
                ICollection<Event> events = (ICollection<Event>)cachingProvider.GetItem(name, false);
                cachingProvider.AddItem(name, events);
                ICollection<EventDto> eventsDto = new HashSet<EventDto>();
                foreach (Event e in events)
                {
                    eventsDto.Add(new EventDto(e));
                }
                return eventsDto;
            }
            else
            {

                String[] keywords = name.Split(' ');
                ICollection<Event> events = EventDao.FindByKeywordsAndCategory(keywords, categoryId, startIndex, count);
                cachingProvider.AddItem(name, events);
                ICollection<EventDto> eventsDto = new HashSet<EventDto>();
                foreach (Event e in events)
                {
                    eventsDto.Add(new EventDto(e));
                }
                return eventsDto;
            }
        
    }

        public ICollection<EventDto> FindAllEvents()
        {
            ICollection<Event> events = EventDao.GetAllElements();
            ICollection<EventDto> eventsDto = new HashSet<EventDto>();
            foreach (Event evento in events)
            {
                eventsDto.Add(new EventDto(evento));
            }
            return eventsDto;

        }


        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public ICollection<CommentDto> FindAllComments(long eventId, int startIndex, int count)
        {
            Event myEvent = EventDao.Find(eventId);

            ICollection<Comment> comments = CommentDao.FindCommentsOrderByDate(eventId, startIndex, count);
            ICollection<CommentDto> commentsDto = new HashSet<CommentDto>();

            foreach (Comment comment in comments)
            {
                commentsDto.Add(new CommentDto(comment, eventId));
            }
            return commentsDto;
        }

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public ICollection<Category> FindAllCategories()
        {
            ICollection<Category> categories = CategoryDao.GetAllElements();
            return categories;

        }

        [Transactional]
        public Comment AddComment(string comment, long eventId, long userId)
        {
            Event e = EventDao.Find(eventId);
            UserProfile u = UserProfileDao.Find(userId);

            Comment c = new Comment();
            c.content = comment;
            c.Event = e;
            c.UserProfile = u;
            c.Labels = new List<Label>();
            c.loginName = u.loginName;
            CommentDao.Create(c);

            return c;
        }

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public Comment EditComment(long commentId, long userId, string newComment)
        {
            Comment c = CommentDao.Find(commentId);
            if (c.UserProfile.usrId == userId)
                c.content = newComment;
            else
                throw new Exception();
            return c;
        }

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public void DeleteComment(long commentId, long userId)
        {
            Comment c = CommentDao.Find(commentId);
            if (c.UserProfile.usrId == userId)
                CommentDao.Remove(c.commentId);
            else
                throw new Exception();

        }


        [Transactional]
        public Label AddLabel(long commentId, string name)
        {
            Comment c = CommentDao.Find(commentId);
            Label l = new Label();
            l.name = name;
            l.Comments = null; // ¿Cómo hacer esto?
            c.Labels.Add(l); //SE AÑADE LA ETIQUETA AL COMENTARIO????
            LabelDao.Create(l);
            return l;
        }

        [Transactional]
        public Label EditLabel(long commentId, string name)
        {
            Comment c = CommentDao.Find(commentId);
            Label l = new Label();
            //c.Labels.() ??????????????? MODIFICAR EL ELEMENTO DE LA LISTA DENTRO DE COMMENTS
            //O ENCONTRAR LA LABEL POR ID Y MODIFICARLA
            l.name = name;
            return l;
        }

        public ICollection<Label> ShowLabels()
        {
            ICollection<Label> lista = LabelDao.GetAllElements();
            return lista;
        }

        public int CountFindEventsByKeywords(string name)
        {
            int countProducts;
         
                String[] keywords = name.Split(' ');
                countProducts = EventDao.CountFindEvents(keywords, -1);
             
                return (int)countProducts;
        }

        public int CountFindEventsByCategory(long categoryId)
        { 
            int countProducts;
                String keywords = "";
                String[] name = keywords.Split(' ');
                countProducts = EventDao.CountFindEvents(name, categoryId);
            return (int)countProducts;
        }


        public int CountFindEventsByKeywordsAndCategory(string name, long categoryId)
        {
            if (name == null)
            {
                name = "";
            }
            int countProducts;
          
                String[] keywords = name.Split(' ');
                countProducts = EventDao.CountFindEvents(keywords, categoryId);
            return (int)countProducts;
        }

        public void cleanCache()
        {
            cachingProvider.Clean();
        }

        #endregion
    }
}