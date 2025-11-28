using Accounting_Managment_System_Frontend.Models;
using Accounting_Managment_System_Frontend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Accounting_Managment_System_Frontend.Controllers
{
    public class AccountTypesController : Controller
    {
        private readonly ApiService _api;

        public AccountTypesController(ApiService api)
        {
            _api = api;
        }

        // GET: /AccountTypes
        public async Task<IActionResult> Index()
        {
            var list = await _api.GetAsync<IEnumerable<AccountTypeViewModel>>("api/accounttype");
            return View(list ?? new List<AccountTypeViewModel>());
        }

        // GET: /AccountTypes/Create
        public IActionResult Create()
        {
            return View(new AccountTypeViewModel());
        }

        // POST: /AccountTypes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AccountTypeViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var ok = await _api.PostAsync("api/accounttype", model);
            if (!ok)
            {
                ModelState.AddModelError("", "Failed to create account type.");
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: /AccountTypes/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _api.GetAsync<AccountTypeViewModel>($"api/accounttype/{id}");
            if (model == null) return NotFound();
            return View(model);
        }

        // POST: /AccountTypes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AccountTypeViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var ok = await _api.PutAsync($"api/accounttype/{id}", model);
            if (!ok)
            {
                ModelState.AddModelError("", "Failed to update account type.");
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: /AccountTypes/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _api.GetAsync<AccountTypeViewModel>($"api/accounttype/{id}");
            if (model == null) return NotFound();
            return View(model);
        }

        // POST: /AccountTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ok = await _api.DeleteAsync($"api/accounttype/{id}");
            if (!ok) TempData["Error"] = "Delete failed.";
            return RedirectToAction(nameof(Index));
        }
    }
}
