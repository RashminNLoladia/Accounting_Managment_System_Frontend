using Accounting_Managment_System_Frontend.Models;
using Accounting_Managment_System_Frontend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Accounting_Managment_System_Frontend.Controllers
{
    public class RoleController : Controller
    {
        private readonly ApiService _api;

        public RoleController(ApiService api)
        {
            _api = api;
        }

        // GET: /Role
        public async Task<IActionResult> Index()
        {
            var list = await _api.GetAsync<IEnumerable<RoleViewModel>>("api/role");
            return View(list ?? new List<RoleViewModel>());
        }

        // GET: /Role/Create
        public IActionResult Create()
        {
            return View(new RoleViewModel());
        }

        // POST: /Role/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoleViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var ok = await _api.PostAsync("api/role", model);
            if (!ok)
            {
                ModelState.AddModelError("", "Failed to create role.");
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: /Role/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _api.GetAsync<RoleViewModel>($"api/role/{id}");
            if (model == null) return NotFound();
            return View(model);
        }

        // POST: /Role/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, RoleViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var ok = await _api.PutAsync($"api/role/{id}", model);
            if (!ok)
            {
                ModelState.AddModelError("", "Failed to update role.");
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: /Role/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _api.GetAsync<RoleViewModel>($"api/role/{id}");
            if (model == null) return NotFound();
            return View(model);
        }

        // POST: /Role/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ok = await _api.DeleteAsync($"api/role/{id}");
            if (!ok) TempData["Error"] = "Delete failed.";
            return RedirectToAction(nameof(Index));
        }
    }
}
