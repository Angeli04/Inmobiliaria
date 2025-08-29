using Microsoft.AspNetCore.Mvc;
using Inmobiliaria.Models;
using System;

namespace Inmobiliaria.Controllers
{
    public class InquilinosController : Controller
    {
        private readonly InquilinoRepositorio _repo;

        public InquilinosController(InquilinoRepositorio repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        public IActionResult Index()
        {
            var lista = _repo.GetAll();
            return View(lista);
        }

        public IActionResult Details(int id)
        {
            var entidad = _repo.GetById(id);
            if (entidad == null) return NotFound();
            return View(entidad);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Inquilino inquilino)
        {
            if (!ModelState.IsValid) return View(inquilino);

            try
            {
                _repo.Add(inquilino);
                TempData["Success"] = "Inquilino creado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error al crear inquilino: " + ex.Message);
                return View(inquilino);
            }
        }

        public IActionResult Edit(int id)
        {
            var entidad = _repo.GetById(id);
            if (entidad == null) return NotFound();
            return View(entidad);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Inquilino inquilino)
        {
            if (!ModelState.IsValid) return View(inquilino);

            try
            {
                _repo.Update(inquilino);
                TempData["Success"] = "Inquilino actualizado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error al actualizar inquilino: " + ex.Message);
                return View(inquilino);
            }
        }

        public IActionResult Delete(int id)
        {
            var entidad = _repo.GetById(id);
            if (entidad == null) return NotFound();
            return View(entidad);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                _repo.Delete(id);
                TempData["Success"] = "Inquilino eliminado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error al eliminar inquilino: " + ex.Message);
                var entidad = _repo.GetById(id);
                if (entidad == null) return RedirectToAction(nameof(Index));
                return View("Delete", entidad);
            }
        }
    }
}
