using System;
using Es.Udc.DotNet.ModelUtil.Dao;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Collections.Generic;

namespace Es.Udc.DotNet.MiniPortal.Model.CommentDao
{
    public class CommentDaoEntityFramework :
        GenericDaoEntityFramework<Comment, Int64>, ICommentDao
    {

        public CommentDaoEntityFramework() { }

        #region ICommentDao Members

        public ICollection<Comment> FindCommentsOrderByDate(long eventId, int startIndex, int count)
        {
            ICollection<Comment> Comments = null;

            #region Option 3: Using Entity SQL and Object Services provided by old ObjectContext.

            String sqlQuery =
                "SELECT VALUE u FROM MiniPortalEntities.Comments AS u " +
                "WHERE u.eventId=@eventId ORDER BY u.commentDate";

            ObjectParameter param = new ObjectParameter("eventId", eventId);

            ObjectQuery<Comment> query =
              ((System.Data.Entity.Infrastructure.IObjectContextAdapter)Context).ObjectContext.CreateQuery<Comment>(sqlQuery, param);

            List<Comment> result = query.Skip(startIndex).Take(count).ToList<Comment>();

            try
            {
                Comments = result;
            }
            catch (Exception)
            {
                Comments = null;
            }

            #endregion

            return Comments;
        }


        #endregion
    }

}

