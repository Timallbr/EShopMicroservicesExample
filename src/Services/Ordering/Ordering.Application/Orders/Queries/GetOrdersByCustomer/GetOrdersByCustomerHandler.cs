﻿using Microsoft.EntityFrameworkCore;
using Ordering.Application.Extensions;
using Ordering.Application.Orders.Queries.GetOrderByName;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Orders.Queries.GetOrdersByCustomer
{
    public class GetOrdersByCustomerHandler
        (IApplicationDbContext dbContext)
        : IQueryHandler<GetOrdersByCustomerQuery, GetOrdersByCustomerResult>
    {
        public async Task<GetOrdersByCustomerResult> Handle(GetOrdersByCustomerQuery query, CancellationToken cancellationToken)
        {
            var orders = await dbContext.Orders
                            .Include(o => o.OrderItems)
                            .AsNoTracking()
                            .Where(o => o.CustomerId == CustomerId.Of(query.CustomerId))
                            .OrderBy(o => o.OrderName.Value)
                            .ToListAsync(cancellationToken);

            var orderDtos = orders.ToOrderDtoList();

            return new GetOrdersByCustomerResult(orderDtos);
        }
    }
}
