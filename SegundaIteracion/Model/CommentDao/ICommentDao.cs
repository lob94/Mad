using System;
using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System.Collections.Generic;

namespace Es.Udc.DotNet.MiniPortal.Model.CommentDao
{
    public interface ICommentDao : IGenericDao<Comment, Int64>
    {
        /// <summary>
        /// Finds a Comment by Group
        /// </summary>
        /// <param eventId="eventId">eventId</param>
        /// <returns>The Comment</returns>
        /// <exception cref="InstanceNotFoundException"/>
        ICollection<Comment> FindCommentsOrderByDate(long eventId, int startIndex, int count);
       
    }
}

