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
    public class AsignaturasController : Controller
    {
        private readonly CBS_CCContext _context;

        public AsignaturasController(CBS_CCContext context)
        {
            _context = context;
        }

        // GET: Asignaturas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Asignatura.ToListAsync());
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
                var asignatura = await _context.Asignatura.FirstAsync(a => a.AsignaturaId == id);
                ViewData["Operacion"] = "Edit";
                return PartialView("_SavePartialView", asignatura);
            }
        }

        [HttpPost, ActionName("Save")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SavePost(int? id)
        {
            string operacion = (id != null) ? "Edit" : "Create";
            var asignaturaToSave = (operacion == "Edit") ? await _context.Asignatura.FindAsync(id) : new Asignatura();

            if (ModelState.IsValid)
            {
                try
                {
                    if (await TryUpdateModelAsync<Asignatura>(asignaturaToSave, "", a => a.Descripcion ))
                    {
                        if (operacion == "Create")
                        {
                            _context.Asignatura.Add(asignaturaToSave);
                        }
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AsignaturaExists((int)id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Redirect(Url.Action("Index", "Asignaturas"));
            }
            return PartialView("_SavePartialView", asignaturaToSave);
        }

        private bool AsignaturaExists(int id)
        {
            return _context.Asignatura.Any(e => e.AsignaturaId == id);
        }
    }
}
