using Es.Udc.DotNet.MiniPortal.Model;
using Es.Udc.DotNet.MiniPortal.Model.EventService;
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

    }
}