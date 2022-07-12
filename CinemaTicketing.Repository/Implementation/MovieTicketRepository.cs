using CinemaTicketing.Domain.DomainModels;
using CinemaTicketing.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CinemaTicketing.Repository.Implementation
{
    public class MovieTicketRepository : IMovieTicketRepository
    {

        private readonly ApplicationDbContext context;
        private DbSet<MovieTicket> entities;
        string errorMessage = string.Empty;

        public MovieTicketRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<MovieTicket>();
        }

        public void Delete(MovieTicket entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();
        }

        public MovieTicket Get(Guid? id)
        {
            return entities
                .Where(z => z.Id == id)
                .Include(z => z.Movie)
                .Include(z => z.Ticket)
                .SingleOrDefault(s => s.Id.Equals(id));
        }

        public IEnumerable<MovieTicket> GetAll()
        {
            return entities
                .Include(z => z.Movie)
                .Include(z => z.Ticket)
                .AsEnumerable();
        }

        public void Insert(MovieTicket entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(MovieTicket entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
            context.SaveChanges();
        }
    }
}
