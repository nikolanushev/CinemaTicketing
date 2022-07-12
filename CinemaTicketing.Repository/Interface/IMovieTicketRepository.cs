using CinemaTicketing.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaTicketing.Repository.Interface
{
    public interface IMovieTicketRepository 
    {
        IEnumerable<MovieTicket> GetAll();
        MovieTicket Get(Guid? id);
        void Insert(MovieTicket entity);
        void Update(MovieTicket entity);
        void Delete(MovieTicket entity);
    }
}
