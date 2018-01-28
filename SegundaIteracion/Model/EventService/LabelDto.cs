using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.MiniPortal.Model.EventService
{
    public class LabelDto
    {
        public long labelId { get; set; }

        public string name { get; set; }

        public int numComments { get; set; }

        public ICollection<CommentDto> comments { get; set; }

        public LabelDto(long labelId, string name, int numComments, ICollection<CommentDto> comments)
        {
            this.labelId = labelId;
            this.name = name;
            this.numComments = numComments;
            this.comments = comments;
        }
    }
}
