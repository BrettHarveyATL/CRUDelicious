using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CRUDelicious.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CRUDelicious.Controllers
{
    public class HomeController : Controller
    {
        private MyContext _context;

        public HomeController(MyContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            ViewBag.AllChefs = _context.Chefs
                .Include(chef => chef.ChefDishes)
                .ToList();
            return View();
        }
        [HttpGet("dishes")]
        public IActionResult AllDishes()
        {
            ViewBag.AllDishes = _context.Dishes.ToList();
            ViewBag.AllChefs = _context.Chefs.ToList();
            return View();
        }
        [HttpGet("NewDish")]
        public IActionResult NewDish()
        {
            ViewBag.AllChefs = _context.Chefs.ToList();
            return View();
        }
        [HttpGet("NewChef")]
        public IActionResult NewChef()
        {
            return View();
        }
        [HttpPost("AddChef")]
        public IActionResult AddChef(Chef newChef)
        {
            if (ModelState.IsValid)
            {
                _context.Chefs.Add(newChef);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("NewChef");
        }
        [HttpPost("AddDish")]
        public IActionResult AddDish(Dish newDish)
        {
            if (ModelState.IsValid)
            {
                _context.Dishes.Add(newDish);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("NewDish");
        }
        [HttpGet("dish/{DishId}")]
        public IActionResult SingleDish(int dishId)
        {
            Dish ThisDish = _context.Dishes.FirstOrDefault(dish => dish.DishId == dishId);
            Console.WriteLine(ThisDish.Name);
            return View(ThisDish);
        }
        [HttpGet("dish/{dishId}/delete")]
        public IActionResult DeleteDish(int dishId)
        {
            Dish deleteDish = _context.Dishes.FirstOrDefault(dish => dish.DishId == dishId);
            _context.Dishes.Remove(deleteDish);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet("dish/{dishId}/edit")]
        public IActionResult EditDishPage(int dishId)
        {
            Dish EditDish = _context.Dishes.FirstOrDefault(dish => dish.DishId == dishId);
            if (EditDish == null)
            {
                return RedirectToAction("Index");
            }
            Console.WriteLine(dishId);
            return View(EditDish);
        }
        [HttpPost("EditDish")]
        public IActionResult EditDish(Dish newEdit)
        {
            Console.WriteLine(newEdit);
            Dish EditDish = _context.Dishes.FirstOrDefault(dish => dish.DishId == newEdit.DishId);
            EditDish.Name = newEdit.Name;
            EditDish.Tastiness = newEdit.Tastiness;
            EditDish.Calories = newEdit.Calories;
            EditDish.Description = newEdit.Description;
            EditDish.UpdatedAt = DateTime.Now;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
