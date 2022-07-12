using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaTicketing.Domain.DomainModels
{
    public class MovieTicket : BaseEntity
    {
        [ForeignKey("MovieId")]
        public Guid MovieId { get; set; }
        public Movie Movie { get; set; }

        [ForeignKey("TicketId")]
        public Guid TicketId { get; set; }
        public Ticket Ticket { get; set; }

        public virtual ICollection<TicketInShoppingCart> TicketInShoppingCarts { get; set; }
        public virtual ICollection<MovieTicketInOrder> Orders { get; set; }

    }
}
