using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.MiniPortal.Model.EventService;
using Microsoft.Practices.EnterpriseLibrary;
using Es.Udc.DotNet.ModelUtil.IoC;
using Ninject;
using System.Collections;
using System.Data;

namespace Es.Udc.DotNet.MiniPortal.Web.Pages.EventPages
{
    public partial class Home : System.Web.UI.Page
    {
        long categoryID = 1;

        ObjectDataSource dataSource = new ObjectDataSource();
        ObjectDataSource dropDataSource = new ObjectDataSource();
        protected void Page_Load(object sender, EventArgs e)
        {
            initGridView();
        }
        protected void dataSource_CreateObject(object sender, ObjectDataSourceEventArgs e)
        {
            IIoCManager container = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IEventService eventService = container.Resolve<IEventService>();

            e.ObjectInstance = (IEventService)eventService;

        }
        private void initGridView()
        {
            dataSource.ObjectCreating += this.dataSource_CreateObject;     
            dataSource.TypeName = "Es.Udc.DotNet.MiniPortal.Model.EventService.IEventService";
            dataSource.SelectMethod = "FindAllEvents";
           

            eventList2.DataSource = dataSource;
            eventList2.DataBind();
        }
    }
}