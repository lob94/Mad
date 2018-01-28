using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.MiniPortal.Model.EventService
{
    public class CommentDto
    {
        public long commentId { get; set; }
        public long userId { get; set; }
        public long eventId { get; set; }
        public string loginName { get; set; }
        public string content { get; set; }
        public DateTime commentDate { get; set; }
        public ICollection<Label> labels { get; set; }

        public CommentDto(Comment comment, long eventId)
        {
            this.commentId = comment.commentId;
            this.userId = comment.UserProfile.usrId;
            this.eventId = eventId;
            this.loginName = comment.UserProfile.loginName;
            this.content = comment.content;
            this.commentDate = comment.commentDate;
            this.labels = comment.Labels;
        }
    }
}
