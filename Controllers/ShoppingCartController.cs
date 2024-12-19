using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using cafeMvc.Data;
using cafeMvc.Models;
using cafeMvc.ViewModels;
using NuGet.Protocol;
using Microsoft.AspNetCore.Http;
using cafeMvc.Extensions;

namespace cafeMvc.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly CafeMvcContext _context;
        public ShoppingCartController(CafeMvcContext context)
        {
             _context = context;
        }

        public IActionResult AddToCart(int dishId)
        {
            // Fetch the dish from the database
            var dish = _context.Dishes.FirstOrDefault(d => d.Id == dishId);

            if(dish == null){
                return NotFound();
            }

           // Retrieve or initialize the cart
           var cart = HttpContext.Session.GetObjectFromJson<Cart>("Cart") ?? new Cart();

           cart.AddItem(new CartItemViewModel{
                        DishId = dish.Id,
                        DishName = dish.Name,
                        Price = dish.Price,
                        Quantity = 1
                    });
        
            //save updated cart to session
            HttpContext.Session.SetObjectAsJson("Cart", cart);
            return RedirectToAction("Index"); // Redirect to menu or cart view

        }

        public IActionResult ViewCart ()
        {
            var cart = HttpContext.Session.GetObjectFromJson<Cart>("Cart") ?? new Cart();
            return View(cart);
        }

        public IActionResult Checkout ()
        {
             var cart = HttpContext.Session.GetObjectFromJson<List<CartItemViewModel>>("Cart");

             if (cart == null|| !cart.Items.Any())
             {
                return BadRequest("Cart is Empty!");
             }

             var order = new Order
             {
                OrderDate = DateTime.Now,
                CustomerId = 1, /* fetch current customer ID */,
                OrderDetails = cartItems.Select(item => new OrderDetail
                {
                    DishId = item.DishId,
                    Quantity = item.Quantity
                }).ToList()
             };


             _context.Orders.Add(order);
             _context.SaveChanges();

              // Clear the session cart
              HttpContext.Session.Remove("Cart");

              return RedirectToAction("OrderConformation", new {orderId = order.Id});


        }




    }
}