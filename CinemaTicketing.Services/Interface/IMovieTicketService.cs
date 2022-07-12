using CinemaTicketing.Domain.DomainModels;
using CinemaTicketing.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaTicketing.Services.Interface
{
    public interface IMovieTicketService
    {
        List<MovieTicket> GetAllMovieTickets();
        MovieTicket GetDetailsForMovieTicket(Guid? id);
        void CreateNewMovieTicket(MovieTicket m);
        void UpdateExistingMovieTicket(MovieTicket m);
        AddToShoppingCartDto GetShoppingCartInfo(Guid? id); 
        void DeleteMovieTicket(Guid id);
        bool AddToShoppingCart(AddToShoppingCartDto item, string userID);
    }
}
