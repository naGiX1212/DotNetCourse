using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        public readonly IUnitOfWork _UnitOfWork;
        public CoverTypeController(IUnitOfWork UnitOfWork)
        {
            _UnitOfWork = UnitOfWork;
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
                return NotFound();
            var coverTypeFromDb = _UnitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
            if (coverTypeFromDb == null)
                return NotFound();
            return View(coverTypeFromDb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _UnitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _UnitOfWork.CoverType.Remove(obj);
            _UnitOfWork.Save();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }
        public IActionResult Index()
        {
            IEnumerable<CoverType> objCoverTypeList = _UnitOfWork.CoverType.GetAll();
            return View(objCoverTypeList);
        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            var coverTypeFromDb = _UnitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
            if (coverTypeFromDb == null)
                return NotFound();
            return View(coverTypeFromDb);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CoverType obj)
        {
            if (ModelState.IsValid)
            {
                _UnitOfWork.CoverType.Update(obj);
                _UnitOfWork.Save();
                TempData["success"] = "Category edited successfully";
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
        public IActionResult Create(CoverType obj)
        {
            if (ModelState.IsValid)
            {
                _UnitOfWork.CoverType.Add(obj);
                _UnitOfWork.Save();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");

            }
            return View(obj);


        }
    }
}
