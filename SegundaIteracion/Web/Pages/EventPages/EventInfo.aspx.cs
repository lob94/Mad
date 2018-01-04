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

namespace Es.Udc.DotNet.MiniPortal.Web.Pages.EventInfo
{
    public partial class EventInfo : System.Web.UI.Page
    {
        long evId;
        IEventService eventService;
        Event evento;
      
        protected void Page_Load(object sender, EventArgs e)
        {
            callService();
            if (!IsPostBack)
            {
                
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
            /* dataSource.ObjectCreating += this.dataSource_CreateObject;     
             dataSource.TypeName = "Es.Udc.DotNet.MiniPortal.Model.EventService.IEventService";
             dataSource.SelectMethod = "FindAllComments";

             comentarios.DataSource = dataSource;
             comentarios.DataBind();*/
            eventService.FindAllComments(evento.eventId,0,1);
        }

        protected void initFromValues()
        {
            string evString = Request.Params.Get("eventId");
            evId = Convert.ToInt32(evString);
            evento = eventService.FindEventById(evId);



        }

        protected void addComentario_Click(object sender, EventArgs e)
        {
            try {
                String contentComment = introducirComentario.Text;
                UserProfileDetails userProfileDetails =
                    SessionManager.FindUserProfileDetails(Context);
                String login = userProfileDetails.Email;
                //Necesitamos el userId
                
                DateTime commentDate = DateTime.Now;

                Comment c = new Comment();
                c.content = contentComment;
                c.loginName = login;
                c.commentDate = commentDate;
                c.Event = evento;
                eventService.AddComment(c,evento.eventId.);

            }
            catch{

            }
        }
    }
}