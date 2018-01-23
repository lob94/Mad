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
    public partial class MyGroupsAndRecommendationsPage : System.Web.UI.Page
    {
        int startIndexGroup = 0;
        int countGroup = 1;
        int startIndexRec = 0;
        int countRec = 1;
        long groupId=-1;
        ICollection<Recommendation> recommendations;
        ICollection<UserGroupDto> groupList;
        IUserService userService;
        IEventService eventService;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                callService();
                initFromValues();
                initGridViewMyGroups();          
            }
            else
            {
                callService();
                initFromValues();
                initGridViewMyGroups();
            }
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
                initGridViewMyRecommendations();
            }    
        }

        protected void initGridViewMyGroups()
        {
            UserProfileDetails userProfileDetails =
                   SessionManager.FindUserProfileDetails(Context);
            String email = userProfileDetails.Email;
            UserProfile u = userService.FindUserByEmail(email);
            groupList = userService.FindGroupsByUserId(u.usrId);
            myGroupsList.DataSource = groupList;
            myGroupsList.DataBind();
        }

        protected void initGridViewMyRecommendations()
        {
            UserProfileDetails userProfileDetails =
                 SessionManager.FindUserProfileDetails(Context);
            String email = userProfileDetails.Email;
            UserProfile u = userService.FindUserByEmail(email);
            recommendations = userService.FindGroupRecommendations(groupId, u.usrId, startIndexRec, countRec);
            recommendationList.DataSource = recommendations;
            recommendationList.DataBind();
        }

        protected void dropout_Click(object sender, EventArgs e)
        {
            if (SessionManager.IsUserAuthenticated(Context))
            {
                /*Obtain groupId*/
                Button btn = (Button)sender;
                GridViewRow gvr = (GridViewRow)btn.NamingContainer;
                String s = gvr.Cells[0].Text;
                ICollection<UserGroupDto> userGroup = userService.FindGroupByName(s, 0, 1);
                /*Obtain userId*/
                UserProfileDetails userProfileDetails =
                SessionManager.FindUserProfileDetails(Context);
                String email = userProfileDetails.Email;
                UserProfile u = userService.FindUserByEmail(email);
                /*Dropout from Group*/
                userService.UnJoinGroup(u.usrId, userGroup.First<UserGroupDto>().groupId);
                Response.Redirect("MyGroupsAndRecommendations.aspx");
               /* String url =
                   String.Format("./MyGroupsAndRecommendations.aspx?groupId={0}", groupId);
                   Response.Redirect(Response.ApplyAppPathModifier(url));
               */
            }
            else
                Response.Redirect("Authentication.aspx");
        }
 
    }
}