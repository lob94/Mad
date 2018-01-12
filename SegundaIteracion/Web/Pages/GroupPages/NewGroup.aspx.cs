using Es.Udc.DotNet.MiniPortal.Model.UserService;
using Es.Udc.DotNet.ModelUtil.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.MiniPortal.Web.Pages.GroupPages
{
    public partial class NewGroup : System.Web.UI.Page
    {
        IUserService userService;
        protected void Page_Load(object sender, EventArgs e)
        {
            IIoCManager container = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            userService = container.Resolve<IUserService>();
        }

        protected void BtnNewGroupClick(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                /* Get data. */

                String name = txtNewGroupName.Text;
                String description = TxtNewGroupDescription.Text;

                //userService.AddGroup(name, description, );

            }
        }
    }
}