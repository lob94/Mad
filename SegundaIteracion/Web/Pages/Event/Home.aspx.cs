using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.MiniPortal.Model.EventService;
using Microsoft.Practices.EnterpriseLibrary;
using Es.Udc.DotNet.ModelUtil.IoC;

namespace Es.Udc.DotNet.MiniPortal.Web.Pages.Event
{
    public partial class Home : System.Web.UI.Page
    {
        ObjectDataSource dataSource = new ObjectDataSource();
        ObjectDataSource dropDataSource = new ObjectDataSource();
        protected void Page_Load(object sender, EventArgs e)
        {
            initGridView();
        }
        protected void dataSource_CreateObject(object sender, ObjectDataSourceEventArgs e)
        {
            IIoCManager container = (IIoCManager)HttpContext.Current.Application["unityContainer"];
            IEventService eventService = container.Resolve<IEventService>();
            e.ObjectInstance = (IEventService)eventService;

        }
        private void initGridView()
        {
            dataSource.ObjectCreating += this.dataSource_CreateObject;
            dataSource.TypeName = "Es.Udc.DotNet.MiniPortal.Model.EventService.IEventService";
                dataSource.SelectMethod = "FindAllEvents";

            eventList.DataSource = dataSource;
            eventList.DataBind();
        }
    }
}