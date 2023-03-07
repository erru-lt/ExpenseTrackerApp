using ExpenseTrackerApp.Models;
using ExpenseTrackerApp.Services.CategoryService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var result = await _categoryService.GetAllCategoriesAsync();
            return View(result);
        }

        public IActionResult Create()
        {
            var category = new Category();
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(nameof(Create), category);
            }

            await _categoryService.AddNewCategoryAsync(category);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);

            if (category == null)
            {
                return View(nameof(NotFound));
            }

            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Category category)
        {
            if (id != category.Id)
            {
                return View(nameof(NotFound));
            }

            if (!ModelState.IsValid)
            {
                return View(category);
            }

            await _categoryService.UpdateCategoryAsync(category);
            return RedirectToAction(nameof(View));
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
