using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using cafeMvc.Data;
using cafeMvc.Models;
using cafeMvc.ViewModels;

namespace cafeMvc.Controllers
{
    public class DishController : Controller
    {
        private readonly CafeMvcContext _context;
        private readonly ILogger<DishController> _logger;

        public DishController(CafeMvcContext context, ILogger<DishController> logger)
        {
            _context = context;
            _logger = logger;
        }


        // GET: Dish
        public async Task<IActionResult> Index() //string searchString
        {
             _logger.LogInformation("DishController Index action accessed.");
                    var dishes = await _context.Dishes.Include(d => d.DishIngredients).ToListAsync();

                 _logger.LogDebug("Fetched {Count} dishes from the database.", dishes.Count);
                    return View(dishes);

            //older code with search string  var dishes = from d in _context.Dishes 
            // select d;
     
            // if(!string.IsNullOrEmpty(searchString))
            // {
            //     dishes = dishes.Where(d => d.Name.Contains(searchString));
            //     return View(await dishes.ToListAsync());
            // }
            // return View(await _context.Dishes.ToListAsync());
        }

        // GET: Dish/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dish = await _context.Dishes
                .Include(d => d.DishIngredients)
                .ThenInclude(di => di.Ingredient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dish == null)
            {
                return NotFound();
            }

            return View(dish);
        }

       // GET: Dish/Create
       public async Task<IActionResult> Create () 
       {
       var viewModel = new DishCreateViewModel
        {
            AvailableIngredients = await _context.Ingredients
                .Select(i => new SelectListItem
                {
                    Value = i.Id.ToString(),
                    Text = i.Name
                })
                .ToListAsync()
        };
        return View(viewModel);
       }
        

        // POST: Dish/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create (DishCreateViewModel viewModel)
        {
                if (!ModelState.IsValid)
                {
                Console.WriteLine("ModelState is invalid:");
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($" error - {error.ErrorMessage}");
                }
                }


            if(ModelState.IsValid)
            {
                // Create the new dish
            var dish = new Dish
            {
                Name = viewModel.Name,
                ImageUrl = viewModel.ImageUrl,
                Price = viewModel.Price
            };

            // Add selected ingredients
            foreach (var ingredientId in viewModel.SelectedIngredientIds)
            {
                dish.DishIngredients.Add(new DishIngredient
                {
                    IngredientId = ingredientId
                });
            }

                _context.Dishes.Add(dish);
                await _context.SaveChangesAsync();
                 return RedirectToAction(nameof(Index));
            }

    // Reload available ingredients on validation failure
    viewModel.AvailableIngredients = await _context.Ingredients
        .Select(i => new SelectListItem
        {
            Value = i.Id.ToString(),
            Text = i.Name
        }).ToListAsync();

    return View(viewModel);
}



        // GET: Dish/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dish = await _context.Dishes.FindAsync(id);
            if (dish == null)
            {
                return NotFound();
            }
            return View(dish);
        }

        // POST: Dish/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ImageUrl,Price")] Dish dish)
        {
            if (id != dish.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dish);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DishExists(dish.Id))
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
            return View(dish);
        }

        // GET: Dish/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dish = await _context.Dishes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dish == null)
            {
                return NotFound();
            }

            return View(dish);
        }

        // POST: Dish/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dish = await _context.Dishes.FindAsync(id);
            if (dish != null)
            {
                _context.Dishes.Remove(dish);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DishExists(int id)
        {
            return _context.Dishes.Any(e => e.Id == id);
        }
    }
}
