using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.MiniPortal.Model.UserService
{
    public class UserGroupDto
    {
        public string name { get; set; }
        public ICollection<Recommendation> recommendations { get; set; }
        public ICollection<UserProfile> users { get; set; }

        public UserGroupDto(UserGroup userGroup)
        {
            this.name = userGroup.name;
            this.recommendations = userGroup.Recommendations;
            this.users = userGroup.UserProfiles;
        }
    }

}
