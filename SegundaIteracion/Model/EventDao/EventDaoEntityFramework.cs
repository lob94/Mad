using System;
using Es.Udc.DotNet.ModelUtil.Dao;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Collections.Generic;
using Es.Udc.DotNet.MiniPortal.Model.Caching;
using Ninject;

namespace Es.Udc.DotNet.MiniPortal.Model.EventDao
{
    public class EventDaoEntityFramework :
        GenericDaoEntityFramework<Event, Int64>, IEventDao
    {
        [Inject]
        public ICachingProvider cachingProvider { get; set; }

        public EventDaoEntityFramework() { }


        #region IEventDao Members

        public ICollection<Event> FindByKeywordsAndCategory(String[] name, long categoryId, int startIndex, int count)
        {
            ICollection<Event> events = new List<Event>();

            /*Necesario para el caching*/
            String items = "";
            /*Para caching*/
            foreach (var item in name)
            {
                items += " " + item.ToLower();
            }
            /*Resultado lista de cache*/
            List<Event> cacheResult = (List<Event>)cachingProvider.GetItem(items, false);
            /*Si la llamada está en caché, devolvemos esa*/
            if (cacheResult != null)
            {
                events = cacheResult
                    .Skip(startIndex)
                    .Take(count)
                    .ToList();
                cachingProvider.AddItem(items, events);
                return events;
            }
                /*En caso de que no esté cacheada, hacemos la llamada*/
                ObjectQuery<Event> query = getFindQuery(name, categoryId);

                var result = query.Skip(startIndex).Take(count).ToList<Event>();

                try
                {
                    events = result.ToList<Event>();
                }
                catch (Exception) {    }
               /*Añadimos el resultado de la nueva llamada a cache*/
                cachingProvider.AddItem(items, events);
            /*Devolvemos la nueva llamada*/
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

            if (categoryId != -1)
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

        public void CleanCache()
        {
            cachingProvider.Clean();
        }

        #endregion
    }

}


