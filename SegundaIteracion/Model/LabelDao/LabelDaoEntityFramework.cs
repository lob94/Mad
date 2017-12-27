using System;
using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Common;


namespace Es.Udc.DotNet.MiniPortal.Model.LabelDao
{
    public class LabelDaoEntityFramework :
        GenericDaoEntityFramework<Label, Int64>, ILabelDao
    {

        public LabelDaoEntityFramework() { }

        #region ILabelDao Members

        public Label FindByName(string name)
        {
            Label Label = null;

            #region Option 3: Using Entity SQL and Object Services provided by old ObjectContext.

            String sqlQuery =
                "SELECT VALUE u FROM MiniPortalEntities.Labels AS u " +
                "WHERE u.name=@name";

            ObjectParameter param = new ObjectParameter("name", name);

            ObjectQuery<Label> query =
              ((System.Data.Entity.Infrastructure.IObjectContextAdapter)Context).ObjectContext.CreateQuery<Label>(sqlQuery, param);

            var result = query.Execute(MergeOption.AppendOnly);

            try
            {
                Label = result.First<Label>();
            }
            catch (Exception)
            {
                Label = null;
            }

            #endregion

            if (Label == null)
                throw new InstanceNotFoundException(name,
                    typeof(Label).ToString());

            return Label;
        }
        #endregion
    }

}

