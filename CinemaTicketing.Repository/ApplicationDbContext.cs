using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using Microsoft.EntityFrameworkCore;
using CinemaTicketing.Domain.Identity;
using CinemaTicketing.Domain.DomainModels;

namespace CinemaTicketing.Repository
{
    public class ApplicationDbContext : IdentityDbContext<CinemaApplicationUser>
    {

        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public virtual DbSet<TicketInShoppingCart> TicketInShoppingCarts { get; set; }
        public virtual DbSet<MovieTicket> MovieTickets { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Movie>()
                .Property(z => z.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<Ticket>()
                .Property(z => z.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<ShoppingCart>()
               .Property(z => z.Id)
               .ValueGeneratedOnAdd();

            builder.Entity<TicketInShoppingCart>()
                .HasKey(z => new { z.MovieTicketId, z.ShoppingCartId });

            builder.Entity<TicketInShoppingCart>()
                .HasOne(z => z.MovieTIcket)
                .WithMany(z => z.TicketInShoppingCarts)
                .HasForeignKey(z => z.ShoppingCartId);

            builder.Entity<TicketInShoppingCart>()
                .HasOne(z => z.ShoppingCart)
                .WithMany(z => z.TicketInShoppingCarts)
                .HasForeignKey(z => z.MovieTicketId);

            builder.Entity<ShoppingCart>()
                .HasOne<CinemaApplicationUser>(z => z.CinemaUser)
                .WithOne(z => z.UserShoppingCart)
                .HasForeignKey<ShoppingCart>(z => z.CinemaUserId);

            builder.Entity<MovieTicketInOrder>()
                .HasKey(z => new { z.MovieTicketId, z.OrderId });

            builder.Entity<MovieTicketInOrder>()
                .HasOne(z => z.SelectedMovieTicket)
                .WithMany(z => z.Orders)
                .HasForeignKey(z => z.MovieTicketId);

            builder.Entity<MovieTicketInOrder>()
                .HasOne(z => z.UserOrder)
                .WithMany(z => z.MovieTickets)
                .HasForeignKey(z => z.OrderId);
        }
    }
}
