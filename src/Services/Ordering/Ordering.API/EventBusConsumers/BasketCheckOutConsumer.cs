using EventBus.Messages.Events;

using MassTransit;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.API.EventBusConsumers
{
    public class BasketCheckOutConsumer : IConsumer<BasketCheckoutEvent>
    {
        public Task Consume(ConsumeContext<BasketCheckoutEvent> context)
        {
            throw new NotImplementedException();
        }
    }
}
