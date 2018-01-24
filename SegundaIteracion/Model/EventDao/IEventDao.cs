using System;
using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System.Collections.Generic;
using Es.Udc.DotNet.MiniPortal.Model.Caching;
using Ninject;
using Microsoft.Practices.EnterpriseLibrary.Caching;

namespace Es.Udc.DotNet.MiniPortal.Model.EventDao
{
    public interface IEventDao : IGenericDao<Event, Int64>
    {
        [Inject]
        ICachingProvider cachingProvider { set; }


        /// <summary>
        /// Finds a Event by name
        /// </summary>
        /// <param name="name">name</param>
        /// <param categoryId="categoryId">categoryId</param>
        /// <returns>The Event</returns>
        /// <exception cref="InstanceNotFoundException"/>
        ICollection<Event> FindByKeywordsAndCategory(String[] name, long categoryId, int startIndex, int count);

        /// <summary>
        /// Finds Total Event by name and category
        /// </summary>
        /// <param name="name">name</param>
        /// <param categoryId="categoryId">categoryId</param>
        /// <returns>Total events od this search</returns>
        int CountFindEvents(String[] name, long categoryId);

        /// <summary>
        /// Removes all elements from cache
        /// </summary>
        void CleanCache();

    }
}

