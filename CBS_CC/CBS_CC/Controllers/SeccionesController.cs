using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CBS_CC.Models;

namespace CBS_CC.Controllers
{
    public class SeccionesController : Controller
    {
        private readonly CBS_CCContext _context;

        public SeccionesController(CBS_CCContext context)
        {
            _context = context;
        }

        // GET: Secciones
        public async Task<IActionResult> Index()
        {
            return View(await _context.Seccion.ToListAsync());
        }

        public async Task<IActionResult> Save(int? id)
        {
            if (id == null)
            {
                ViewData["Operacion"] = "Create";
                return PartialView("_SavePartialView");
            }
            else
            {
                var secciones = await _context.Seccion.FirstAsync(s => s.SeccionId == id);
                ViewData["Operacion"] = "Edit";
                return PartialView("_SavePartialView", secciones);
            }
        }

        [HttpPost, ActionName("Save")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SavePost(int? id)
        {
            string operacion = (id != null) ? "Edit" : "Create";
            var seccionToSave = (operacion == "Edit") ? await _context.Seccion.FindAsync(id) : new Seccion();

            if (ModelState.IsValid)
            {
                try
                {
                    if (await TryUpdateModelAsync<Seccion>(seccionToSave, "", s => s.Descripcion))
                    {
                        if (operacion == "Create")
                        {
                            _context.Seccion.Add(seccionToSave);
                        }
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SeccionExists((int)id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Redirect(Url.Action("Index", "Secciones"));
            }
            return PartialView("_SavePartialView", seccionToSave);
        }

        private bool SeccionExists(int id)
        {
            return _context.Seccion.Any(e => e.SeccionId == id);
        }
    }
}
