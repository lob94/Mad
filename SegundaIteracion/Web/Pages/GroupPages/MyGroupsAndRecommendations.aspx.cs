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
            callService();
            initFromValues();
            initGridViewMyGroups();
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

        }
    }
}