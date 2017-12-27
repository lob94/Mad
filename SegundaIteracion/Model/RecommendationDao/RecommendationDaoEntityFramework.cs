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

            String sqlQuery =
                "SELECT VALUE u FROM MiniPortalEntities.Recommendations AS u " +
                "WHERE u.UserGroup.groupId = @groupId ORDER BY u.recommendationId";

            ObjectParameter param = new ObjectParameter("groupId", groupId);

            ObjectQuery<Recommendation> query =
              ((System.Data.Entity.Infrastructure.IObjectContextAdapter)Context).ObjectContext.CreateQuery<Recommendation>(sqlQuery, param);

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


        #endregion
    }

}