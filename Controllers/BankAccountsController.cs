using Accounting_Managment_System_Frontend.Models;
using Accounting_Managment_System_Frontend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Accounting_Managment_System_Frontend.Controllers
{
    public class BankAccountsController : Controller
    {
        private readonly ApiService _api;

        public BankAccountsController(ApiService api)
        {
            _api = api;
        }

        // ==================
        // INDEX
        // ==================
        public async Task<IActionResult> Index()
        {
            var list = await _api.GetAsync<IEnumerable<BankAccountViewModel>>("api/bankaccount");
            return View(list ?? new List<BankAccountViewModel>());
        }

        // ==================
        // CREATE - GET
        // ==================
        public async Task<IActionResult> Create()
        {
            await LoadDropdowns();
            return View(new BankAccountViewModel());
        }

        // ==================
        // CREATE - POST
        // ==================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BankAccountViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await LoadDropdowns();
                return View(model);
            }

            var ok = await _api.PostAsync("api/bankaccount", model);
            if (!ok)
            {
                ModelState.AddModelError("", "Failed to create bank account.");
                await LoadDropdowns();
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        // ==================
        // EDIT - GET
        // ==================
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _api.GetAsync<BankAccountViewModel>($"api/bankaccount/{id}");
            if (model == null) return NotFound();

            await LoadDropdowns();
            return View(model);
        }

        // ==================
        // EDIT - POST
        // ==================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BankAccountViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await LoadDropdowns();
                return View(model);
            }

            var ok = await _api.PutAsync($"api/bankaccount/{id}", model);
            if (!ok)
            {
                ModelState.AddModelError("", "Failed to update bank account.");
                await LoadDropdowns();
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        // ==================
        // DELETE - GET
        // ==================
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _api.GetAsync<BankAccountViewModel>($"api/bankaccount/{id}");
            if (model == null) return NotFound();

            return View(model);
        }

        // ==================
        // DELETE - POST
        // ==================
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _api.DeleteAsync($"api/bankaccount/{id}");
            return RedirectToAction(nameof(Index));
        }

        // ==================
        // DROPDOWNS
        // ==================
        private async Task LoadDropdowns()
        {
            ViewBag.Companies = await _api.GetAsync<IEnumerable<CompanyViewModel>>("api/company")
                                ?? new List<CompanyViewModel>();

            ViewBag.Currencies = await _api.GetAsync<IEnumerable<CurrencyViewModel>>("api/currency")
                                 ?? new List<CurrencyViewModel>();
        }
    }
}
