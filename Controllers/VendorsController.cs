using Accounting_Managment_System_Frontend.Models;
using Accounting_Managment_System_Frontend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Accounting_Managment_System_Frontend.Controllers
{
    public class VendorsController : Controller
    {
        private readonly ApiService _api;

        public VendorsController(ApiService api)
        {
            _api = api;
        }

        // GET: /Vendors
        public async Task<IActionResult> Index()
        {
            var list = await _api.GetAsync<IEnumerable<VendorViewModel>>("api/vendor");
            return View(list ?? new List<VendorViewModel>());
        }

        // GET: /Vendors/Create
        public async Task<IActionResult> Create()
        {
            await PopulateCompaniesDropDown();
            return View(new VendorViewModel { IsActive = true });
        }

        // POST: /Vendors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VendorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateCompaniesDropDown();
                return View(model);
            }

            var ok = await _api.PostAsync("api/vendor", model);
            if (!ok)
            {
                ModelState.AddModelError("", "Failed to create vendor. Check API or server logs.");
                await PopulateCompaniesDropDown();
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: /Vendors/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _api.GetAsync<VendorViewModel>($"api/vendor/{id}");
            if (model == null) return NotFound();

            await PopulateCompaniesDropDown();
            return View(model);
        }

        // POST: /Vendors/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, VendorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateCompaniesDropDown();
                return View(model);
            }

            var ok = await _api.PutAsync($"api/vendor/{id}", model);
            if (!ok)
            {
                ModelState.AddModelError("", "Failed to update vendor.");
                await PopulateCompaniesDropDown();
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: /Vendors/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _api.GetAsync<VendorViewModel>($"api/vendor/{id}");
            if (model == null) return NotFound();
            return View(model);
        }

        // POST: /Vendors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ok = await _api.DeleteAsync($"api/vendor/{id}");
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
