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
                initGridView();
            }
            else
            {
                initFromValues();
                initGridView();
            }
        }
        protected void callService()
        {
            IIoCManager container = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            eventService = container.Resolve<IEventService>();

        }
        private void initGridView()
        {
            try
            {
                ICollection<CommentDto> comentarios = eventService.FindAllComments(evento.eventId, 0, 10);
                comentariosList.DataSource = comentarios;
                comentariosList.DataBind();
            }
            catch{ }
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
                String contentComment = introducirComentario.Text;
                UserProfileDetails userProfileDetails =
                    SessionManager.FindUserProfileDetails(Context);
                String login = userProfileDetails.Email;
                UserProfile u = userService.FindUserByLoginName(login);
                
                DateTime commentDate = DateTime.Now;

                Comment c = new Comment();
                c.content = contentComment;
                c.loginName = login;
                c.commentDate = commentDate;
                c.Event = evento;
                eventService.AddComment(contentComment,evento.eventId,u.usrId);
            }
            catch{
                throw new Exception();
            }
        }
    }
}