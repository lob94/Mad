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

namespace Es.Udc.DotNet.MiniPortal.Web.Pages.EventPages
{
    public partial class LabelComments : System.Web.UI.Page
    {
        long labelId;
        IEventService eventService;
        IUserService userService;

        protected void Page_Load(object sender, EventArgs e)
        {
            callService();
            initFromValues();
            initGridView();
        }

        protected void callService()
        {
            IIoCManager container = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            eventService = container.Resolve<IEventService>();
            userService = container.Resolve<IUserService>();

        }

        protected void initFromValues()
        {
            string labelString = Request.Params.Get("labelId");
            labelId = Convert.ToInt32(labelString);

        }

        private void initGridView()
        {
            try
            {
                LabelDto labelDto = eventService.Find(labelId);
                commentsList.DataSource = labelDto.comments;
                commentsList.DataBind();

            }
            catch { }
        }

        protected Boolean visibility(String loginName)
        {
            Boolean b = true;
            if (SessionManager.IsUserAuthenticated(Context))
            {
                UserProfile u = userService.FindUserByEmail(SessionManager.FindUserProfileDetails(Context).Email);
                if (u.loginName != loginName)
                {
                    b = false;
                }
            }
            else
            {
                b = false;
            }
            return b;
        }

        protected void edit_Click(object sender, CommandEventArgs e)
        {
            string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });

            String url =
                String.Format("EditComment.aspx?comment={0}&reason={1}&eventId={2}", commandArgs[0], commandArgs[1], commandArgs[2]);
            Response.Redirect(url);
        }

        protected void remove_Click(object sender, CommandEventArgs e)
        {
            long cId = Convert.ToInt32(e.CommandArgument);

            long usrId = SessionManager.GetUserSession(Context).UserProfileId;
            eventService.DeleteComment(cId, usrId);
        }

    }
}