using Es.Udc.DotNet.MiniPortal.Model;
using Es.Udc.DotNet.MiniPortal.Model.EventService;
using Es.Udc.DotNet.MiniPortal.Model.UserService;
using Es.Udc.DotNet.MiniPortal.Web.HTTP.Session;
using Es.Udc.DotNet.ModelUtil.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.MiniPortal.Web.Pages.GroupPages
{
    public partial class RecommendationsPage : System.Web.UI.Page
    {
        int startIndex = 0;
        int count = 1;
        long groupId = -1;
        ICollection<RecommendationDto> recommendations;
        IUserService userService;
        IEventService eventService;
        protected void Page_Load(object sender, EventArgs e)
        {
            callService();
            initFromValues();
            initGridViewMyRecommendations();
            PreviousNextButtons();
        }

        protected void callService()
        {
            IIoCManager container = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            userService = container.Resolve<IUserService>();
            eventService = container.Resolve<IEventService>();
        }

        protected void initFromValues()
        {
            String groupIdString = Request.Params.Get("groupId");
            if (groupIdString == null)
            {
                groupId = -1;
            }
            else
            {
                groupId = Convert.ToInt32(groupIdString);
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

        protected void initGridViewMyRecommendations()
        {
            UserProfileDetails userProfileDetails =
                 SessionManager.FindUserProfileDetails(Context);
            String email = userProfileDetails.Email;
            UserProfile u = userService.FindUserByEmail(email);
            recommendations = userService.FindGroupRecommendations(groupId, u.usrId, startIndex, count);
            recommendationList.DataSource = recommendations;
            recommendationList.DataBind();
        }

        private void PreviousNextButtons()
        {
            if ((startIndex - count) >= 0)
            {
                String url = "http://localhost:8082/Pages/GroupPages/" + "Recommendations.aspx" + "?groupId=" 
                    + groupId + "&startIndex=" + (startIndex - count);
                
                this.linkPrevious.NavigateUrl = Response.ApplyAppPathModifier(url);
                this.linkPrevious.Visible = true;
            }
            int numberResult;
            
                numberResult = userService.CountFindGroupRecommendation(groupId);
            if ((startIndex + count) < numberResult)
            {
                String url = "http://localhost:8082/Pages/GroupPages/" + "Recommendations.aspx" + "?groupId="
                    + groupId + "&startIndex=" + (startIndex + count);
                
                this.linkNext.NavigateUrl = Response.ApplyAppPathModifier(url);
                this.linkNext.Visible = true;
            }
        }

    }
}