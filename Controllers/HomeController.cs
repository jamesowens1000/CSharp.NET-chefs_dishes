using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ChefsNDishes.Models;

namespace ChefsNDishes.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dbContext;

        public HomeController(MyContext context)
        {
            dbContext = context;
        }

        [HttpGet("")]
        public IActionResult Chefs()
        {
            List<Chef> EveryChef = dbContext.Chefs.Include(c => c.CreatedDishes).ToList();
            ViewBag.AllChefs = EveryChef;
            return View("Chefs");
        }

        [HttpGet("dishes")]
        public IActionResult Dishes()
        {
            List<Dish> EveryDish = dbContext.Dishes.Include(d => d.Creator).ToList();
            ViewBag.AllDishes = EveryDish;
            return View("Dishes");
        }

        [HttpGet("new")]
        public IActionResult AddChef()
        {
            return View("AddChef");
        }

        [HttpPost("InsertChef")]
        public IActionResult InsertChef(Chef newChef)
        {
            if(ModelState.IsValid)
            {
                dbContext.Add(newChef);
                dbContext.SaveChanges();
                return RedirectToAction("Chefs");
            }
            else
            {
                return View("AddChef");
            }
        }

        [HttpGet("dishes/new")]
        public IActionResult AddDish()
        {
            List<Chef> EveryChef = dbContext.Chefs.ToList();
            ViewBag.AllChefs = EveryChef;
            return View("AddDish");
        }

        [HttpPost("InsertDish")]
        public IActionResult InsertDish(Dish newDish)
        {
            dbContext.Add(newDish);
            dbContext.SaveChanges();
            return RedirectToAction("Dishes");
        }
    }
}