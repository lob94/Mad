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
using Es.Udc.DotNet.MiniPortal.Web.HTTP.Session;

namespace Es.Udc.DotNet.MiniPortal.Web.Pages.Event
{
    public partial class Home : System.Web.UI.Page
    {
        String keywords;
        Boolean categoryForm = false;
        long categoryID = -1;
        IEventService eventService;

        ObjectDataSource dataSource = new ObjectDataSource();
        ObjectDataSource dropDataSource = new ObjectDataSource();
        protected void Page_Load(object sender, EventArgs e)
        {
            IIoCManager container = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            eventService = container.Resolve<IEventService>();
            ICollection<EventDto> eventDto = eventService.FindAllEvents();
            initFromsValues();
            initDropDownListView();
            initGridView();
        }
        protected void dropDataSource_CreateObject(object sender, ObjectDataSourceEventArgs e)
        {
            IIoCManager container = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IEventService eventService = container.Resolve<IEventService>();
            
            e.ObjectInstance = (IEventService)eventService;

        }
        private void initDropDownListView()
        {
            dropDataSource.ObjectCreating += this.dropDataSource_CreateObject;
            dropDataSource.TypeName = "Es.Udc.DotNet.MiniPortal.Model.EventService.IEventService";
            dropDataSource.SelectMethod = "FindAllCategories";


            dropDownList.DataSource = dropDataSource;
            dropDownList.DataBind();
        }
        private void initFromsValues()
        {
            keywords = Request.Params.Get("keywords");
            if (keywords == null)
            {
                keywords = "";
            }

            String catString = Request.Params.Get("categoryId");
            if (catString == null || catString == "0")
            {
                categoryForm = false;
            }
            else
            {
                categoryForm = true;
                categoryID = Convert.ToInt32(catString);

            }
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



            if (categoryForm)
            {
                dataSource.SelectParameters.Add("categoryId", DbType.Int32, categoryID.ToString());
                if(keywords != "")
                {
                    dataSource.SelectParameters.Add("name", DbType.String, keywords);
                    dataSource.SelectMethod = "FindEventsByKeywordsAndCategory";
                    dataSource.SelectCountMethod = "CountFindEventsByKeywordsAndCategory";
                }else
                {
                    dataSource.SelectMethod = "FindEventsByCategory";
                    dataSource.SelectCountMethod = "CountFindEventsByCategory";
                }
            }else
            {
                dataSource.SelectParameters.Add("name", DbType.String, keywords);
                dataSource.SelectMethod = "FindEventsByKeywords";
                dataSource.SelectCountMethod = "CountFindEventsByKeywords";
            }
            

            

            eventList2.AllowPaging = true;
            eventList2.PageSize = 10;
            eventList2.DataSource = dataSource;
            eventList2.DataBind();
        }
        protected void search_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                /* Get data. */
                
                String keywords = textEntry.Text;
                String categoryId = dropDownList.SelectedItem.Value;
                /* Do action. */
                String url =
                    String.Format("./Home.aspx?keywords={0}&categoryId={1}", keywords, categoryId);

                Response.Redirect(Response.ApplyAppPathModifier(url));

            }
        }
    }
}