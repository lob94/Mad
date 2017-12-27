using Es.Udc.DotNet.MiniPortal.Model.EventService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System.Data;
using Es.Udc.DotNet.MiniPortal.Web.Properties;

namespace Es.Udc.DotNet.MiniPortal.Web.Pages.Event
{
    public partial class WebForm1 : System.Web.UI.Page
    {

        ObjectDataSource pbpDataSource = new ObjectDataSource();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // ObjectCreating is executed before ObjectDataSource creates 
                // an instance of the type used as DataSource (EventService).
                // We need to intercept this call to replace the standard creation
                // procedure (a new EventService() sentence) to use the Unity 
                // Container that allows to complete the dependences (eventDao,...)
                pbpDataSource.ObjectCreating += this.PbpDataSource_ObjectCreating;

                pbpDataSource.TypeName =
                     Settings.Default.ObjectDS_AccountOperations_Service;


                pbpDataSource.EnablePaging = true;

                pbpDataSource.SelectMethod =
                    Settings.Default.ObjectDS_AccountOperations_SelectMethod;

                /* Get Account Identifier */
                long accID = Convert.ToInt32(Request.Params.Get("accID"));

                /* Get the start date (without time) */
                DateTime startDate = Convert.ToDateTime(Request.Params.Get("startDate"));

                /* Get the end date (without time) */
                DateTime aux = Convert.ToDateTime(Request.Params.Get("endDate"));
                DateTime endDate = aux.AddSeconds(23 * 60 * 60 + 59 * 60 + 59);  // Adds 23:59:59

                pbpDataSource.SelectParameters.Add("eventIdentifier", DbType.Int64, accID.ToString());
                pbpDataSource.SelectParameters.Add("name", DbType.String, startDate.ToString());
                pbpDataSource.SelectParameters.Add("review", DbType.String, endDate.ToString());
                pbpDataSource.SelectParameters.Add("date", DbType.DateTime, endDate.ToString());
                pbpDataSource.SelectParameters.Add("categoryIdentifier", DbType.Int64, category.ToString());


               /* pbpDataSource.SelectCountMethod =
                    Settings.Default.ObjectDS_AccountOperations_CountMethod;
                pbpDataSource.StartRowIndexParameterName =
                    Settings.Default.ObjectDS_AccountOperations_StartIndexParameter;
                pbpDataSource.MaximumRowsParameterName =
                    Settings.Default.ObjectDS_AccountOperations_CountParameter;
                    */
                listEvents.AllowPaging = true;
                listEvents.PageSize = Settings.Default.MiniPortal_defaultCount;

                listEvents.DataSource = pbpDataSource;
                listEvents.DataBind();

            }
            catch (Exception)
            {
                label1.Visible = true;
            }
        }


        protected void EventPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            listEvents.PageIndex = e.NewPageIndex;
            listEvents.DataBind();
        }

        protected void PbpDataSource_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
        {

            /* Get the Service */
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IEventService eventService = iocManager.Resolve<IEventService>();

            e.ObjectInstance = eventService;
        }
    }
}