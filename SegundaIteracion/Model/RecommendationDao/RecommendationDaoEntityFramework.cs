using System;
using Es.Udc.DotNet.ModelUtil.Dao;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Collections.Generic;

namespace Es.Udc.DotNet.MiniPortal.Model.RecommendationDao
{
    public class RecommendationDaoEntityFramework :
        GenericDaoEntityFramework<Recommendation, Int64>, IRecommendationDao
    {

        public RecommendationDaoEntityFramework() { }

        #region IRecommendationDao Members

        public ICollection<Recommendation> FindByGroupId(long groupId, int startIndex, int count)
        {
            ICollection<Recommendation> lista = new List<Recommendation>();

            #region Option 3: Using Entity SQL and Object Services provided by old ObjectContext.

            ObjectQuery<Recommendation> query = getFindQuery(groupId);

            List<Recommendation> result = query.Skip(startIndex).Take(count).ToList<Recommendation>(); ;

            try
            {
                lista = result.ToList<Recommendation>();
            }
            catch (Exception)
            {
                lista = null;
            }

            #endregion


            return lista;
        }

        private System.Data.Entity.Core.Objects.ObjectQuery<Recommendation> getFindQuery(long groupId)
        {
            String sqlQuery =
                "SELECT VALUE u FROM MiniPortalEntities.Recommendations AS u " +
                "WHERE u.UserGroup.groupId = @groupId ORDER BY u.created";

            ObjectParameter param = new ObjectParameter("groupId", groupId);

            ObjectQuery<Recommendation> query =
              ((System.Data.Entity.Infrastructure.IObjectContextAdapter)Context).ObjectContext.CreateQuery<Recommendation>(sqlQuery, param);
            return query;
        }

        public Recommendation FindByGroupIdAndEventIdAndUsrId(long groupId, long usrId, long eventId)
        {
            Recommendation recommendation = null;

            #region Option 3: Using Entity SQL and Object Services provided by old ObjectContext.

            String sqlQuery =
                "SELECT VALUE u FROM MiniPortalEntities.Recommendations AS u " +
                "WHERE u.UserGroup.groupId = @groupId AND u.Event.eventId = @eventId AND u.UserProfile.usrId = @usrId " + 
                "ORDER BY u.created";

            ObjectParameter param1 = new ObjectParameter("groupId", groupId);
            ObjectParameter param2 = new ObjectParameter("eventId", eventId);
            ObjectParameter param3 = new ObjectParameter("usrId", usrId);

            ObjectQuery<Recommendation> query =
              ((System.Data.Entity.Infrastructure.IObjectContextAdapter)Context).ObjectContext.CreateQuery<Recommendation>(sqlQuery, param1, param2, param3);

            var result = query.Execute(MergeOption.AppendOnly);

            try
            {
                recommendation = result.First<Recommendation>();
            }
            catch (Exception)
            {
                recommendation = null;
            }

            #endregion


            return recommendation;
        }

        public int CountFindGroupRecommendation(long groupId)
        {
            int result = getFindQuery(groupId).Count();
            return result;
        }

        #endregion
    }

}