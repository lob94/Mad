using System;
using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;

namespace Es.Udc.DotNet.MiniPortal.Model.LabelDao
{
    public interface ILabelDao : IGenericDao<Label, Int64>
    {
        /// <summary>
        /// Finds a Label by name
        /// </summary>
        /// <param name="name">name</param>
        /// <returns>The UserGroup</returns>
        /// <exception cref="InstanceNotFoundException"/>
        Label FindByName(string name);
    }
}

