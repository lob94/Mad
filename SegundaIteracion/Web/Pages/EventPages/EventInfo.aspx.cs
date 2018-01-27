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
using Es.Udc.DotNet.MiniPortal.Model.UserService;
using Es.Udc.DotNet.MiniPortal.Model;

namespace Es.Udc.DotNet.MiniPortal.Web.Pages.EventPages
{
    public partial class EventInfo : System.Web.UI.Page
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
            }
            else
            {
                initFromValues();
            }
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
            nombre.Text = evento.name;
            categoria.Text = evento.Category.name;
            review.Text = evento.review;
            date.Text = evento.eventDate.ToString();
           
        }

        protected void addComentario_Click(object sender, EventArgs e)
        {
            try {
                if (SessionManager.IsUserAuthenticated(Context))
                {
                    String contentComment = introducirComentario.Text;
                    UserProfileDetails userProfileDetails =
                        SessionManager.FindUserProfileDetails(Context);
                    String email = userProfileDetails.Email;
                    UserProfile u = userService.FindUserByEmail(email);

                    DateTime commentDate = DateTime.Now;

                    Comment c = new Comment();
                    c.content = contentComment;
                    c.loginName = u.loginName;
                    c.commentDate = commentDate;
                    c.Event = evento;
                    eventService.AddComment(contentComment, evento.eventId, u.usrId);
                }
            }
            catch{
                throw new Exception();
            }
        }
        protected void addRecommendation_Click(object sender, EventArgs e)
        {
            String url = "http://localhost:8082/Pages/GroupPages/" + "NewRecommendationForm.aspx" + "?eventId=" + evId;
            Response.Redirect(url);
        }
        protected void comments_Click(object sender, EventArgs e)
        {
            String url = "http://localhost:8082/Pages/EventPages/" + "EventComments.aspx" + "?eventId=" + evId;
            Response.Redirect(url);
        }
    }
}