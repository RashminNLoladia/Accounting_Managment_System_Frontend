using Microsoft.AspNetCore.Mvc;
using Accounting_Managment_System_Frontend.Services;
using Accounting_Managment_System_Frontend.Models;

namespace Accounting_Managment_System_Frontend.Controllers
{
    public class SalesInvoiceController : Controller
    {
        private readonly ApiService _api;

        public SalesInvoiceController(ApiService api)
        {
            _api = api;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _api.GetAsync<List<SalesInvoiceViewModel>>("SalesInvoice");
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            await PopulateCustomersAndProducts();
            return View(new SalesInvoiceViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(SalesInvoiceViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateCustomersAndProducts();
                return View(model);
            }

            var ok = await _api.PostAsync("SalesInvoice", model);
            if (!ok)
            {
                ModelState.AddModelError("", "Failed to create invoice.");
                await PopulateCustomersAndProducts();
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = await _api.GetAsync<SalesInvoiceViewModel>($"SalesInvoice/{id}");
            if (model == null) return NotFound();

            await PopulateCustomersAndProducts();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SalesInvoiceViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateCustomersAndProducts();
                return View(model);
            }

            var ok = await _api.PutAsync($"SalesInvoice/{model.Id}", model);
            if (!ok)
            {
                ModelState.AddModelError("", "Failed to update invoice.");
                await PopulateCustomersAndProducts();
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var model = await _api.GetAsync<SalesInvoiceViewModel>($"SalesInvoice/{id}");
            if (model == null) return NotFound();
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _api.DeleteAsync($"SalesInvoice/{id}");
            return RedirectToAction(nameof(Index));
        }

        private async Task PopulateCustomersAndProducts()
        {
            var customers = await _api.GetAsync<List<CustomerViewModel>>("Customer");
            ViewBag.Customers = customers ?? new List<CustomerViewModel>();

            var products = await _api.GetAsync<List<ProductViewModel>>("Product");
            ViewBag.Products = products ?? new List<ProductViewModel>();
        }
    }
}
