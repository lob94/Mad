﻿using System;
using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System.Collections.Generic;

namespace Es.Udc.DotNet.MiniPortal.Model.RecommendationDao
{
    public interface IRecommendationDao : IGenericDao<Recommendation, Int64>
    {
        /// <summary>
        /// Finds a Recommendation by Group
        /// </summary>
        /// <param groupId="groupId">groupId</param>
        /// <returns>The Recommendation</returns>
        /// <exception cref="InstanceNotFoundException"/>
        ICollection<Recommendation> FindByGroupId(long id, int startIndex, int count);

    }
}

