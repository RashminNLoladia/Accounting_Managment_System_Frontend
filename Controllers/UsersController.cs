using Accounting_Managment_System_Frontend.Models;
using Accounting_Managment_System_Frontend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Accounting_Managment_System_Frontend.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApiService _api;

        public UsersController(ApiService api)
        {
            _api = api;
        }

        // GET: /Users
        public async Task<IActionResult> Index()
        {
            var list = await _api.GetAsync<IEnumerable<UserViewModel>>("api/user");
            return View(list ?? new List<UserViewModel>());
        }

        // GET: /Users/Create
        public IActionResult Create()
        {
            return View(new UserViewModel());
        }

        // POST: /Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var ok = await _api.PostAsync("api/user", model);
            if (!ok)
            {
                ModelState.AddModelError("", "Failed to create user.");
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: /Users/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _api.GetAsync<UserViewModel>($"api/user/{id}");
            if (model == null) return NotFound();
            return View(model);
        }

        // POST: /Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var ok = await _api.PutAsync($"api/user/{id}", model);
            if (!ok)
            {
                ModelState.AddModelError("", "Failed to update user.");
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: /Users/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _api.GetAsync<UserViewModel>($"api/user/{id}");
            if (model == null) return NotFound();
            return View(model);
        }

        // POST: /Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ok = await _api.DeleteAsync($"api/user/{id}");
            if (!ok) TempData["Error"] = "Delete failed.";
            return RedirectToAction(nameof(Index));
        }
    }
}
