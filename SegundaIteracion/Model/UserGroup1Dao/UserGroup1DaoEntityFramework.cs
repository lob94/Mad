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

        public ICollection<UserGroup> FindByName(string[] name, int startIndex, int count)
        {
            int i=0;
            ICollection<UserGroup> UserGroups = new List<UserGroup>();

            #region Option 3: Using Entity SQL and Object Services provided by old ObjectContext.

            String sqlQuery =
                "SELECT VALUE u FROM MiniPortalEntities.UserGroups AS u ";

            foreach (String s in name)
            {
                if (i == 0)
                {
                    sqlQuery += "WHERE u.name LIKE '%" + s + "%' ";
                    i++;
                }
                else
                {
                    sqlQuery += "AND u.name LIKE '%" + s + "%' ";
                }
            }

            sqlQuery += "ORDER BY u.groupId";

            ObjectQuery<UserGroup> query =
            ((System.Data.Entity.Infrastructure.IObjectContextAdapter)Context).ObjectContext.CreateQuery<UserGroup>(sqlQuery);

            var result = query.Skip(startIndex).Take(count).ToList<UserGroup>();

            try
            {
                UserGroups = result.ToList<UserGroup>();
            }
            catch (Exception)
            {
            }

            #endregion

            return UserGroups;
        }

        #endregion
    }

}