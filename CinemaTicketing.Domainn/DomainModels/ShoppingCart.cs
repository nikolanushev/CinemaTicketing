
using CinemaTicketing.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaTicketing.Domain.DomainModels
{
    public class ShoppingCart : BaseEntity
    {
        public string CinemaUserId { get; set; }
        public CinemaApplicationUser CinemaUser { get; set; }
        public virtual ICollection<TicketInShoppingCart> TicketInShoppingCarts { get; set; }
    }
}
