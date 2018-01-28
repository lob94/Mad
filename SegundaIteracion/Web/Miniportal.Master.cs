using System;

using Es.Udc.DotNet.MiniPortal.Web.HTTP.Session;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.MiniPortal.Model.EventService;
using System.Web;
using Es.Udc.DotNet.MiniPortal.Web.Properties;

namespace Es.Udc.DotNet.MiniPortal.Web
{

    public partial class Miniportal : System.Web.UI.MasterPage
    {

        public static readonly String USER_SESSION_ATTRIBUTE = "userSession";
        private long totalReferences;

        protected void Page_Load(object sender, EventArgs e)
        {

            /* Get the Service */
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IEventService eventService = iocManager.Resolve<IEventService>();

            if (!IsPostBack)
            {
                this.GvLabels.DataSource = eventService.GetLabelsDtos();
                this.GvLabels.DataBind();
            }

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
                if (lnkAddLabel != null)
                    lnkAddLabel.Visible = false;
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

        protected int GetFontSize(Object references)
        {
            /* Get the Service */
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IEventService eventService = iocManager.Resolve<IEventService>();

            if (totalReferences == 0)
            {
                totalReferences = eventService.GetTotalReferences();
            }
            int size = Settings.Default.Labels_FontSize;
            int increment = Settings.Default.Labels_FontIncrement;

            for (int i = Settings.Default.Labels_NumFontIncrements; i > 1; i--)
            {
                if ((int)references > totalReferences / i)
                {
                    size += increment;
                }
            }
            return size;
        }
    }
}
