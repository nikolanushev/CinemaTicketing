using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaTicketing.Domain.DomainModels
{
    public class Ticket : BaseEntity
    {
        [Required]
        [Display(Name = "Ticket Number")]
        public string TicketNumber { get; set; }

        [Required]
        [Display(Name = "Seat Number")]
        public int SeatNumber { get; set; }

        [Required]
        public string Row { get; set; }

        [Required]
        public string Hall { get; set; }

        [Display(Name = "Date and Time")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dddd, MMMM d, yyyy}")]
        public DateTime DateAndTime { get; set; }
    }
}
