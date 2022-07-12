
using CinemaTicketing.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaTicketing.Domain.DTO
{
    public class ShoppingCartDto
    {
        public List<TicketInShoppingCart> TicketsInShoppingCart { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
