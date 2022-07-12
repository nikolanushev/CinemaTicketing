using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaTicketing.Domain.DomainModels
{
    public class MovieTicketInOrder : BaseEntity
    {
        public Guid MovieTicketId { get; set; }
        public MovieTicket SelectedMovieTicket { get; set; }
        public Guid OrderId { get; set; }
        public Order UserOrder { get; set; }
        public int Quantity { get; set; }
    }
}
