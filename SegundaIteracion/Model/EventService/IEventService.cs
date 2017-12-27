using System;

using Es.Udc.DotNet.MiniPortal.Model.EventDao;
using System.Collections.Generic;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Ninject;
using Es.Udc.DotNet.MiniPortal.Model.UserGroup1Dao;
using Es.Udc.DotNet.MiniPortal.Model.UserProfileDao;
using Es.Udc.DotNet.MiniPortal.Model.RecommendationDao;
using Es.Udc.DotNet.MiniPortal.Model.CommentDao;
using Es.Udc.DotNet.MiniPortal.Model.LabelDao;
using Es.Udc.DotNet.MiniPortal.Model.CategoryDao;

namespace Es.Udc.DotNet.MiniPortal.Model.EventService
{
    public interface IEventService
    {
        [Inject]
        IEventDao EventDao { set; }
        [Inject]
        ICommentDao CommentDao { set; }
        [Inject]
        ILabelDao LabelDao { set; }
        [Inject]
        ICategoryDao CategoryDao { set; }

        /// <summary>
        /// Finds event by id.
        /// </summary>
        /// <param eventId="EventId">The event id.</param>
        /// <returns>The event</returns> 
        /// <exception cref="InstanceNotFoundException"/>  
        [Transactional]
        Event FindEventById(long eventId);
        ///DUDA DE SI LO NECESITAMOS!!!

        /// <summary>
        /// Find a event by category.
        /// </summary>
        /// <param name="Name">The event name</param>
        /// <param startIndex="StartIndex">The event name</param>
        /// <param count="Count">The event name</param>
        /// <return>The list of events</return>
        /// <exception cref="InstanceNotFoundException"/>
        ICollection<EventDto> FindEventsByKeywords(string name, int startIndex, int count);

        /// <summary>
        /// Find a event by category.
        /// </summary>
        /// <param categoryId="CategoryId">The event categoryId.</param>
        /// <param startIndex="StartIndex">The event name</param>
        /// <param count="Count">The event name</param>
        /// <return>The list of events</return>
        /// <exception cref="InstanceNotFoundException"/>
        ICollection<EventDto> FindEventsByCategory(long categoryId, int startIndex, int count);

        /// <summary>
        /// Find a event by category.
        /// </summary>
        /// <return>The list of eventsDto</return>
        /// <exception cref="InstanceNotFoundException"/>
        ICollection<EventDto> FindAllEvents();


        /// <summary>
        /// Show the comments of the event.
        /// </summary>
        /// <param eventId="EventId">The event id.</param>
        /// <param startIndex="startIndex">integer</param>
        /// <param count="count">integer</param>
        /// <return>A list of commentsDto</return>
        /// <exception cref="InstanceNotFoundException"/>
        ICollection<CommentDto> FindAllComments(long eventId, int startIndex, int count);

        /// <summary>
        /// Add new Comment to an event
        /// </summary>
        /// <param eventId="EventId">The event id.</param>  
        /// <param content="content">The comment's text</param>
        /// <param userId="UserId">The user id.</param>
        /// <return>A new comment</return>
        /// <exception cref="InstanceNotFoundException"/>
        Comment AddComment(string content, long eventId, long userId);

        /// <summary>
        /// Edit Comment to an event
        /// </summary>
        /// <param commentId="CommentId">The comment id.</param>  
        /// <param newComment="NewComment">The comment's new text</param>
        /// <param userId="UserId">The user id.</param>
        /// <return>Comment modified</return>
        /// <exception cref="InstanceNotFoundException"/>
        Comment EditComment(long commentId, long userId, string newComment);

        /// <summary>
        /// Delete Comment
        /// </summary>
        /// <param commentId="CommentId">The comment id.</param>  
        /// <param userId="UserId">The user id.</param>
        /// <exception cref="InstanceNotFoundException"/>
        void DeleteComment(long commentId, long userId);

        /// <summary>
        /// Add Label to a Comment
        /// </summary>
        /// <param commentId="CommentId">The Comment where a label is added</param>
        /// <param name="Name">The Label's Name</param>
        /// <exception cref="DuplicateInstanceException"/> 
        [Transactional]
        Label AddLabel(long commentId, string name);

        /// <summary>
        /// Edit Label's name of a Comment
        /// </summary>
        /// <param commentId="CommentId">The Comment where the label is</param>
        /// <param name="Name">The new label's name</param>
        /// <exception cref="InstanceNotFoundException"/> 
        [Transactional]
        Label EditLabel(long commentId, string name);

        ///<summary>
        /// Show All Labels to choose
        /// </summary>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        ICollection<Label> ShowLabels();


    }
}