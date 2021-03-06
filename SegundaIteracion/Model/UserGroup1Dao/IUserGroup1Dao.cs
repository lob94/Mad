﻿using System;
using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System.Collections.Generic;

namespace Es.Udc.DotNet.MiniPortal.Model.UserGroup1Dao
{
    public interface IUserGroup1Dao : IGenericDao<UserGroup, Int64>
    {
        /// <summary>
        /// Finds a list of UserGroup1 by name
        /// </summary>
        /// <param name="name">name</param>
        /// <param startIndex="StartIndex">name</param>
        /// <param count="Count">name</param>
        /// <returns>A list of UserGroups that matches the keywords</returns>/>
        ICollection<UserGroup> FindByKeywords(string[] name, int startIndex, int count);

        /// <summary>
        /// Finds a UserGroup1 by name
        /// </summary>
        /// <param name="name">name</param>
        /// <returns>The UserGroup</returns>
        /// <exception cref="InstanceNotFoundException"/>
        UserGroup FindByName(string name);

        /// <summary>
        /// Finds a list of UserGroup1 by name
        /// </summary>
        /// <param name="name">name</param>
        /// <returns>A list of UserGroups that matches the keywords</returns>/>
        int CountFindGroupsByName(String[] name);
    }
}

