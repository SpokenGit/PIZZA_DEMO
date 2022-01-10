using System;
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
    
    public class ordenController : Controller
    {
        private readonly MyContext _context;
        public Dictionary<int, string> estadosList = new Dictionary<int, string>() ;

        public ordenController(MyContext context)
        {
            _context = context;
            estadosList.Add(1,"ENTREGADO");
            estadosList.Add(2, "PENDIENTE DE ENTREGA");
            estadosList.Add(3, "CANCELADO");
        }

        //GET: Lista de ordenes pendientes
        [Authorize(Roles = "ADMIN,EMPLEADO")]
        public IActionResult PendingOrders()
        {
            var MypendingOrders = _context.ordenes.Include(o => o.pizza).Include(o => o.usuario);
            MypendingOrders.Where(s=> s.estado_orden == 2);
            List<pendingOrders> Listpendingorders = new List<pendingOrders>();
            foreach (var item in MypendingOrders)
            {
                Listpendingorders.Add(new pendingOrders() 
                { fechaorden= item.fecha_orden, numeroOrden = item.Id,   cantidadOrden = item.cantidad, solicitanteOrden = item.nombre_solicitante, 
                precioOrden = item.pizza.precio_unitario, totalOrden = Math.Round(item.cantidad*item.pizza.precio_unitario, 2)
            });
            }
            return View(Listpendingorders.OrderBy(f=>f.fechaorden).ToList());
        }

        // GET: orden
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Index()
        {
            var myContext = _context.ordenes.Include(o => o.pizza).Include(o => o.usuario);
            return View(await myContext.ToListAsync());
        }

        // GET: orden/Details/5
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orden = await _context.ordenes
                .Include(o => o.pizza)
                .Include(o => o.usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orden == null)
            {
                return NotFound();
            }

            return View(orden);
        }

        // GET: orden/Create
        [Authorize(Roles = "ADMIN,EMPLEADO")]
        public IActionResult Create()
        {
            ViewData["pizzaId"] = new SelectList(_context.pizzas, "Id", "nombre_pizza");
            ViewData["usuarioId"] = new SelectList(_context.usuarios, "Id", "nombre_usuario");
            ViewData["estados"] = new SelectList(estadosList,"Key","Value",0);
            return View();
        }

        // POST: orden/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ADMIN,EMPLEADO")]
        public async Task<IActionResult> Create([Bind("Id,nombre_solicitante,pizzaId,cantidad,fecha_orden,direccion_envio,comentarios,estado_orden,usuarioId")] orden orden)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orden);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["pizzaId"] = new SelectList(_context.pizzas, "Id", "nombre_pizza", orden.pizzaId);
            ViewData["usuarioId"] = new SelectList(_context.usuarios, "Id", "nombre_usuario", orden.usuarioId);
            ViewData["estados"] = new SelectList(estadosList, "Key", "Value", orden.estado_orden);
            return View(orden);
        }

        // GET: orden/Edit/5
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orden = await _context.ordenes.FindAsync(id);
            if (orden == null)
            {
                return NotFound();
            }
            ViewData["pizzaId"] = new SelectList(_context.pizzas, "Id", "nombre_pizza", orden.pizzaId);
            ViewData["usuarioId"] = new SelectList(_context.usuarios, "Id", "nombre_usuario", orden.usuarioId);
            ViewData["estados"] = new SelectList(estadosList, "Key", "Value", orden.estado_orden);
            return View(orden);
        }

        // POST: orden/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,nombre_solicitante,pizzaId,cantidad,fecha_orden,direccion_envio,comentarios,estado_orden,usuarioId")] orden orden)
        {
            if (id != orden.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orden);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ordenExists(orden.Id))
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
            ViewData["pizzaId"] = new SelectList(_context.pizzas, "Id", "nombre_pizza", orden.pizzaId);
            ViewData["usuarioId"] = new SelectList(_context.usuarios, "Id", "nombre_usuario", orden.usuarioId);
            ViewData["estados"] = new SelectList(estadosList, "Key", "Value", orden.estado_orden);
            return View(orden);
        }

        // GET: orden/Delete/5
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orden = await _context.ordenes
                .Include(o => o.pizza)
                .Include(o => o.usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orden == null)
            {
                return NotFound();
            }

            return View(orden);
        }

        // POST: orden/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orden = await _context.ordenes.FindAsync(id);
            _context.ordenes.Remove(orden);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ordenExists(int id)
        {
            return _context.ordenes.Any(e => e.Id == id);
        }
    }
}
