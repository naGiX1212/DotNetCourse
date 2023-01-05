using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {


        public readonly IUnitOfWork _UnitOfWork;
        public CategoryController(IUnitOfWork UnitOfWork)
        {
            _UnitOfWork = UnitOfWork;
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
                return NotFound();
            var categoryFromDb = _UnitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            if (categoryFromDb == null)
                return NotFound();
            return View(categoryFromDb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _UnitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _UnitOfWork.Category.Remove(obj);
            _UnitOfWork.Save();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }
        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _UnitOfWork.Category.GetAll();
            return View(objCategoryList);
        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            var categoryFromDbFirst = _UnitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            if (categoryFromDbFirst == null)
                return NotFound();
            return View(categoryFromDbFirst);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The Display Order Cannot Match Name");
            }
            if (ModelState.IsValid)
            {
                _UnitOfWork.Category.Update(obj);
                _UnitOfWork.Save();
                TempData["success"] = "Category Edited successfully";
                return RedirectToAction("Index");

            }
            return View(obj);


        }
        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The Display Order Cannot Match Name");
            }
            if (ModelState.IsValid)
            {
                _UnitOfWork.Category.Add(obj);
                _UnitOfWork.Save();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");

            }
            return View(obj);


        }

    }
}
