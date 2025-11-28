using Accounting_Managment_System_Frontend.Models;
using Accounting_Managment_System_Frontend.Services;
using Accounting_Managment_System_Frontend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Accounting_Managment_System_Frontend.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly ApiService _api;

        public PaymentsController(ApiService api)
        {
            _api = api;
        }

        // GET: /Payments
        public async Task<IActionResult> Index()
        {
            var list = await _api.GetAsync<IEnumerable<PaymentViewModel>>("api/payment");
            return View(list ?? new List<PaymentViewModel>());
        }

        // GET: /Payments/Create
        public async Task<IActionResult> Create()
        {
            await PopulateDropdowns();
            return View(new PaymentViewModel { PaymentDate = DateTime.Now });
        }

        // POST: /Payments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PaymentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateDropdowns();
                return View(model);
            }

            var ok = await _api.PostAsync("api/payment", model);
            if (!ok)
            {
                ModelState.AddModelError("", "Failed to create payment.");
                await PopulateDropdowns();
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: /Payments/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _api.GetAsync<PaymentViewModel>($"api/payment/{id}");
            if (model == null) return NotFound();

            await PopulateDropdowns();
            return View(model);
        }

        // POST: /Payments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PaymentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateDropdowns();
                return View(model);
            }

            var ok = await _api.PutAsync($"api/payment/{id}", model);
            if (!ok)
            {
                ModelState.AddModelError("", "Failed to update payment.");
                await PopulateDropdowns();
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: /Payments/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _api.GetAsync<PaymentViewModel>($"api/payment/{id}");
            if (model == null) return NotFound();
            return View(model);
        }

        // POST: /Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ok = await _api.DeleteAsync($"api/payment/{id}");
            if (!ok) TempData["Error"] = "Delete failed.";
            return RedirectToAction(nameof(Index));
        }

        // Helper: load dropdowns for Company, Invoice, Bill, BankAccount
        private async Task PopulateDropdowns()
        {
            var companies = await _api.GetAsync<IEnumerable<CompanyViewModel>>("api/company");
            var invoices = await _api.GetAsync<IEnumerable<SalesInvoiceViewModel>>("api/salesinvoice");
            var bills = await _api.GetAsync<IEnumerable<PurchaseInvoiceViewModel>>("api/purchaseinvoice");
            var banks = await _api.GetAsync<IEnumerable<BankAccountViewModel>>("api/bankaccount");

            ViewBag.Companies = companies ?? new List<CompanyViewModel>();
            ViewBag.Invoices = invoices ?? new List<SalesInvoiceViewModel>();
            ViewBag.Bills = bills ?? new List<PurchaseInvoiceViewModel>();
            ViewBag.Banks = banks ?? new List<BankAccountViewModel>();
        }
    }
}
