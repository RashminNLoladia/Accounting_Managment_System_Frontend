using Microsoft.AspNetCore.Mvc;
using Accounting_Managment_System_Frontend.Models;
using Accounting_Managment_System_Frontend.Services;

namespace Accounting_Managment_System_Frontend.Controllers
{
    public class TaxRateController : Controller
    {
        private readonly ApiService _api;

        public TaxRateController(ApiService api)
        {
            _api = api;
        }

        // GET: /TaxRate
        public async Task<IActionResult> Index()
        {
            var list = await _api.GetAsync<IEnumerable<TaxRateViewModel>>("api/taxrate");
            return View(list ?? new List<TaxRateViewModel>());
        }

        // GET: /TaxRate/Create
        public IActionResult Create()
        {
            return View(new TaxRateViewModel());
        }

        // POST: /TaxRate/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TaxRateViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var ok = await _api.PostAsync("api/taxrate", model);
            if (!ok)
            {
                ModelState.AddModelError("", "Failed to create tax rate.");
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: /TaxRate/Edit/{id}
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _api.GetAsync<TaxRateViewModel>($"api/taxrate/{id}");
            if (model == null) return NotFound();

            return View(model);
        }

        // POST: /TaxRate/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TaxRateViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var ok = await _api.PutAsync($"api/taxrate/{id}", model);
            if (!ok)
            {
                ModelState.AddModelError("", "Failed to update tax rate.");
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: /TaxRate/Delete/{id}
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _api.GetAsync<TaxRateViewModel>($"api/taxrate/{id}");
            if (model == null) return NotFound();

            return View(model);
        }

        // POST: /TaxRate/DeleteConfirmed
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ok = await _api.DeleteAsync($"api/taxrate/{id}");
            if (!ok) TempData["Error"] = "Delete failed.";

            return RedirectToAction(nameof(Index));
        }
    }
}
