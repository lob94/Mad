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
    public partial class NewRecommendationForm : System.Web.UI.Page
    {
        long eventId = -1;
        long usrId = -1;
        ICollection<UserGroupDto> groups;
        IUserService userService;
        protected void Page_Load(object sender, EventArgs e)
        {
            IIoCManager container = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            userService = container.Resolve<IUserService>();
            initFromsValues();
           
            if (!IsPostBack)
            {
                initGridView();
            }
        }

        private void initFromsValues()
        {
            string eventIdS = Request.Params.Get("eventId");
            eventId = Convert.ToInt32(eventIdS);
            usrId = userService.FindUserByEmail(SessionManager.FindUserProfileDetails(Context).Email).usrId;
        }

        private void initGridView()
        {
            groups = userService.FindGroupsByUserId(usrId);
            groupsList.DataSource = groups;
            groupsList.DataBind();
        }

        protected void AddRecommendation_Click(object sender, EventArgs e)
        {
            if (SessionManager.IsUserAuthenticated(Context))
            {
                ICollection<long> groupsIds = new List<long>();
                string comment = textEntry.Text;
                foreach (GridViewRow row in groupsList.Rows)
                {
                    CheckBox cb = (CheckBox)row.FindControl("Sel");
                    if (cb != null && cb.Checked)
                    {

                        String s = row.Cells[0].Text;
                        UserGroupDto userGroup = userService.FindGroupsByName(s);
                        groupsIds.Add(userGroup.groupId);
                    }
                }

                userService.AddRecommendation(eventId, groupsIds, usrId, comment);
                Response.Redirect("Recommendations.aspx");
            }
            else
                Response.Redirect("Authentication.aspx");
        }
    }
}