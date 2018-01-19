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
using Es.Udc.DotNet.MiniPortal.Model.UserService;

namespace Es.Udc.DotNet.MiniPortal.Web.Pages.GroupPages
{
    public partial class Groups : System.Web.UI.Page
    {
        String keywords;
        Boolean categoryForm = false;
        long categoryID = -1;
        int startIndex = 0;
        int count = 1;
        ICollection<UserGroupDto> groups;
        IUserService userService;

        protected void Page_Load(object sender, EventArgs e)
        {
            IIoCManager container = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            userService = container.Resolve<IUserService>();
            initFromsValues();
            initGridView();
            //PreviousNextButtons();
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
        }
        private void initGridView()
        {
            groups = userService.FindAllGroups();
            groupList.DataSource = groups;
            groupList.DataBind();
        }

        protected void subs_Click(object sender, EventArgs e)
        {

        }

        /*private void PreviousNextButtons()
        {
            if ((startIndex - count) >= 0)
            {
                String url = "http://localhost:8082/Pages/EventPages/" + "Home.aspx" + "?startIndex=" + (startIndex - count);
                if (keywords != "")
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
            if ((startIndex + count) < numberResult)
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
        }*/

        protected void search_Click(object sender, EventArgs e)
        {
            /*if (Page.IsValid)
            {*/
                /* Get data. */

               /* String keywords = textEntry.Text;
                String categoryId = dropDownList.SelectedItem.Value;*/
                /* Do action. */
               /* String url =
                    String.Format("./Home.aspx?keywords={0}&categoryId={1}", keywords, categoryId);

                Response.Redirect(Response.ApplyAppPathModifier(url));

            }*/
        }
    }
}