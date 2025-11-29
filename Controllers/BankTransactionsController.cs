using Accounting_Managment_System_Frontend.Models;
using Accounting_Managment_System_Frontend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

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
            var model = await BuildBankTransactionViewModelAsync();
            return View(model);
        }

        // POST: /BankTransactions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BankTransactionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model = await BuildBankTransactionViewModelAsync(model);
                return View(model);
            }

            var ok = await _api.PostAsync("api/banktransaction", model);
            if (!ok)
            {
                ModelState.AddModelError("", "Failed to create bank transaction.");
                model = await BuildBankTransactionViewModelAsync(model);
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: /BankTransactions/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _api.GetAsync<BankTransactionViewModel>($"api/banktransaction/{id}");
            if (model == null) return NotFound();

            model = await BuildBankTransactionViewModelAsync(model);
            return View(model);
        }

        // POST: /BankTransactions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BankTransactionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model = await BuildBankTransactionViewModelAsync(model);
                return View(model);
            }

            var ok = await _api.PutAsync($"api/banktransaction/{id}", model);
            if (!ok)
            {
                ModelState.AddModelError("", "Failed to update bank transaction.");
                model = await BuildBankTransactionViewModelAsync(model);
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

        // Helper: build BankTransactionViewModel with BankAccounts dropdown
        private async Task<BankTransactionViewModel> BuildBankTransactionViewModelAsync(BankTransactionViewModel existingModel = null)
        {
            var banks = await _api.GetAsync<IEnumerable<BankAccountViewModel>>("api/bankaccount")
                        ?? new List<BankAccountViewModel>();

            var model = existingModel ?? new BankTransactionViewModel
            {
                TransactionDate = System.DateTime.Today
            };

            // Populate the dropdown safely
            model.BankAccounts = banks.Select(b => new SelectListItem
            {
                Value = b.BankAccountId.ToString(),
                Text = $"{b.BankName} - {b.AccountNumber}" // Adjust fields according to your BankAccountViewModel
            }).ToList();

            return model;
        }
    }
}
