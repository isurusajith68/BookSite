using Microsoft.AspNetCore.Mvc;
using ZeroToHero.DataAccess.Data;
using ZeroToHero.Models.Models;

namespace ZeroToHero.Controllers
{
    public class CategoryController : Controller
    {

        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db) {
            
            _db = db;
        
        }
        public IActionResult Index()
        {
            List<Category> objCategoryLists = _db.Categories.ToList();

            return View(objCategoryLists);
        }

        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if(obj.Name == obj.DisplayOrder.ToString()) 
            {
                ModelState.AddModelError("Name", "Diplay order cannot exacly match the name");
            
            }
            if(ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
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

            var catogoty = _db.Categories.Find(id);

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
                _db.Categories.Update(obj);
                _db.SaveChanges();
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

            var catogoty = _db.Categories.Find(id);

            if (catogoty == null)
            {
                return NotFound();

            }
            return View(catogoty);

        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {


            Category? obji = _db.Categories.Find(id);
            if (obji == null)
            {
                return NotFound();
            }

            _db.Categories.Remove(obji);
            _db.SaveChanges();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");


        }


    }
}
