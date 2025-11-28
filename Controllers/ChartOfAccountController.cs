using Accounting_Managment_System_Frontend.Models;
using Accounting_Managment_System_Frontend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Accounting_Managment_System_Frontend.Controllers
{
    public class ChartOfAccountController : Controller
    {
        private readonly ApiService _api;

        public ChartOfAccountController(ApiService api)
        {
            _api = api;
        }

        // GET: /ChartOfAccount
        public async Task<IActionResult> Index()
        {
            var list = await _api.GetAsync<IEnumerable<ChartOfAccountViewModel>>("api/chartofaccount");
            return View(list ?? new List<ChartOfAccountViewModel>());
        }

        // GET: /ChartOfAccount/Create
        public async Task<IActionResult> Create()
        {
            var model = new ChartOfAccountViewModel();
            await PopulateDropdowns();
            return View(model);
        }

        // POST: /ChartOfAccount/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ChartOfAccountViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateDropdowns();
                return View(model);
            }

            var ok = await _api.PostAsync("api/chartofaccount", model);
            if (!ok)
            {
                ModelState.AddModelError("", "Failed to create account.");
                await PopulateDropdowns();
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: /ChartOfAccount/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _api.GetAsync<ChartOfAccountViewModel>($"api/chartofaccount/{id}");
            if (model == null) return NotFound();

            await PopulateDropdowns();
            return View(model);
        }

        // POST: /ChartOfAccount/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ChartOfAccountViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateDropdowns();
                return View(model);
            }

            var ok = await _api.PutAsync($"api/chartofaccount/{id}", model);
            if (!ok)
            {
                ModelState.AddModelError("", "Failed to update account.");
                await PopulateDropdowns();
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: /ChartOfAccount/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _api.GetAsync<ChartOfAccountViewModel>($"api/chartofaccount/{id}");
            if (model == null) return NotFound();
            return View(model);
        }

        // POST: /ChartOfAccount/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ok = await _api.DeleteAsync($"api/chartofaccount/{id}");
            if (!ok) TempData["Error"] = "Delete failed.";
            return RedirectToAction(nameof(Index));
        }

        // Populate dropdowns for Companies, AccountTypes, ParentAccounts
        private async Task PopulateDropdowns()
        {
            var companies = await _api.GetAsync<IEnumerable<KeyValuePair<int, string>>>("api/chartofaccount/companydropdown");
            var accountTypes = await _api.GetAsync<IEnumerable<KeyValuePair<int, string>>>("api/chartofaccount/accounttypedropdown");
            var parentAccounts = await _api.GetAsync<IEnumerable<KeyValuePair<int, string>>>("api/chartofaccount/parentaccountdropdown");


            ViewBag.Companies = companies ?? new List<KeyValuePair<int, string>>();
            ViewBag.AccountTypes = accountTypes ?? new List<KeyValuePair<int, string>>();
            ViewBag.ParentAccounts = parentAccounts ?? new List<KeyValuePair<int, string>>();
        }
    }
}
