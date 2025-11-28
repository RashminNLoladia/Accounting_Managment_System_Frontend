using Accounting_Managment_System_Frontend.Models;
using Accounting_Managment_System_Frontend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Accounting_Managment_System_Frontend.Controllers
{
    public class UserRolesController : Controller
    {
        private readonly ApiService _api;

        public UserRolesController(ApiService api)
        {
            _api = api;
        }

        // GET: /UserRoles
        public async Task<IActionResult> Index()
        {
            var list = await _api.GetAsync<IEnumerable<UserRoleViewModel>>("api/userrole");
            return View(list ?? new List<UserRoleViewModel>());
        }

        // GET: /UserRoles/Create
        public async Task<IActionResult> Create()
        {
            await PopulateDropdowns();
            return View(new UserRoleViewModel());
        }

        // POST: /UserRoles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserRoleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateDropdowns();
                return View(model);
            }

            var ok = await _api.PostAsync("api/userrole", model);
            if (!ok)
            {
                ModelState.AddModelError("", "Failed to assign role.");
                await PopulateDropdowns();
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: /UserRoles/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _api.GetAsync<UserRoleViewModel>($"api/userrole/{id}");
            if (model == null) return NotFound();

            await PopulateDropdowns();
            return View(model);
        }

        // POST: /UserRoles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UserRoleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateDropdowns();
                return View(model);
            }

            var ok = await _api.PutAsync($"api/userrole/{id}", model);
            if (!ok)
            {
                ModelState.AddModelError("", "Failed to update role assignment.");
                await PopulateDropdowns();
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: /UserRoles/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _api.GetAsync<UserRoleViewModel>($"api/userrole/{id}");
            if (model == null) return NotFound();
            return View(model);
        }

        // POST: /UserRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ok = await _api.DeleteAsync($"api/userrole/{id}");
            if (!ok) TempData["Error"] = "Delete failed.";
            return RedirectToAction(nameof(Index));
        }

        // Helper: load dropdowns for Users and Roles
        private async Task PopulateDropdowns()
        {
            var users = await _api.GetAsync<IEnumerable<UserViewModel>>("api/user");
            var roles = await _api.GetAsync<IEnumerable<RoleViewModel>>("api/role");

            ViewBag.Users = users ?? new List<UserViewModel>();
            ViewBag.Roles = roles ?? new List<RoleViewModel>();
        }
    }
}
