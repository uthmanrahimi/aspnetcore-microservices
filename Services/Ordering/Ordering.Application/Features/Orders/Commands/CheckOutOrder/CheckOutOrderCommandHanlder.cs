using AutoMapper;

using MediatR;

using Microsoft.Extensions.Logging;

using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Models;
using Ordering.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.CheckOutOrder
{
    public class CheckOutOrderCommandHanlder : IRequestHandler<CheckOutOrderCommand, int>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ILogger<CheckOutOrderCommandHanlder> _logger;

        public CheckOutOrderCommandHanlder(IOrderRepository orderRepository, IMapper mapper, IEmailService emailService, ILogger<CheckOutOrderCommandHanlder> logger)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _emailService = emailService;
            _logger = logger;
        }
        public async Task<int> Handle(CheckOutOrderCommand request, CancellationToken cancellationToken)
        {
            var order = _mapper.Map<Order>(request);
            await _orderRepository.AddAsync(order);
            _logger.LogInformation($"Order {order.Id} has been created successfully.");
            await SendMail(order);
            return order.Id;

        }

        private async Task SendMail(Order order)
        {
            var email = new Email { To = order.EmailAddress, Subject = "Purchase", Body = "Order Was Created" };
            try
            {
                await _emailService.SendEmail(email);

            }
            catch (Exception)
            {

                _logger.LogError($"An error occured while sending email.orderid:{order.Id}");
            }

        }
    }
}
