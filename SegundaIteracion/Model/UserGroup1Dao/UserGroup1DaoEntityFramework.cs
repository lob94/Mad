using System;
using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System.Linq;
using System.Data.Entity.Core.Objects;
using System.Collections.Generic;

namespace Es.Udc.DotNet.MiniPortal.Model.UserGroup1Dao
{
    public class UserGroup1DaoEntityFramework :
        GenericDaoEntityFramework<UserGroup, Int64>, IUserGroup1Dao
    {

        public UserGroup1DaoEntityFramework() { }

        #region IUserGroup1Dao Members

        public UserGroup FindByName(string name)
        {
            UserGroup UserGroup = null;

            #region Option 3: Using Entity SQL and Object Services provided by old ObjectContext.

            String sqlQuery =
                "SELECT VALUE u FROM MiniPortalEntities.UserGroups AS u " +
                "WHERE u.name=@name";

            ObjectParameter param = new ObjectParameter("name", name);

            ObjectQuery<UserGroup> query =
              ((System.Data.Entity.Infrastructure.IObjectContextAdapter)Context).ObjectContext.CreateQuery<UserGroup>(sqlQuery, param);

            var result = query.Execute(MergeOption.AppendOnly);

            try
            {
                UserGroup = result.First<UserGroup>();
            }
            catch (Exception)
            {
                UserGroup = null;
            }

            #endregion

            if (UserGroup == null)
                throw new InstanceNotFoundException(name,
                    typeof(UserGroup).ToString()); //FullName ??????????????????????????

            return UserGroup;
        }

        public ICollection<UserGroup> FindAllGroupsPagination(int startIndex, int count)
        {

            ICollection<UserGroup> events = new List<UserGroup>();

            String sqlQuery =
               "SELECT VALUE u FROM MiniPortalEntities.UserGroups AS u " + "ORDER BY u.groupId"; ;
            ObjectQuery<UserGroup> queryI =
             ((System.Data.Entity.Infrastructure.IObjectContextAdapter)Context).ObjectContext.CreateQuery<UserGroup>(sqlQuery);
            ObjectQuery<UserGroup> query = queryI;

            var result = query.Skip(startIndex).Take(count).ToList<UserGroup>();

            try
            {
                events = result.ToList<UserGroup>();
            }
            catch (Exception)
            {
            }

            return events;
        }

        #endregion
    }

}