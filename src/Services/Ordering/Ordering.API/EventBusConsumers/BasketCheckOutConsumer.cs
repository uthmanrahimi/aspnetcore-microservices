using AutoMapper;

using EventBus.Messages.Events;

using MassTransit;

using MediatR;

using Microsoft.Extensions.Logging;

using Ordering.Application.Features.Orders.Commands.CheckOutOrder;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.API.EventBusConsumers
{
    public class BasketCheckOutConsumer : IConsumer<BasketCheckoutEvent>
    {
        private readonly IMapper _mapper;
        private readonly ISender _sender;
        private readonly ILogger<BasketCheckOutConsumer> _logger;

        public BasketCheckOutConsumer(IMapper mapper, ISender sender, ILogger<BasketCheckOutConsumer> logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _sender = sender ?? throw new ArgumentNullException(nameof(sender));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
        {
            var command = _mapper.Map<CheckOutOrderCommand>(context.Message);
            var result = await _sender.Send(command);
            _logger.LogInformation($"BasketCheckOutEvent consumed successfully. Created Order is : {result}");
        }
    }
}
