using Accounting_Managment_System_Frontend.Models;
using Accounting_Managment_System_Frontend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Accounting_Managment_System_Frontend.Controllers
{
    public class BankTransactionsController : Controller
    {
        private readonly ApiService _api;

        public BankTransactionsController(ApiService api)
        {
            _api = api;
        }

        // GET: /BankTransactions
        public async Task<IActionResult> Index()
        {
            var list = await _api.GetAsync<IEnumerable<BankTransactionViewModel>>("api/banktransaction");
            return View(list ?? new List<BankTransactionViewModel>());
        }

        // GET: /BankTransactions/Create
        public async Task<IActionResult> Create()
        {
            await PopulateDropdowns();
            return View(new BankTransactionViewModel { TransactionDate = DateTime.Today });
        }

        // POST: /BankTransactions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BankTransactionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateDropdowns();
                return View(model);
            }

            var ok = await _api.PostAsync("api/banktransaction", model);
            if (!ok)
            {
                ModelState.AddModelError("", "Failed to create bank transaction.");
                await PopulateDropdowns();
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: /BankTransactions/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _api.GetAsync<BankTransactionViewModel>($"api/banktransaction/{id}");
            if (model == null) return NotFound();

            await PopulateDropdowns();
            return View(model);
        }

        // POST: /BankTransactions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BankTransactionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateDropdowns();
                return View(model);
            }

            var ok = await _api.PutAsync($"api/banktransaction/{id}", model);
            if (!ok)
            {
                ModelState.AddModelError("", "Failed to update bank transaction.");
                await PopulateDropdowns();
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: /BankTransactions/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _api.GetAsync<BankTransactionViewModel>($"api/banktransaction/{id}");
            if (model == null) return NotFound();
            return View(model);
        }

        // POST: /BankTransactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ok = await _api.DeleteAsync($"api/banktransaction/{id}");
            if (!ok) TempData["Error"] = "Delete failed.";
            return RedirectToAction(nameof(Index));
        }

        // Helper: load dropdowns for Bank Accounts
        private async Task PopulateDropdowns()
        {
            var banks = await _api.GetAsync<IEnumerable<BankAccountViewModel>>("api/bankaccount");
            ViewBag.BankAccounts = banks ?? new List<BankAccountViewModel>();
        }
    }
}
