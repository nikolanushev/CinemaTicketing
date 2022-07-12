using CinemaTicketing.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaTicketing.Services.Interface
{
    public interface IShoppingCartService
    {
        ShoppingCartDto getShopingCartInfo(string userId);
        bool deleteMovieTicketFromShoppingCart(string userId, Guid movieTicketId);
        bool orderNow(string userId);
    }
}
