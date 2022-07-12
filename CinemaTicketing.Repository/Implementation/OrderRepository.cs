using CinemaTicketing.Domain.DomainModels;
using CinemaTicketing.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CinemaTicketing.Repository.Implementation
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        private DbSet<Order> entities;
        string errorMessage = string.Empty;


        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
            entities = context.Set<Order>();
        }

        public List<Order> GetAllOrders()
        {
            return entities
                .Include(z => z.MovieTickets)
                .Include(z => z.User)
                .Include("MovieTickets.SelectedMovieTicket")
                .ToListAsync().Result;
        }

        public Order GetOrderDetails(BaseEntity model)
        {
            return entities
                .Where(z => z.Id == model.Id)
                .Include(z => z.MovieTickets)
                .Include(z => z.User)
                .Include("MovieTickets.SelectedMovieTicket")
                .Include("MovieTickets.SelectedMovieTicket.Movie")
                .Include("MovieTickets.SelectedMovieTicket.Ticket")
                .SingleOrDefault();

        }
    }
}
