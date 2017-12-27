using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.MiniPortal.Model.EventService
{
    public class EventDto
    {
        public string name { get; set; }
        public string categoryName { get; set; }

        public EventDto(Event evento)
        {
            this.name = evento.name;
            this.categoryName = evento.Category.name;
        }
    }
}
