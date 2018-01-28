using Es.Udc.DotNet.MiniPortal.Model.EventService;
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
    public partial class EditComment : System.Web.UI.Page
    {
        long eventId;
        IEventService eventService;
        protected void Page_Load(object sender, EventArgs e)
        {
            callService();
            eventId = Convert.ToInt32(Request.Params.Get("eventId"));
            string eventName = eventService.FindEventById(eventId).name;
            this.eventName.Text = eventName;
            this.lnkAddLabel.NavigateUrl = "~/Pages/EventPages/LabelComment.aspx?action=Add&commentId=" + Request.Params.Get("commentId");
            this.lnkRemoveLabel.NavigateUrl = "~/Pages/EventPages/LabelComment.aspx?action=Remove&commentId=" + Request.Params.Get("commentId");

            if (!IsPostBack)
            {
                this.txtEdit.Text = Request.Params.Get("content");
            }
        }

        protected void callService()
        {
            IIoCManager container = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            eventService = container.Resolve<IEventService>();
        }

        protected void BtnEditClick(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                /* Get data. */
                long commentId = Convert.ToInt32(Request.Params.Get("commentId"));
                long userId = SessionManager.GetUserSession(Context).UserProfileId;
                String newContent = this.txtEdit.Text;
                /*Edit Comment*/
                eventService.EditComment(commentId, userId, newContent);
                /* Redirect after editting */
                String url = String.Format("/Pages/EventPages/EventComments.aspx?eventId={0}", eventId);
                Response.Redirect(Response.ApplyAppPathModifier(url));
            }
        }
    }
}