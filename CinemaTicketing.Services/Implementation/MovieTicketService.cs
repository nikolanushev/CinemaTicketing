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
    public class MovieTicketService : IMovieTicketService
    {
        private readonly IMovieTicketRepository _movieTicketRepository;
        private readonly IRepository<TicketInShoppingCart> _ticketInShoppingCartRepository;
        private readonly IUserRepository _userRepository;

        public MovieTicketService(IMovieTicketRepository movieTicketRepository, IUserRepository userRepository, IRepository<TicketInShoppingCart> ticketInShoppingCartRepository)
        {
            _movieTicketRepository = movieTicketRepository;
            _userRepository = userRepository;
            _ticketInShoppingCartRepository = ticketInShoppingCartRepository;
        }

        public bool AddToShoppingCart(AddToShoppingCartDto item, string userID)
        {
            var user = this._userRepository.Get(userID);
            var userShoppingCart = user.UserShoppingCart;

            if(item.MovieTicketId != null && userShoppingCart != null)
            {
                var movieTicket = this.GetDetailsForMovieTicket(item.MovieTicketId);
                if(movieTicket != null)
                {
                    TicketInShoppingCart itemToAdd = new TicketInShoppingCart
                    {
                        MovieTIcket = movieTicket,
                        MovieTicketId = movieTicket.Id,
                        ShoppingCart = userShoppingCart,
                        ShoppingCartId = userShoppingCart.Id,
                        Quantity = item.Quantity
                    };
                    this._ticketInShoppingCartRepository.Insert(itemToAdd);
                    return true;
                }
                return false;
            }
            return false;
        }

        public void CreateNewMovieTicket(MovieTicket m)
        {
            this._movieTicketRepository.Insert(m);
        }

        public void DeleteMovieTicket(Guid id)
        {
            var movie = this.GetDetailsForMovieTicket(id);
            this._movieTicketRepository.Delete(movie);
        }

        public List<MovieTicket> GetAllMovieTickets()
        {
            return this._movieTicketRepository.GetAll().ToList();
        }

        public MovieTicket GetDetailsForMovieTicket(Guid? id)
        {
            return this._movieTicketRepository.Get(id); 
        }

        public AddToShoppingCartDto GetShoppingCartInfo(Guid? id)
        {
            var movieTicket = this.GetDetailsForMovieTicket(id);
            AddToShoppingCartDto model = new AddToShoppingCartDto
            {
                SelectedMovieTicket = movieTicket,
                MovieTicketId = movieTicket.Id,
                Quantity = 1
            };
            return model;
        }

        public void UpdateExistingMovieTicket(MovieTicket m)
        {
            this._movieTicketRepository.Update(m);
        }
    }
}
