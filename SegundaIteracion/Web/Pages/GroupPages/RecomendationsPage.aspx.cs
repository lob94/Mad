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
    public partial class RecomendationsPage : System.Web.UI.Page
    {
        IUserService userService;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void callService()
        {
            IIoCManager container = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            userService = container.Resolve<IUserService>();

        }

        protected void initFromValues()
        {
            
        }
    }
}