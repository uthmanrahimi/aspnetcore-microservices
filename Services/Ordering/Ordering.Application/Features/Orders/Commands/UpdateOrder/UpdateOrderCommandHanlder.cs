using AutoMapper;

using MediatR;

using Microsoft.Extensions.Logging;

using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Exceptions;
using Ordering.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandHanlder : IRequestHandler<UpdateOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ILogger<UpdateOrderCommandHanlder> _logger;
        public UpdateOrderCommandHanlder(IOrderRepository orderRepository, IMapper mapper, IEmailService emailService, ILogger<UpdateOrderCommandHanlder> logger)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _emailService = emailService;
            _logger = logger;
        }


        public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.Id);

            if (order == null)
            {
                throw new NotFoundException(nameof(Order), request.Id);
            }

            _mapper.Map(request, order, typeof(UpdateOrderCommand), typeof(Order));
            await _orderRepository.UpdateAsync(order);
            _logger.LogInformation($"Order {order.Id} was updated successfully.");

            return Unit.Value;
        }
    }
}
