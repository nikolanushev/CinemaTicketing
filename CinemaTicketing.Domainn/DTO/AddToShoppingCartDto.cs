
using CinemaTicketing.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaTicketing.Domain.DTO
{
    public class AddToShoppingCartDto
    {
        public MovieTicket SelectedMovieTicket { get; set; }
        public Guid MovieTicketId { get; set; }
        public int Quantity { get; set; }
    }
}
