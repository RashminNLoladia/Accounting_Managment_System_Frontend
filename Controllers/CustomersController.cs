using Accounting_Managment_System_Frontend.Models;
using Accounting_Managment_System_Frontend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Accounting_Managment_System_Frontend.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ApiService _api;

        public CustomersController(ApiService api)
        {
            _api = api;
        }

        // GET: /Customers
        public async Task<IActionResult> Index()
        {
            var list = await _api.GetAsync<IEnumerable<CustomerViewModel>>("api/customer");
            return View(list ?? new List<CustomerViewModel>());
        }

        // GET: /Customers/Create
        public async Task<IActionResult> Create()
        {
            await PopulateCompaniesDropDown();
            return View(new CustomerViewModel { IsActive = true });
        }

        // POST: /Customers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CustomerViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateCompaniesDropDown();
                return View(model);
            }

            var ok = await _api.PostAsync("api/customer", model);
            if (!ok)
            {
                ModelState.AddModelError("", "Failed to create customer. Check API or server logs.");
                await PopulateCompaniesDropDown();
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: /Customers/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _api.GetAsync<CustomerViewModel>($"api/customer/{id}");
            if (model == null) return NotFound();

            await PopulateCompaniesDropDown();
            return View(model);
        }

        // POST: /Customers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CustomerViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateCompaniesDropDown();
                return View(model);
            }

            var ok = await _api.PutAsync($"api/customer/{id}", model);
            if (!ok)
            {
                ModelState.AddModelError("", "Failed to update customer.");
                await PopulateCompaniesDropDown();
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: /Customers/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _api.GetAsync<CustomerViewModel>($"api/customer/{id}");
            if (model == null) return NotFound();
            return View(model);
        }

        // POST: /Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ok = await _api.DeleteAsync($"api/customer/{id}");
            if (!ok)
            {
                TempData["Error"] = "Delete failed.";
            }

            return RedirectToAction(nameof(Index));
        }

        // helper to load companies for dropdown
        private async Task PopulateCompaniesDropDown()
        {
            var companies = await _api.GetAsync<IEnumerable<CompanyViewModel>>("api/company");
            ViewBag.Companies = companies ?? new List<CompanyViewModel>();
        }
    }
}
