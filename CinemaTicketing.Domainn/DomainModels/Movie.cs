using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaTicketing.Domain.DomainModels
{
    public class Movie : BaseEntity
    {
        [Required]
        [Display(Name = "Movie Name")]
        public string MovieName { get; set; }

        [Display(Name = "Movie Genre")]
        public string Genre { get; set; }

        [Display(Name = "Movie Image")]
        public string MovieImage { get; set; }

        [Display(Name = "Movie Description")]
        public string MovieDesrciption { get; set; }

        [Display(Name = "Movie Price")]
        public decimal MoviePrice { get; set; }

        public int Rating { get; set; }

        public List<MovieTicket> MovieTickets { get; set; }
    }
}
