﻿using System;
using Es.Udc.DotNet.ModelUtil.Dao;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Collections.Generic;
using Es.Udc.DotNet.MiniPortal.Model.Caching;
using Ninject;
using Microsoft.Practices.EnterpriseLibrary.Caching;

namespace Es.Udc.DotNet.MiniPortal.Model.EventDao
{
    public class EventDaoEntityFramework :
        GenericDaoEntityFramework<Event, Int64>, IEventDao
    { 
        public EventDaoEntityFramework() { }


        #region IEventDao Members

        public ICollection<Event> FindByKeywordsAndCategory(String[] name, long categoryId, int startIndex, int count)
        {
            ICollection<Event> events = new List<Event>();
              ObjectQuery<Event> query = getFindQuery(name, categoryId);

                var result = query.Skip(startIndex).Take(count).ToList<Event>();

                try
                {
                    events = result.ToList<Event>();
                }
                catch (Exception) {    }
             
            return events;
        }

        private System.Data.Entity.Core.Objects.ObjectQuery<Event> getFindQuery(string[] name, long categoryId)
        {
            int i = 0;
            String sqlQuery =
                "SELECT VALUE u FROM MiniPortalEntities.Events AS u ";

            if (categoryId != -1)
            {
                sqlQuery += "WHERE u.category.categoryId = @categoryId ";
                foreach (String s in name)
                {
                    sqlQuery += "AND u.name LIKE '%" + s + "%' ";
                }
            }
            else
            {
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
            }

            sqlQuery += "ORDER BY u.eventId";
            
            if(categoryId != -1)
            {
                ObjectParameter pCategoryId = new ObjectParameter("categoryId", categoryId);

                ObjectQuery<Event> query =
                  ((System.Data.Entity.Infrastructure.IObjectContextAdapter)Context).ObjectContext.CreateQuery<Event>(sqlQuery, pCategoryId);
                return query;
            }
            else
            {
                ObjectQuery<Event> query =
                  ((System.Data.Entity.Infrastructure.IObjectContextAdapter)Context).ObjectContext.CreateQuery<Event>(sqlQuery);
                return query;
            }
        }

        public int CountFindEvents(String[] name, long categoryId)
        {
            int result = getFindQuery(name, categoryId).Count();
            return result;
        }

        #endregion
    }

}


