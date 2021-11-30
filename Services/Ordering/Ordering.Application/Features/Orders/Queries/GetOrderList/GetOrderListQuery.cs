using MediatR;

using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Application.Features.Orders.Queries.GetOrderList
{
    public class GetOrderListQuery : IRequest<IEnumerable<OrderDto>>
    {
        public string UserName { get; set; }
        public GetOrderListQuery(string userName)
        {
            UserName = userName ?? throw new NullReferenceException(nameof(userName));
        }
    }
}
