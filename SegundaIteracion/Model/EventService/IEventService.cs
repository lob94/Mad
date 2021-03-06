﻿using System;

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
using Es.Udc.DotNet.MiniPortal.Model.Caching;

using Microsoft.Practices.EnterpriseLibrary.Caching;

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
        [Inject]
        ICachingProvider cachingProvider { set; }

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
        /// <param startIndex="StartIndex">Starting search position</param>
        /// <param count="Count">Max. number of events to search</param>
        /// <return>The list of events</return>
        /// <exception cref="InstanceNotFoundException"/>
        ICollection<EventDto> FindEventsByKeywords(string name, int startIndex, int count);

        /// <summary>
        /// Find a event by category.
        /// </summary>
        /// <param categoryId="CategoryId">The event categoryId.</param>
        /// <param startIndex="StartIndex">Starting search position</param>
        /// <param count="Count">Max. number of events to search</param>
        /// <return>The list of events</return>
        /// <exception cref="InstanceNotFoundException"/>
        ICollection<EventDto> FindEventsByCategory(long categoryId, int startIndex, int count);

        /// <summary>
        /// Find a event by keywords and category.
        /// </summary>
        /// <param name="Name">The event name</param>
        /// <param categoryId="CategoryId">The event categoryId.</param>
        /// <param startIndex="StartIndex">Starting search position</param>
        /// <param count="Count">Max. number of events to search</param>
        /// <return>The list of events</return>
        /// <exception cref="InstanceNotFoundException"/>
        ICollection<EventDto> FindEventsByKeywordsAndCategory(String name, long categoryId, int startIndex, int count);

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
        /// Show all the categories.
        /// </summary>
        /// <return>A list of categories</return>
        /// <exception cref="InstanceNotFoundException"/>
        ICollection<Category> FindAllCategories();

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
        /// Creates a label into database
        /// </summary>
        /// <param name="Label"> Label to create</param>
        /// <returns>Label</returns>
        /// <exception cref="DuplicateInstanceException"/>
        [Transactional]
        Label Create(Label label);

        /// <summary>
        /// Find a label by name
        /// </summary>
        /// <param name="labelName"> Name of the label</param>
        /// <returns>Label</returns>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        Label FindLabelByName(string labelName);

        /// <summary>
        /// Finds a label by id
        /// </summary>
        /// <param name="labelId">Identifier of the label</param>
        /// <returns>Label</returns>
        /// /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        LabelDto Find(long labelId);

        /// <summary>
        /// Gets All of labelsDtos
        /// </summary>
        /// <param name="startIndex">starting at 0</param>
        /// <param name="count">the maximun elements to return</param>
        /// <returns> of labels</returns>
        [Transactional]
        ICollection<LabelDto> GetLabelsDtos();

        ///<summary>
        /// Show All Labels to choose
        /// </summary>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        ICollection<Label> GetAllLabels();

        /// <summary>
        /// Get number of references
        /// </summary>
        /// <param name="label">Label object</param>
        /// <returns>Number of references</returns>
        [Transactional]
        int GetReferences(Label label);

        /// <summary>
        /// Get the number of references
        /// </summary>
        /// <returns>int number of references</returns>
        [Transactional]
        int GetTotalReferences();

        /// <summary>
        /// Returns number of labels registered
        /// </summary>
        /// <returns>Total number of labels</returns>
        [Transactional]
        int GetNumberOfLabels();

        /// <summary>
        /// Add label in acomment
        /// </summary>
        /// <param name="commentId">commentId</param>
        /// <param name="labels">collection with the labels</param>
        /// <returns>Comnment with the labels added</returns>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        Comment AddLabel(long commentId, ICollection<Label> labels);

        /// <summary>
        /// Get the labels asociated to a comment
        /// </summary>
        /// <param name="commentId">Identifier of the comment</param>
        /// <returns>ICollection with Labels</returns>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        ICollection<LabelDto> GetCommentLabels(long commentId);

        /// <summary>
        /// remove a list of labels
        /// </summary>
        /// <param name="commentId">Identifier of the comment</param>
        /// <param name="labels">collection with the labels</param>
        /// <returns>Comment</returns>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        Comment RemoveLabel(long commentId, ICollection<Label> labels);

        /// <summary>
        /// Number of found events
        /// </summary>
        /// <param name="name">name</param>
        /// <returns>int</returns>
        int CountFindEventsByKeywords(string name);

        /// <summary>
        /// Number of found events
        /// </summary>
        /// <param name="categoryId">categoryId</param>
        /// <returns>int</returns>
        int CountFindEventsByCategory(long categoryId);

        /// <summary>
        /// Number of found events
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="categoryId">categoryId</param>
        /// <returns>int</returns>
        int CountFindEventsByKeywordsAndCategory(string name, long categoryId);

        /// <summary>
        /// Clean cache for tests
        /// </summary>
        void cleanCache();
    }
}