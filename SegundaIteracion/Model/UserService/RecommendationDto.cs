using Es.Udc.DotNet.MiniPortal.Model.EventDao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.MiniPortal.Model.UserService
{
    public class RecommendationDto
    {
        public long eventId { get; set; }
        public String eventName { get; set; }
        public long recommendationId { get; set; }
        public long groupId { get; set; }
        public string reason { get; set; }
        


        public RecommendationDto(Recommendation r, String name)
        {
            this.recommendationId = r.recommendationId;
            this.eventName = name;
            this.eventId = r.eventId;
            this.reason = r.reason;
            this.groupId = r.groupId;
        }
    }
}
