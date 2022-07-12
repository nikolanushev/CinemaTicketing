using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using CinemaTicketing.Repository;
using CinemaTicketing.Domain.DTO;
using CinemaTicketing.Domain.DomainModels;
using CinemaTicketing.Services.Interface;

namespace CinemaTicketing.Web.Controllers
{
    public class MovieTicketsController : Controller
    {
        private readonly IMovieTicketService _movieTicketService;
        private readonly IMovieService _movieService;
        private readonly ITicketService _ticketService;

        public MovieTicketsController(IMovieTicketService movieTicketService, IMovieService movieService, ITicketService ticketService)
        {
            _movieTicketService = movieTicketService;
            _movieService = movieService;
            _ticketService = ticketService;
        }

        // GET: MovieTickets
        public IActionResult Index()
        {
            var allMovieTickets = this._movieTicketService.GetAllMovieTickets();
            return View(allMovieTickets);
        }

        public IActionResult AddMovieTicketToCart(Guid? id)
        {
            var model = this._movieTicketService.GetShoppingCartInfo(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddMovieTicketToCart([Bind("MovieTicketId, Quantity")] AddToShoppingCartDto item)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = this._movieTicketService.AddToShoppingCart(item, userId);

            if (result)
            {
                return RedirectToAction("Index", "MovieTickets");
            }
            return View(item);
        }

        // GET: MovieTickets/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieTicket = this._movieTicketService.GetDetailsForMovieTicket(id);

            if (movieTicket == null)
            {
                return NotFound();
            }

            return View(movieTicket);
        }

        // GET: MovieTickets/Create
        public IActionResult Create()
        {
            List<Movie> moviesList = this._movieService.GetAllMovies();
            List<Ticket> ticketsList = this._ticketService.GetAllTickets();

            ViewData["MovieId"] = new SelectList(moviesList, "Id", "MovieName");
            ViewData["TicketId"] = new SelectList(ticketsList, "Id", "TicketNumber");
            return View();
        }

        // POST: MovieTickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MovieTicket movieTicket)
        {
            Movie movie = _movieService.GetDetailsForMovie(movieTicket.MovieId);
            Ticket ticket = _ticketService.GetDetailsForTicket(movieTicket.MovieId);

            if (ModelState.IsValid)
            {
                MovieTicket newMovieTicket = movieTicket;
                newMovieTicket.Movie = movie;
                newMovieTicket.Ticket = ticket;

                this._movieTicketService.CreateNewMovieTicket(newMovieTicket);
                return RedirectToAction(nameof(Index));
            }
            return View(movieTicket);
        }

        // GET: MovieTickets/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieTicket = this._movieTicketService.GetDetailsForMovieTicket(id);
            if (movieTicket == null)
            {
                return NotFound();
            }
            return View(movieTicket);
        }

        // POST: MovieTickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("MovieId,TicketId,Id")] MovieTicket movieTicket)
        {
            if (id != movieTicket.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    this._movieTicketService.UpdateExistingMovieTicket(movieTicket);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieTicketExists(movieTicket.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movieTicket);
        }

        // GET: MovieTickets/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieTicket = this._movieTicketService.GetDetailsForMovieTicket(id);
            if (movieTicket == null)
            {
                return NotFound();
            }

            return View(movieTicket);
        }

        // POST: MovieTickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            this._movieTicketService.DeleteMovieTicket(id);
            return RedirectToAction(nameof(Index));
        }

        private bool MovieTicketExists(Guid id)
        {
            return this._movieTicketService.GetDetailsForMovieTicket(id) != null;
        }
    }
}
