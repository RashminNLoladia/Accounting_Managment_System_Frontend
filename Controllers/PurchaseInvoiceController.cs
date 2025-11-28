using Accounting_Managment_System_Frontend.Models;
using Accounting_Managment_System_Frontend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Accounting_Managment_System_Frontend.Controllers
{
    public class PurchaseInvoiceController : Controller
    {
        private readonly ApiService _api;

        public PurchaseInvoiceController(ApiService api) => _api = api;

        // GET: /PurchaseInvoice
        public async Task<IActionResult> Index()
        {
            var list = await _api.GetAsync<IEnumerable<PurchaseInvoiceViewModel>>("api/purchaseinvoice");
            return View(list ?? new List<PurchaseInvoiceViewModel>());
        }

        // GET: /PurchaseInvoice/Create
        public async Task<IActionResult> Create()
        {
            await PopulateDropdowns();
            return View(new PurchaseInvoiceViewModel { BillDate = DateTime.Now });
        }

        // POST: /PurchaseInvoice/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PurchaseInvoiceViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateDropdowns();
                return View(model);
            }

            var ok = await _api.PostAsync("api/purchaseinvoice", model);
            if (!ok)
            {
                ModelState.AddModelError("", "Failed to create invoice.");
                await PopulateDropdowns();
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: /PurchaseInvoice/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _api.GetAsync<PurchaseInvoiceViewModel>($"api/purchaseinvoice/{id}");
            if (model == null) return NotFound();

            await PopulateDropdowns();
            return View(model);
        }

        // POST: /PurchaseInvoice/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PurchaseInvoiceViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateDropdowns();
                return View(model);
            }

            var ok = await _api.PutAsync($"api/purchaseinvoice/{id}", model);
            if (!ok)
            {
                ModelState.AddModelError("", "Failed to update invoice.");
                await PopulateDropdowns();
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: /PurchaseInvoice/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _api.GetAsync<PurchaseInvoiceViewModel>($"api/purchaseinvoice/{id}");
            if (model == null) return NotFound();
            return View(model);
        }

        // POST: /PurchaseInvoice/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ok = await _api.DeleteAsync($"api/purchaseinvoice/{id}");
            if (!ok) TempData["Error"] = "Delete failed.";
            return RedirectToAction(nameof(Index));
        }

        // Populate dropdowns for company, vendor, currency, product, tax
        private async Task PopulateDropdowns()
        {
            ViewBag.Companies = await _api.GetAsync<IEnumerable<CompanyViewModel>>("api/company") ?? new List<CompanyViewModel>();
            ViewBag.Vendors = await _api.GetAsync<IEnumerable<VendorViewModel>>("api/vendor") ?? new List<VendorViewModel>();
            ViewBag.Currencies = await _api.GetAsync<IEnumerable<CurrencyViewModel>>("api/currency") ?? new List<CurrencyViewModel>();
            ViewBag.Products = await _api.GetAsync<IEnumerable<ProductViewModel>>("api/product") ?? new List<ProductViewModel>();
            ViewBag.TaxRates = await _api.GetAsync<IEnumerable<TaxRateViewModel>>("api/taxrate") ?? new List<TaxRateViewModel>();
        }
    }
}
