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
using Es.Udc.DotNet.MiniPortal.Web.Properties;
using Es.Udc.DotNet.MiniPortal.Model;

namespace Es.Udc.DotNet.MiniPortal.Web.Pages.EventPages
{
    public partial class Home : System.Web.UI.Page
    {
        String keywords;
        Boolean categoryForm = false;
        long categoryID = -1;
        int startIndex = 0;
        int count = 1;
        ICollection<EventDto> events;
        IEventService eventService;

        protected void Page_Load(object sender, EventArgs e)
        {
            callService();
            if (!IsPostBack)
            {
                
                initFromsValues();
                initDropDownListView();
                initGridView();
                PreviousNextButtons();
            }
            else
            {
                initFromsValues();
                initGridView();
                PreviousNextButtons();
            }
        }

        private void callService()
        {
            IIoCManager container = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            eventService = container.Resolve<IEventService>();
            ICollection<EventDto> eventDto = eventService.FindAllEvents();
        }
        private void initDropDownListView()
        {
            ICollection<Category> cat = eventService.FindAllCategories();
            dropDownList.DataSource = cat;
            dropDownList.DataBind();
        }
        private void initFromsValues()
        {
            keywords = Request.Params.Get("keywords");
            if (keywords == null)
            {
                keywords = "";
            }

            String startString = Request.Params.Get("startIndex");
            if (startString == null || startString == "0")
            {
                startIndex = 0;
            }
            else
            {
                startIndex = Convert.ToInt16(startString);
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
        private void initGridView()
        {
            if (categoryForm)
            {
                if(keywords != "")
                {
                    events = eventService.FindEventsByKeywordsAndCategory(keywords, categoryID, startIndex, count);
                }else
                {
                    events = eventService.FindEventsByCategory(categoryID, startIndex, count);
                }
            }else
            {
                events = eventService.FindEventsByKeywords(keywords, startIndex, count);
            }
            eventList2.DataSource = events;
            eventList2.DataBind();
        }

        private void PreviousNextButtons()
        {
            if ((startIndex - count) >= 0)
            {
                String url = "http://localhost:8082/Pages/EventPages/" + "Home.aspx" + "?startIndex=" + (startIndex - count);
                if(keywords != "")
                {
                    url += "&keywords=" + keywords;
                }
                if (categoryForm)
                {
                    url += "&categoryId=" + categoryID;
                }
                this.linkPrevious.NavigateUrl = Response.ApplyAppPathModifier(url);
                this.linkPrevious.Visible = true;
            }
            int numberResult;
            if (categoryForm)
                numberResult = eventService.CountFindEventsByKeywordsAndCategory(keywords, categoryID);
            else
                numberResult = eventService.CountFindEventsByKeywords(keywords);
            if((startIndex + count) < numberResult)
            {
                String url = "http://localhost:8082/Pages/EventPages/" + "Home.aspx" + "?startIndex=" + (startIndex + count);
                if (keywords != "")
                {
                    url += "&keywords=" + keywords;
                }
                if (categoryForm)
                {
                    url += "&categoryId=" + categoryID;
                }
                this.linkNext.NavigateUrl = Response.ApplyAppPathModifier(url);
                this.linkNext.Visible = true;
            }
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