using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaTicketing.Domain.DomainModels
{
    public class TicketInShoppingCart : BaseEntity
    {
        public Guid MovieTicketId { get; set; }
        public MovieTicket MovieTIcket { get; set; }

        public Guid ShoppingCartId { get; set; }
        public ShoppingCart ShoppingCart { get; set; }

        public int Quantity { get; set; }

    }
}
