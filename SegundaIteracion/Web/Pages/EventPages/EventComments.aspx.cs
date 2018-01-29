using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.MiniPortal.Model;
using Es.Udc.DotNet.MiniPortal.Model.EventService;
using Es.Udc.DotNet.MiniPortal.Model.UserService;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.MiniPortal.Web.HTTP.Session;

namespace Es.Udc.DotNet.MiniPortal.Web.Pages.EventPages
{
    public partial class EventComments : System.Web.UI.Page
    {
        long evId;
        IEventService eventService;
        IUserService userService;
        Event evento;

        protected void Page_Load(object sender, EventArgs e)
        {
            callService();
            if (!IsPostBack)
            {
                initFromValues();
                initGridView();
            }
        }

        private void initGridView()
        {
            try
            {
                ICollection<CommentDto> comentarios = eventService.FindAllComments(evento.eventId, 0, 10);
              
                    comentariosList.DataSource = comentarios;
                    comentariosList.DataBind();
                
            }
            catch { }
        }

        protected void callService()
        {
            IIoCManager container = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            eventService = container.Resolve<IEventService>();
            userService = container.Resolve<IUserService>();

        }

        protected void initFromValues()
        {
            string evString = Request.Params.Get("eventId");
            evId = Convert.ToInt32(evString);
            evento = eventService.FindEventById(evId);

        }

        protected void volver_Click(object sender, EventArgs e)
        {
            String url =
                    String.Format("./Home.aspx");

            Response.Redirect(Response.ApplyAppPathModifier(url));
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
            }else
            {
                b = false;
            }
            return b;
        }

        protected void edit_Click(object sender, CommandEventArgs e)
        {
            string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });

            String url = 
                String.Format("EditComment.aspx?comment={0}&reason={1}&eventId={2}", commandArgs[0], commandArgs[1], evId);
            Response.Redirect(url);
        }

        protected void remove_Click(object sender, CommandEventArgs e)
        {
            long cId = Convert.ToInt32(e.CommandArgument);

            long usrId = SessionManager.GetUserSession(Context).UserProfileId;
            eventService.DeleteComment(cId, usrId);
            String url =
                String.Format("EventComments.aspx?eventId={0}", evId);
            Response.Redirect(url);
        }
    }
}