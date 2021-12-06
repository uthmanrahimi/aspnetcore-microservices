using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Messages.Events
{
    public class BaseEvent
    {
        public Guid Id { get; init; }
        public DateTime CreationDate { get; set; }
    }
}
