using Es.Udc.DotNet.MiniPortal.Model;
using Es.Udc.DotNet.MiniPortal.Model.EventService;
using Es.Udc.DotNet.MiniPortal.Model.UserService;
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
    public partial class RecomendationsPage : System.Web.UI.Page
    {
        int startIndex = 0;
        int count = 1;
        ICollection<Recommendation> recommendations;
        ICollection<UserGroupDto> groupList;
        IUserService userService;
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
            userService = container.Resolve<IUserService>();
            eventService = container.Resolve<IEventService>();

        }

        protected void initFromValues()
        {
            String startString = Request.Params.Get("startIndex");
            if (startString == null || startString == "0")
            {
                startIndex = 0;
            }
            else
            {
                startIndex = Convert.ToInt16(startString);
            }

        }

        protected void initGridView()
        {
            UserProfileDetails userProfileDetails =
                   SessionManager.FindUserProfileDetails(Context);
            String email = userProfileDetails.Email;
            UserProfile u = userService.FindUserByEmail(email);

            groupList = userService.FindAllGroups();
            foreach (UserGroupDto gr in groupList)
            {
              //  recommendations = userService.FindGroupRecommendations(gr.id,u.usrId,startIndex,count);
            }
            ///PROBLEMA!!! NECESARIO OBTENER EL ID DEL GRUPO, PERO TENEMOS UN GROUPDTO EN VEZ DE UN GRUPO
            ///Por ello necesitamos el grupo y su id
            ///Ademas vamos a tener que mostrar la lista de recomendaciones de cada grupo 
            ///RECORRIENDO UNO POR UNO LOS GRUPOS Y MOSTRANDO DE CADA UNO SUS RECOMENDACIONES
            ///CADA RECOMENDACION LLEVARIA ASIGNADA UN EVENTO Y UNA DESCRIPCION.
        }
    }
}