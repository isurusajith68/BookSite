using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ZeroToHero.DataAccess.Data;
using ZeroToHero.DataAccess.Repository;
using ZeroToHero.DataAccess.Repository.IRepository;
using ZeroToHero.Models.Models;

namespace ZeroToHero.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {

        private readonly IunitOfWorks _unitOfWorks;
        public ProductController(IunitOfWorks unitOfWorks)
        {

            _unitOfWorks = unitOfWorks;

        }
        public IActionResult Index()
        {
            List<Product> objProductLists = _unitOfWorks.Product.GetAll().ToList();
            
            return View(objProductLists);
        }

        public IActionResult Create()
        {
            IEnumerable<SelectListItem> CategoryList = _unitOfWorks.Category
                .GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                });

            ViewBag.CategoryList = CategoryList;
            //ViewData["CategoryList"] = CategoryList;
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product obj)
        {
            //if (obj.Title == obj.Title.ToString())
            //{
            //    ModelState.AddModelError("Name", "Diplay order cannot exacly match the name");

            //}
            if (ModelState.IsValid)
            {
                _unitOfWorks.Product.Add(obj);
                _unitOfWorks.Save();
                TempData["success"] = "Product create successfully";
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

            var Product = _unitOfWorks.Product.Get(u => u.Id == id);

            if (Product == null)
            {
                return NotFound();

            }
            return View(Product);

        }
        [HttpPost]
        public IActionResult Edit(Product obj)
        {

            if (ModelState.IsValid)
            {
                _unitOfWorks.Product.Update(obj);
                _unitOfWorks.Save();
                TempData["success"] = "Product update successfully";
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

            var Product = _unitOfWorks.Product.Get(u => u.Id == id);

            if (Product == null)
            {
                return NotFound();

            }
            return View(Product);

        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {


            Product? obji = _unitOfWorks.Product.Get(u => u.Id == id);
            if (obji == null)
            {
                return NotFound();
            }

            _unitOfWorks.Product.Remove(obji);
            _unitOfWorks.Save();
            TempData["success"] = "Product deleted successfully";
            return RedirectToAction("Index");


        }


    }
}
