using Es.Udc.DotNet.MiniPortal.Model.EventService;
using Es.Udc.DotNet.MiniPortal.Web.HTTP.Session;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.ModelUtil.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.MiniPortal.Web.Pages.EventPages
{
    public partial class AddLabel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void CreateLabelClick(object sender, EventArgs e)
        {

            if (Page.IsValid)
            {
                /* Create an Account. */
                String labelName = txtLabel.Text;

                Model.Label label = new Model.Label();
                label.name = labelName;

                /* Get the Service */
                IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
                IEventService eventService = iocManager.Resolve<IEventService>();

                try
                {
                    Model.Label labelCreated = eventService.Create(label);

                    Context.Items.Add("createdGroup", labelCreated);

                    LogManager.RecordMessage("Label " + label.name + " created.", MessageType.Info);

                    Server.Transfer(Response.ApplyAppPathModifier("~/Pages/EventPages/Home.aspx"));

                }
                catch (DuplicateInstanceException)
                {
                    Server.Transfer(Response.ApplyAppPathModifier("~/Pages/Errors/InternalError.aspx"));
                }
            }
        }
    }
}