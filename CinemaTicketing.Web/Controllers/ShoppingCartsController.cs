using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using CinemaTicketing.Domain.Identity;
using CinemaTicketing.Repository;
using CinemaTicketing.Domain.DTO;
using CinemaTicketing.Domain.DomainModels;
using CinemaTicketing.Services.Interface;
using Stripe;

namespace CinemaTicketing.Web.Controllers
{
    public class ShoppingCartsController : Controller
    {
        private readonly IShoppingCartService _shoppingCartService;
        public ShoppingCartsController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        // GET: ShoppingCarts
        public IActionResult Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View(this._shoppingCartService.getShopingCartInfo(userId));

        }

        public IActionResult PayOrder(string stripeEmail, string stripeToken)
        {
            var customerService = new CustomerService();
            var chargeService = new ChargeService();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var order = this._shoppingCartService.getShopingCartInfo(userId);

            var customer = customerService.Create(new CustomerCreateOptions
            {
                Email = stripeEmail,
                Source = stripeToken
            });

            var charge = chargeService.Create(new ChargeCreateOptions
            {
                Amount = ((long?)(order.TotalPrice) * 100),
                Description = "Cinema Ticketing Application Payment",
                Currency = "MKD",
                Customer = customer.Id
            });

            if (charge.Status == "succeeded")
            {
                var result = this.OrderNow();

                if (result)
                {
                    return RedirectToAction("Index", "ShoppingCarts");
                }
                else
                {
                    return RedirectToAction("Index", "ShoppingCarts");
                }
            }
            return RedirectToAction("Index", "ShoppingCarts");
        }

        public IActionResult DeleteFromShoppingCart (Guid id)
        {

            var result = this._shoppingCartService.deleteMovieTicketFromShoppingCart(User.FindFirstValue(ClaimTypes.NameIdentifier), id);

            if (result)
            {
                return RedirectToAction("Index", "ShoppingCarts");
            }
            else
            {
                return RedirectToAction("Index", "ShoppingCarts");
            }
        }

        private bool OrderNow()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = this._shoppingCartService.orderNow(userId);
            return result;
        }
    }
}
