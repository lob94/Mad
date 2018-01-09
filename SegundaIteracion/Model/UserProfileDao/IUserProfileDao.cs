﻿using System;
using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;

namespace Es.Udc.DotNet.MiniPortal.Model.UserProfileDao
{
    public interface IUserProfileDao : IGenericDao<UserProfile, Int64>
    {
        /// <summary>
        /// Finds a UserProfile by loginName
        /// </summary>
        /// <param name="loginName">loginName</param>
        /// <returns>The UserProfile</returns>
        /// <exception cref="InstanceNotFoundException"/>
        UserProfile FindByLoginName(String loginName);

        /// <summary>
        /// Finds a UserProfile by email
        /// </summary>
        /// <param name="email">email</param>
        /// <returns>The UserProfile</returns>
        /// <exception cref="InstanceNotFoundException"/>
        UserProfile FindByEmail(String email);
    }
}

