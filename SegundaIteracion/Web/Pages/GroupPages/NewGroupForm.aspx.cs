﻿using Es.Udc.DotNet.MiniPortal.Model.UserService;
using Es.Udc.DotNet.MiniPortal.Web.HTTP.Session;
using Es.Udc.DotNet.ModelUtil.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.MiniPortal.Web.Pages.GroupPages
{
    public partial class NewGroupForm : System.Web.UI.Page
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
                long usrId = userService.FindUserByEmail(SessionManager.FindUserProfileDetails(Context).Email).usrId;
                userService.AddGroup(name, description, usrId);
                Response.Redirect("Groups.aspx");

            }
        }
    }
}