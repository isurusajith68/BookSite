using Microsoft.AspNetCore.Mvc;
using ZeroToHero.DataAccess.Data;
using ZeroToHero.DataAccess.Repository;
using ZeroToHero.DataAccess.Repository.IRepository;
using ZeroToHero.Models.Models;

namespace ZeroToHero.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {

        private readonly IunitOfWorks _unitOfWorks;
        public CategoryController(IunitOfWorks unitOfWorks)
        {

            _unitOfWorks = unitOfWorks;

        }
        public IActionResult Index()
        {
            List<Category> objCategoryLists = _unitOfWorks.Category.GetAll().ToList();

            return View(objCategoryLists);
        }

        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Diplay order cannot exacly match the name");

            }
            if (ModelState.IsValid)
            {
                _unitOfWorks.Category.Add(obj);
                _unitOfWorks.Save();
                TempData["success"] = "Category create successfully";
                return RedirectToAction("Index");

            }
            return View();

        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var catogoty = _unitOfWorks.Category.Get(u => u.Id == id);

            if (catogoty == null)
            {
                return NotFound();

            }
            return View(catogoty);

        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {

            if (ModelState.IsValid)
            {
                _unitOfWorks.Category.Update(obj);
                _unitOfWorks.Save();
                TempData["success"] = "Category update successfully";
                return RedirectToAction("Index");

            }
            return View();

        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var catogoty = _unitOfWorks.Category.Get(u => u.Id == id);

            if (catogoty == null)
            {
                return NotFound();

            }
            return View(catogoty);

        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {


            Category? obji = _unitOfWorks.Category.Get(u => u.Id == id);
            if (obji == null)
            {
                return NotFound();
            }

            _unitOfWorks.Category.Remove(obji);
            _unitOfWorks.Save();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");


        }


    }
}
