using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.MiniPortal.Model.EventService
{
    public class EventDto
    {
        public long eventId { get; set; }
        public string name { get; set; }
        public long categoryId { get; set; }
        public string categoryName { get; set; }

        public EventDto(Event evento)
        {
            this.eventId = evento.eventId;
            this.name = evento.name;
            this.categoryId = evento.Category.categoryId;
            this.categoryName = evento.Category.name;
        }
    }
}
