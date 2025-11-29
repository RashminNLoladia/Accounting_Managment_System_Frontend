using Microsoft.AspNetCore.Mvc;
using Accounting_Managment_System_Frontend.Services;
using Accounting_Managment_System_Frontend.Models;
using System.Text.Json.Serialization;

namespace Accounting_Managment_System_Frontend.Controllers
{
    public class SalesInvoiceController : Controller
    {
        private readonly ApiService _api;

        public SalesInvoiceController(ApiService api)
        {
            _api = api;
        }

        // GET: /SalesInvoice
        public async Task<IActionResult> Index()
        {
            // Ensure API route matches backend
            var invoices = await _api.GetAsync<List<SalesInvoiceViewModel>>("api/SalesInvoice");
            return View(invoices ?? new List<SalesInvoiceViewModel>());
        }

        // GET: Create
        public async Task<IActionResult> Create()
        {
            await PopulateCustomersAndProducts();

            // Initialize one empty item row for the form
            var model = new SalesInvoiceViewModel();
            model.Items.Add(new SalesInvoiceItemViewModel());
            return View(model);
        }

        // POST: Create
        [HttpPost]
        public async Task<IActionResult> Create(SalesInvoiceViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateCustomersAndProducts();
                return View(model);
            }

            // Remove invalid items and map to backend DTO
            var items = model.Items
                .Where(i => i.ProductId.HasValue && i.Quantity > 0)
                .Select(i => new SalesInvoiceItemViewModel
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice,
                    LineTotal = i.Quantity * i.UnitPrice,
                    Description = i.Description,
                    TaxRateId = i.TaxRateId
                }).ToList();

            if (!items.Any())
            {
                ModelState.AddModelError("", "Add at least one product with quantity > 0.");
                await PopulateCustomersAndProducts();
                return View(model);
            }

            // Map SalesInvoiceViewModel to InvoiceDto
            var invoiceDto = new SalesInvoiceViewModel
            {
                CompanyId = model.CompanyId,
                CurrencyId = model.CurrencyId,
                CustomerId = model.CustomerId,
                InvoiceDate = model.InvoiceDate,
                Reference = model.Reference,
                TaxTotal = model.TaxTotal,
                SubTotal = items.Sum(i => i.LineTotal),
                Total = items.Sum(i => i.LineTotal) + model.TaxTotal,
                Items = items
            };

            // Call backend API to save invoice
            var ok = await _api.PostAsync("api/SalesInvoice", invoiceDto);

            if (!ok)
            {
                ModelState.AddModelError("", "Failed to create invoice.");
                await PopulateCustomersAndProducts();
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }




        // GET: /SalesInvoice/Edit/{id}
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _api.GetAsync<SalesInvoiceViewModel>($"api/SalesInvoice/{id}");
            if (model == null) return NotFound();

            // Ensure Items list is initialized
            model.Items = model.Items ?? new List<SalesInvoiceItemViewModel>();

            await PopulateCustomersAndProducts();
            return View(model);
        }

        // POST: /SalesInvoice/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(SalesInvoiceViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateCustomersAndProducts();
                return View(model);
            }

            model.Items = model.Items?.Where(i => i.ProductId.HasValue && i.Quantity > 0).ToList() ?? new List<SalesInvoiceItemViewModel>();

            var ok = await _api.PutAsync($"api/SalesInvoice/{model.InvoiceId}", model);
            if (!ok)
            {
                ModelState.AddModelError("", "Failed to update invoice.");
                await PopulateCustomersAndProducts();
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: /SalesInvoice/Delete/{id}
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _api.GetAsync<SalesInvoiceViewModel>($"api/SalesInvoice/{id}");
            if (model == null) return NotFound();
            return View(model);
        }

        // POST: /SalesInvoice/Delete/{id}
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _api.DeleteAsync($"api/SalesInvoice/{id}");
            return RedirectToAction(nameof(Index));
        }

        // Populate dropdowns
        private async Task PopulateCustomersAndProducts()
        {
            var customers = await _api.GetAsync<List<CustomerViewModel>>("api/Customer");
            ViewBag.Customers = customers ?? new List<CustomerViewModel>();

            var products = await _api.GetAsync<List<ProductViewModel>>("api/Product");
            ViewBag.Products = products ?? new List<ProductViewModel>();
        }
    }

    
}
