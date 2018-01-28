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
    public partial class LabelComment : System.Web.UI.Page
    {
        IEventService eventService;
        long commentId;
        protected void Page_Load(object sender, EventArgs e)
        {
            callService();
            initFromValues();

            if (!Page.IsPostBack)
            {
                if (Request.Params.Get("action").Equals("Add"))
                {
                    this.checkboxLabels.DataSource = eventService.GetLabelsDtos();
                }
                else
                {
                    this.checkboxLabels.DataSource = eventService.GetCommentLabels(Convert.ToInt32(Request.Params.Get("commentId")));
                }
                this.checkboxLabels.DataTextField = "name";
                this.checkboxLabels.DataValueField = "labelId";
                this.checkboxLabels.DataBind();
            }
        }

        protected void callService()
        {
            /* Get the Service */
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            eventService = iocManager.Resolve<IEventService>();
        }

        protected void initFromValues()
        {
            commentId = Convert.ToInt32(Request.Params.Get("commentId"));
        }

        protected void btnAddClick(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                
                ICollection<Model.Label> labels = new List<Model.Label>();
                foreach (ListItem item in checkboxLabels.Items)
                {
                    if (item.Selected)
                    {
                        Model.Label label = eventService.GetAllLabels().Where(x => x.labelId == Convert.ToInt32(item.Value)).Single();
                        labels.Add(label);
                    }
                }
                if (Request.Params.Get("action").Equals("Add"))
                {
                    eventService.AddLabel(commentId, labels);
                }
                else
                {
                    eventService.RemoveLabel(commentId, labels);
                }

                /* Do action. */
                String url = String.Format("~/Pages/EventPages/Home.aspx");
                Response.Redirect(Response.ApplyAppPathModifier(url));
            }
        }
    }
}