using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.MiniPortal.Model.UserService
{
    public class UserGroupDto
    {
        public long groupId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public ICollection<Recommendation> recommendations { get; set; }
        public ICollection<UserProfile> users { get; set; }
        public int usersCount { get; set; }
        public int recommendationsCount { get; set; }

        public UserGroupDto(UserGroup userGroup)
        {
            this.groupId = userGroup.groupId;
            this.name = userGroup.name;
            this.description = userGroup.description;
            this.recommendations = userGroup.Recommendations;
            this.users = userGroup.UserProfiles;
            this.usersCount = users.Count;
            this.recommendationsCount = recommendations.Count;
        }
    }

}
