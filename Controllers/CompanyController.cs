using Accounting_Managment_System_Frontend.Models;
using Accounting_Managment_System_Frontend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Accounting_Managment_System_Frontend.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ApiService _api;

        public CompanyController(ApiService api)
        {
            _api = api;
        }

        // GET: /Company
        public async Task<IActionResult> Index()
        {
            var list = await _api.GetAsync<IEnumerable<CompanyViewModel>>("api/company");
            return View(list ?? new List<CompanyViewModel>());
        }

        // GET: /Company/Create
        public IActionResult Create()
        {
            return View(new CompanyViewModel { IsActive = true });
        }

        // POST: /Company/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CompanyViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var ok = await _api.PostAsync("api/company", model);
            if (!ok)
            {
                ModelState.AddModelError("", "Failed to create company.");
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: /Company/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _api.GetAsync<CompanyViewModel>($"api/company/{id}");
            if (model == null) return NotFound();

            return View(model);
        }

        // POST: /Company/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CompanyViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var ok = await _api.PutAsync($"api/company/{id}", model);
            if (!ok)
            {
                ModelState.AddModelError("", "Failed to update company.");
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: /Company/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _api.GetAsync<CompanyViewModel>($"api/company/{id}");
            if (model == null) return NotFound();

            return View(model);
        }

        // POST: /Company/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ok = await _api.DeleteAsync($"api/company/{id}");
            if (!ok)
            {
                TempData["Error"] = "Delete failed.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
