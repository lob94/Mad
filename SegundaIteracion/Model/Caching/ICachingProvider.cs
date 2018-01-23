using System;
using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System.Collections.Generic;
using System.Runtime.Caching;


namespace Es.Udc.DotNet.MiniPortal.Model.Caching
{
    public interface ICachingProvider
    {
        /// <summary>
        /// Adds CacheItem to Cache
        /// </summary>
        void addItem();
        /// <summary>
        /// Finds CacheItem by key
        /// </summary>
        /// <param key="key">key</param>
        /// <returns>CacheItem</returns>
        CacheItem getItem(string key);
        /// <summary>
        /// Finds CacheItem by key
        /// </summary>
        /// <param key="key">key</param>
        /// <param remove="remove">remove</param>
        /// <returns>CacheItem</returns>
        CacheItem getItem(string key, bool remove);
        /// <summary>
        /// Clean cache
        /// </summary>
        void clean();
    }
}

