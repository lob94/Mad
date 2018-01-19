﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.MiniPortal.Model;
using Es.Udc.DotNet.MiniPortal.Model.EventService;
using Es.Udc.DotNet.MiniPortal.Model.UserService;
using Es.Udc.DotNet.ModelUtil.IoC;

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
            initFromValues();
            initGridView();

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
    }
}