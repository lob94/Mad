using System;
using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;

namespace Es.Udc.DotNet.MiniPortal.Model.UserGroup1Dao
{
    public interface IUserGroup1Dao : IGenericDao<UserGroup, Int64>
    {
        /// <summary>
        /// Finds a UserGroup1 by name
        /// </summary>
        /// <param name="name">name</param>
        /// <returns>The UserGroup</returns>
        /// <exception cref="InstanceNotFoundException"/>
        UserGroup FindByName(string name);

    }
}

