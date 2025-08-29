using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

public class PropietariosController : Controller
{
    private readonly PropietarioRepositorio _repo;

    public PropietariosController(PropietarioRepositorio repo)
    {
        _repo = repo ?? throw new ArgumentNullException(nameof(repo));
    }

    // GET: /Propietarios
    public IActionResult Index()
    {
        var lista = _repo.GetAll();
        return View(lista);
    }

    // GET: /Propietarios/Details/5
    public IActionResult Details(int id)
    {
        var entidad = _repo.GetById(id);
        if (entidad == null) return NotFound();
        return View(entidad);
    }

    // GET: /Propietarios/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: /Propietarios/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Propietario propietario)
    {
        if (!ModelState.IsValid)
            return View(propietario);

        try
        {
            _repo.Add(propietario);
            // Opcional: Message para la vista
            TempData["Success"] = "Propietario creado correctamente.";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            // Loguear excepción según tu logger (no incluido aquí)
            ModelState.AddModelError(string.Empty, "Error al crear el propietario: " + ex.Message);
            return View(propietario);
        }
    }

    // GET: /Propietarios/Edit/5
    public IActionResult Edit(int id)
    {
        var entidad = _repo.GetById(id);
        if (entidad == null) return NotFound();
        return View(entidad);
    }

    // POST: /Propietarios/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Propietario propietario)
    {
        if (!ModelState.IsValid)
            return View(propietario);

        try
        {
            _repo.Update(propietario);
            TempData["Success"] = "Propietario actualizado correctamente.";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, "Error al actualizar el propietario: " + ex.Message);
            return View(propietario);
        }
    }

    // GET: /Propietarios/Delete/5
    public IActionResult Delete(int id)
    {
        var entidad = _repo.GetById(id);
        if (entidad == null) return NotFound();
        return View(entidad);
    }

    // POST: /Propietarios/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        try
        {
            _repo.Delete(id);
            TempData["Success"] = "Propietario eliminado correctamente.";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            // Si falló, re-mostrar la vista Delete con el error
            ModelState.AddModelError(string.Empty, "Error al eliminar el propietario: " + ex.Message);
            var entidad = _repo.GetById(id);
            if (entidad == null) return RedirectToAction(nameof(Index));
            return View("Delete", entidad);
        }
    }
}
