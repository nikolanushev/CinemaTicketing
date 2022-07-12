using CinemaTicketing.Domain.DomainModels;
using CinemaTicketing.Domain.DTO;
using CinemaTicketing.Repository.Interface;
using CinemaTicketing.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CinemaTicketing.Services.Implementation
{
    public class ShoppingCartService : IShoppingCartService
    {
        public readonly IUserRepository _userRepository;
        public readonly IRepository<MovieTicketInOrder> _movieTicketInOrderRepository;
        public readonly IRepository<ShoppingCart> _shoppingCartRepository;
        public readonly IRepository<Order> _orderRepository;
        public ShoppingCartService(IUserRepository userRepository,
                IRepository<MovieTicketInOrder> movieTicketInOrderRepository,
                IRepository<ShoppingCart> shoppingCartRepository,
                IRepository<Order> orderRepository)
        {
            _userRepository = userRepository;
            _movieTicketInOrderRepository = movieTicketInOrderRepository;
            _shoppingCartRepository = shoppingCartRepository;
            _orderRepository = orderRepository;
        }

        public bool deleteMovieTicketFromShoppingCart(string userId, Guid movieTicketId)
        {
            if (!string.IsNullOrEmpty(userId) && movieTicketId != null)
            {
                //Select * from Users Where Id LIKE userId

                var loggedInUser = this._userRepository.Get(userId);

                var userShoppingCart = loggedInUser.UserShoppingCart;

                var itemToDelete = userShoppingCart.TicketInShoppingCarts.Where(z => z.MovieTicketId.Equals(movieTicketId)).FirstOrDefault();

                userShoppingCart.TicketInShoppingCarts.Remove(itemToDelete);

                this._shoppingCartRepository.Update(userShoppingCart);

                return true;
            }

            return false;
        }

        public ShoppingCartDto getShopingCartInfo(string userId)
        {
            var user = _userRepository.Get(userId);
            var userShoppingCart = user.UserShoppingCart;

            var movieTicketList = userShoppingCart.TicketInShoppingCarts.Select(z => new
            {
                Quantity = z.Quantity,
                MovieTicketPrice = z.MovieTIcket.Movie.MoviePrice
            });
            decimal totalPrice = 0;
            foreach (var item in movieTicketList)
            {
                totalPrice += item.MovieTicketPrice * item.Quantity;
            }

            ShoppingCartDto model = new ShoppingCartDto
            {
                TotalPrice = totalPrice,
                TicketsInShoppingCart = userShoppingCart.TicketInShoppingCarts.ToList()
            };

            return model;
        }

        public bool orderNow(string userId)
        {
            var user = _userRepository.Get(userId);
            var userShoppingCart = user.UserShoppingCart;

            Order newOrder = new Order
            {
                UserId = user.Id,
                User = user
            };
            _orderRepository.Insert(newOrder);

            List<MovieTicketInOrder> movieTicketsInOrder = userShoppingCart.TicketInShoppingCarts.Select(z => new MovieTicketInOrder
            {
                SelectedMovieTicket = z.MovieTIcket,
                MovieTicketId = z.MovieTicketId,
                UserOrder = newOrder,
                OrderId = newOrder.Id,
                Quantity = z.Quantity
            }).ToList();

            foreach (var item in movieTicketsInOrder)
            {
                _movieTicketInOrderRepository.Insert(item);
            }
            user.UserShoppingCart.TicketInShoppingCarts.Clear();
            _userRepository.Update(user);

            return true;
        }
    }
}
