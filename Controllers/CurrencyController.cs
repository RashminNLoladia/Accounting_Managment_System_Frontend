using Accounting_Managment_System_Frontend.Models;
using Accounting_Managment_System_Frontend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Accounting_Managment_System_Frontend.Controllers
{
    public class CurrencyController : Controller
    {
        private readonly ApiService _api;

        public CurrencyController(ApiService api)
        {
            _api = api;
        }

        // GET: /Currency
        public async Task<IActionResult> Index()
        {
            var list = await _api.GetAsync<IEnumerable<CurrencyViewModel>>("api/currency");
            return View(list ?? new List<CurrencyViewModel>());
        }

        // GET: /Currency/Create
        public IActionResult Create()
        {
            return View(new CurrencyViewModel());
        }

        // POST: /Currency/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CurrencyViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var ok = await _api.PostAsync("api/currency", model);
            if (!ok)
            {
                ModelState.AddModelError("", "Failed to create currency.");
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: /Currency/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _api.GetAsync<CurrencyViewModel>($"api/currency/{id}");
            if (model == null) return NotFound();
            return View(model);
        }

        // POST: /Currency/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CurrencyViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var ok = await _api.PutAsync($"api/currency/{id}", model);
            if (!ok)
            {
                ModelState.AddModelError("", "Failed to update currency.");
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: /Currency/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _api.GetAsync<CurrencyViewModel>($"api/currency/{id}");
            if (model == null) return NotFound();
            return View(model);
        }

        // POST: /Currency/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ok = await _api.DeleteAsync($"api/currency/{id}");
            if (!ok) TempData["Error"] = "Delete failed.";
            return RedirectToAction(nameof(Index));
        }
    }
}
