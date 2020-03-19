using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TP_Module5_Pizza_Part1.Models;

namespace TP_Module5_Pizza_Part1.Controllers
{
    public class PizzaController : Controller
    {
        private static List<Ingredient> listIngredients = Pizza.IngredientsDisponibles;
        private static List<Pate> listPates = Pizza.PatesDisponibles;

        private static List<Pizza> pizzas;

        public PizzaController()
        {
            if (pizzas == null)
            {
                pizzas = FakeDbPizza.Instance.Pizzas;
            }
        }

        // GET: Pizza
        public ActionResult Index()
        {
            return View(pizzas);
        }

        // GET: Pizza/Details/5
        public ActionResult Details(int id)
        {
            Pizza pizza = pizzas.FirstOrDefault(p => p.Id == id);
            if (pizza != null)
            {
                return View(pizza);
            }
            return RedirectToAction("Index");
        }

        // GET: Pizza/Create
        public ActionResult Create()
        {
            PizzaVm pizzaVM = new PizzaVm();
            pizzaVM.setIngredients(listIngredients);
            pizzaVM.setPates(listPates);
            return View(pizzaVM);
        }

        // POST: Pizza/Create
        [HttpPost]
        public ActionResult Create(PizzaVm pizzaVM)
        {
            try
            {
                Pizza nouvellePizza = pizzaVM.Pizza;
                int maxId = pizzas.Max(p => p.Id);
                nouvellePizza.Pate = listPates.FirstOrDefault(p => p.Id == pizzaVM.selectedPate);
                foreach (var ingredient in pizzaVM.selectedIngredients)
                {
                    nouvellePizza.Ingredients.Add(listIngredients.FirstOrDefault(i => i.Id == ingredient));
                }
                nouvellePizza.Id = maxId + 1;
                pizzas.Add(nouvellePizza);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Pizza/Edit/5
        public ActionResult Edit(int id)
        {

            Pizza pizza = pizzas.FirstOrDefault(p => p.Id == id);
            if (pizza != null)
            {
                var pizzaVM = new PizzaVm();
                pizzaVM.setIngredients(listIngredients);
                pizzaVM.setPates(listPates);
                pizzaVM.Pizza = pizza;
                return View(pizzaVM);
            }
            return RedirectToAction("Index");
        }

        // POST: Pizza/Edit/5
        [HttpPost]
        public ActionResult Edit(PizzaVm pizzaVm)
        {
            try
            {
                Pizza pizzaDb = pizzas.FirstOrDefault(p => p.Id == pizzaVm.Pizza.Id);
                pizzaDb.Nom = pizzaVm.Pizza.Nom;
                pizzaDb.Pate = listPates.FirstOrDefault(p => p.Id == pizzaVm.selectedPate);
                foreach (var ingredient in pizzaVm.selectedIngredients)
                {
                    pizzaDb.Ingredients.Add(listIngredients.FirstOrDefault(i => i.Id == ingredient));
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Pizza/Delete/5
        public ActionResult Delete(int id)
        {
            Pizza pizza = pizzas.FirstOrDefault(p => p.Id == id);
            if (pizza != null)
            {
                return View(pizza);
            }
            return RedirectToAction("Index");
        }

        // POST: Pizza/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                Pizza pizza = pizzas.FirstOrDefault(p => p.Id == id);
                pizzas.Remove(pizza);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
