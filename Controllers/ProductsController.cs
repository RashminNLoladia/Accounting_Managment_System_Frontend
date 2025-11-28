using Accounting_Managment_System_Frontend.Models;
using Accounting_Managment_System_Frontend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Accounting_Managment_System_Frontend.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApiService _api;

        public ProductsController(ApiService api)
        {
            _api = api;
        }

        // GET: /Products
        public async Task<IActionResult> Index()
        {
            var list = await _api.GetAsync<IEnumerable<ProductViewModel>>("api/product");
            return View(list ?? new List<ProductViewModel>());
        }

        // GET: /Products/Create
        public async Task<IActionResult> Create()
        {
            await PopulateCompaniesDropDown();
            await PopulateAccountsDropDown();
            return View(new ProductViewModel { IsActive = true });
        }

        // POST: /Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateCompaniesDropDown();
                await PopulateAccountsDropDown();
                return View(model);
            }

            var ok = await _api.PostAsync("api/product", model);
            if (!ok)
            {
                ModelState.AddModelError("", "Failed to create product. Check API or server logs.");
                await PopulateCompaniesDropDown();
                await PopulateAccountsDropDown();
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: /Products/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _api.GetAsync<ProductViewModel>($"api/product/{id}");
            if (model == null) return NotFound();

            await PopulateCompaniesDropDown();
            await PopulateAccountsDropDown();
            return View(model);
        }

        // POST: /Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateCompaniesDropDown();
                await PopulateAccountsDropDown();
                return View(model);
            }

            var ok = await _api.PutAsync($"api/product/{id}", model);
            if (!ok)
            {
                ModelState.AddModelError("", "Failed to update product.");
                await PopulateCompaniesDropDown();
                await PopulateAccountsDropDown();
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: /Products/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _api.GetAsync<ProductViewModel>($"api/product/{id}");
            if (model == null) return NotFound();
            return View(model);
        }

        // POST: /Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ok = await _api.DeleteAsync($"api/product/{id}");
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

        // helper to load chart of accounts for dropdowns
        private async Task PopulateAccountsDropDown()
        {
            var accounts = await _api.GetAsync<IEnumerable<ChartOfAccountViewModel>>("api/chartofaccount");
            ViewBag.Accounts = accounts ?? new List<ChartOfAccountViewModel>();
        }
    }
}
