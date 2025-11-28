using Accounting_Managment_System_Frontend.Models;
using Accounting_Managment_System_Frontend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Accounting_Managment_System_Frontend.Controllers
{
    public class JournalEntriesController : Controller
    {
        private readonly ApiService _api;

        public JournalEntriesController(ApiService api)
        {
            _api = api;
        }

        // GET: /JournalEntries
        public async Task<IActionResult> Index()
        {
            var list = await _api.GetAsync<IEnumerable<JournalEntryViewModel>>("api/journalentry");
            return View(list ?? new List<JournalEntryViewModel>());
        }

        // GET: /JournalEntries/Create
        public async Task<IActionResult> Create()
        {
            await PopulateDropdowns();
            var vm = new JournalEntryViewModel
            {
                Date = DateTime.Today,
                Lines = new List<JournalEntryLineViewModel>
                {
                    new JournalEntryLineViewModel(),
                    new JournalEntryLineViewModel()
                } // start with 2 blank lines
            };
            return View(vm);
        }

        // POST: /JournalEntries/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(JournalEntryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateDropdowns();
                return View(model);
            }

            // optional: basic server-side balancing check
            var totalDebit = model.Lines?.Sum(l => l.Debit) ?? 0m;
            var totalCredit = model.Lines?.Sum(l => l.Credit) ?? 0m;
            if (totalDebit != totalCredit)
            {
                ModelState.AddModelError("", "Total Debit must equal Total Credit.");
                await PopulateDropdowns();
                return View(model);
            }

            var ok = await _api.PostAsync("api/journalentry", model);
            if (!ok)
            {
                ModelState.AddModelError("", "Failed to create journal entry.");
                await PopulateDropdowns();
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: /JournalEntries/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _api.GetAsync<JournalEntryViewModel>($"api/journalentry/{id}");
            if (model == null) return NotFound();

            await PopulateDropdowns();
            // ensure at least one line exists for UI
            if (model.Lines == null || !model.Lines.Any())
                model.Lines = new List<JournalEntryLineViewModel> { new JournalEntryLineViewModel() };

            return View(model);
        }

        // POST: /JournalEntries/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, JournalEntryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateDropdowns();
                return View(model);
            }

            var totalDebit = model.Lines?.Sum(l => l.Debit) ?? 0m;
            var totalCredit = model.Lines?.Sum(l => l.Credit) ?? 0m;
            if (totalDebit != totalCredit)
            {
                ModelState.AddModelError("", "Total Debit must equal Total Credit.");
                await PopulateDropdowns();
                return View(model);
            }

            var ok = await _api.PutAsync($"api/journalentry/{id}", model);
            if (!ok)
            {
                ModelState.AddModelError("", "Failed to update journal entry.");
                await PopulateDropdowns();
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: /JournalEntries/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _api.GetAsync<JournalEntryViewModel>($"api/journalentry/{id}");
            if (model == null) return NotFound();
            return View(model);
        }

        // POST: /JournalEntries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ok = await _api.DeleteAsync($"api/journalentry/{id}");
            if (!ok) TempData["Error"] = "Delete failed.";
            return RedirectToAction(nameof(Index));
        }

        // helper: populate companies and accounts for dropdowns
        private async Task PopulateDropdowns()
        {
            var companies = await _api.GetAsync<IEnumerable<CompanyViewModel>>("api/company");
            var accounts = await _api.GetAsync<IEnumerable<ChartOfAccountViewModel>>("api/chartofaccount");

            ViewBag.Companies = companies ?? new List<CompanyViewModel>();
            ViewBag.Accounts = accounts ?? new List<ChartOfAccountViewModel>();
        }
    }
}
