using System;
using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Common;


namespace Es.Udc.DotNet.MiniPortal.Model.CategoryDao
{
    public class CategoryDaoEntityFramework :
        GenericDaoEntityFramework<Category, Int64>, ICategoryDao
    {

        public CategoryDaoEntityFramework() { }

        #region ICategoryDao Members

        #endregion
    }

}

