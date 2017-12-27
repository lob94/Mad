using System;
using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System.Collections.Generic;

namespace Es.Udc.DotNet.MiniPortal.Model.EventDao
{
    public interface IEventDao : IGenericDao<Event, Int64>
    {
        /// <summary>
        /// Finds a Event by name
        /// </summary>
        /// <param name="name">name</param>
        /// <param categoryId="categoryId">categoryId</param>
        /// <returns>The Event</returns>
        /// <exception cref="InstanceNotFoundException"/>
        ICollection<Event> FindByKeywordsAndCategory(String[] name, long categoryId, int startIndex, int count);

    }
}

