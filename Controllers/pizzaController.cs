using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pizza_App.Data;
using Pizza_App.Models;

namespace Pizza_App.Controllers
{
   
    public class pizzaController : Controller
    {
        private readonly MyContext _context;

        public pizzaController(MyContext context)
        {
            _context = context;
        }

        // GET: pizza
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.pizzas.ToListAsync());
        }

        // GET: pizza/Details/5
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizza = await _context.pizzas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pizza == null)
            {
                return NotFound();
            }

            return View(pizza);
        }

        // GET: pizza/Create
        [Authorize(Roles = "ADMIN")]
        public IActionResult Create()
        {
           return View();
        }

        // POST: pizza/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Create([Bind("Id,nombre_pizza,cantidad_porciones,descripcion,precio_unitario,tamano")] pizza pizza)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pizza);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pizza);
        }

        // GET: pizza/Edit/5
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizza = await _context.pizzas.FindAsync(id);
            if (pizza == null)
            {
                return NotFound();
            }
            return View(pizza);
        }

        // POST: pizza/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,nombre_pizza,cantidad_porciones,descripcion,precio_unitario,tamano")] pizza pizza)
        {
            if (id != pizza.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pizza);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!pizzaExists(pizza.Id))
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
            return View(pizza);
        }

        // GET: pizza/Delete/5
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizza = await _context.pizzas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pizza == null)
            {
                return NotFound();
            }

            return View(pizza);
        }

        // POST: pizza/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pizza = await _context.pizzas.FindAsync(id);
            _context.pizzas.Remove(pizza);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool pizzaExists(int id)
        {
            return _context.pizzas.Any(e => e.Id == id);
        }

        [Authorize(Roles = "EMPLEADO,ADMIN")]
        public async Task<IActionResult> CatalogoPizza()
        {
            return View(await _context.pizzas.ToListAsync());
        }
    }
}
