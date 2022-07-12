using CinemaTicketing.Domain.DomainModels;
using CinemaTicketing.Repository.Interface;
using CinemaTicketing.Services.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaTicketing.Services.Implementation
{
    public class OrderService : IOrderService
    {
        public readonly IOrderRepository _orderRepository;
        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public List<Order> GetAllOrders()
        {
            return this._orderRepository.GetAllOrders();
        }

        public Order GetOrderDetails(BaseEntity model)
        {
            return this._orderRepository.GetOrderDetails(model);
        }
    }
}
