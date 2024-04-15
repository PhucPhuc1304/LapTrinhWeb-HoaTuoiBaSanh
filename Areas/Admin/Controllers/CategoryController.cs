using CF_HOATUOIBASANH.Authencation;
using CF_HOATUOIBASANH.Interface;
using CF_HOATUOIBASANH.Models;
using Microsoft.AspNetCore.Mvc;

namespace CF_HOATUOIBASANH.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[CustomAuthorize(Roles = "Admin")]

    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public IActionResult Index()
        {
            var category = _categoryRepository.GetAll();
            return View(category);
        }
        public IActionResult Add()
        {
            return View();
        }
        public IActionResult Edit(int id)
        {
            var editCategory = _categoryRepository.GetById(id);
            return View(editCategory);
        }
        public IActionResult Delete(int id)
        {
            var removeCategory = _categoryRepository.GetById(id);
            _categoryRepository.Remove(removeCategory);
            return RedirectToAction("Index", "Category");
        }
        [HttpPost]
        public IActionResult EditCategory(int categoryId, string categoryName)
        {
            var existingCategory = _categoryRepository.GetById(categoryId);

            if (existingCategory == null)
            {
                return NotFound(); 
            }

            // Update the category name
            existingCategory.CategoryName = categoryName;

            // Save changes to the database
            _categoryRepository.Update(existingCategory);

            return RedirectToAction("Index", "Category"); // Redirect to the category list page
        }
        [HttpPost]
        public IActionResult AddCategory(string categoryName)
        {

            var newCategory = new Category()
            {
                CategoryName = categoryName
            };
            _categoryRepository.Add(newCategory);

            return RedirectToAction("Index", "Category");

        }
    }
}
