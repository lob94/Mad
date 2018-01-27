using System;

using Es.Udc.DotNet.MiniPortal.Web.HTTP.Session;

namespace Es.Udc.DotNet.MiniPortal.Web
{

    public partial class Miniportal : System.Web.UI.MasterPage
    {

        public static readonly String USER_SESSION_ATTRIBUTE = "userSession";

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!SessionManager.IsUserAuthenticated(Context))
            {

                if (lblDash2 != null)
                    lblDash2.Visible = false;
                if (lnkUpdate != null)
                    lnkUpdate.Visible = false;
                if (lblDash3 != null)
                    lblDash3.Visible = false;
                if (lnkLogout != null)
                    lnkLogout.Visible = false;
                if (lblDash4 != null)
                    lblDash4.Visible = false;
                if (lnkGroups != null)
                    lnkGroups.Visible = false;
                if (lblDash5 != null)
                    lblDash5.Visible = false;
                if (lnkNewGroup != null)
                    lnkNewGroup.Visible = false;
                if (lblDash6 != null)
                    lblDash6.Visible = false;
                if (lnkMyGroups != null)
                    lnkMyGroups.Visible = false;
                if (lblDash7 != null)
                    lblDash7.Visible = false;
                if (lnkRecommendations != null)
                    lnkRecommendations.Visible = false;
                if (lblDash8 != null)
                    lblDash8.Visible = false;
                if (lnkEvents != null)
                    lnkEvents.Visible = false;

            }
            else
            {
                if (lblWelcome != null)                   
                    lblWelcome.Text =
                        GetLocalResourceObject("lblWelcome.Hello.Text").ToString()
                        + " " + SessionManager.GetUserSession(Context).FirstName;
                if (lblDash1 != null)
                    lblDash1.Visible = false;
                if (lnkAuthenticate != null)
                    lnkAuthenticate.Visible = false;
            }
        }
    }
}
