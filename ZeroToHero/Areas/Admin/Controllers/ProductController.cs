using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing.Constraints;
using ZeroToHero.DataAccess.Data;
using ZeroToHero.DataAccess.Repository;
using ZeroToHero.DataAccess.Repository.IRepository;
using ZeroToHero.Models.Models;
using ZeroToHero.Models.ViewModel;

namespace ZeroToHero.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {

        private readonly IunitOfWorks _unitOfWorks;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IunitOfWorks unitOfWorks, IWebHostEnvironment webhostEnvironment)
        {

            _unitOfWorks = unitOfWorks;
            _webHostEnvironment = webhostEnvironment;
        }
        public IActionResult Index()
        {
            List<Product> objProductLists = _unitOfWorks.Product.GetAll(includeProperties:"Category").ToList();
            
            return View(objProductLists);
        }

        public IActionResult Upsert(int? id)
        {
            //IEnumerable<SelectListItem> CategoryList =

            //ViewBag.CategoryList = CategoryList;
            //ViewData["CategoryList"] = CategoryList;
            ProductViewModels productViewModels = new()
            {
                CategoryList = _unitOfWorks.Category
                .GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                }),
            Product = new Product()
            };

            if(id == null || id == 0)
            {

            return View(productViewModels);
            }
            else
            {
                //update
                productViewModels.Product = _unitOfWorks.Product.Get(u => u.Id == id);
                return View(productViewModels);

            }


        }
        [HttpPost]
        public IActionResult Upsert(ProductViewModels productViewModels, IFormFile? file)
        {
            //if (obj.Title == obj.Title.ToString())
            //{
            //    ModelState.AddModelError("Name", "Diplay order cannot exacly match the name");

            //}
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if(file != null)
                {
                    string filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product");

                    if(!string.IsNullOrEmpty(productViewModels.Product.ImageUrl)) 
                    {
                        //delete old image
                        var oldImagePath = 
                            Path.Combine(wwwRootPath + productViewModels.Product.ImageUrl.TrimStart('\\'));
                        
                        if(System.IO.File.Exists(oldImagePath)) 
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(productPath, filename), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    productViewModels.Product.ImageUrl = @"/images/product/" + filename;
                }

                if (productViewModels.Product.Id == 0)
                {
                    _unitOfWorks.Product.Add(productViewModels.Product);

                }
                else
                {
                    _unitOfWorks.Product.Update(productViewModels.Product);

                }

                //_unitOfWorks.Product.Add(productViewModels.Product);
                _unitOfWorks.Save();
                TempData["success"] = "Product create successfully";
                return RedirectToAction("Index");

            }
            else
            {
                productViewModels.CategoryList = _unitOfWorks.Category
                .GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                });
             return View(productViewModels);
            }

        }

        //public IActionResult Edit(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }

        //    var Product = _unitOfWorks.Product.Get(u => u.Id == id);

        //    if (Product == null)
        //    {
        //        return NotFound();

        //    }
        //    return View(Product);

        //}
        //[HttpPost]
        //public IActionResult Edit(Product obj)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        _unitOfWorks.Product.Update(obj);
        //        _unitOfWorks.Save();
        //        TempData["success"] = "Product update successfully";
        //        return RedirectToAction("Index");

        //    }
        //    return View();

        //}

        //public IActionResult Delete(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }

        //    var Product = _unitOfWorks.Product.Get(u => u.Id == id);

        //    if (Product == null)
        //    {
        //        return NotFound();

        //    }
        //    return View(Product);

        //}
        //[HttpPost, ActionName("Delete")]
        //public IActionResult DeletePost(int? id)
        //{


        //    Product? obji = _unitOfWorks.Product.Get(u => u.Id == id);
        //    if (obji == null)
        //    {
        //        return NotFound();
        //    }

        //    _unitOfWorks.Product.Remove(obji);
        //    _unitOfWorks.Save();
        //    TempData["success"] = "Product deleted successfully";
        //    return RedirectToAction("Index");


        //}

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> objProductList = _unitOfWorks.Product.GetAll(includeProperties: "Category").ToList();
            return Json(new { data = objProductList });
        }

       

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var productToBeDeleted = _unitOfWorks.Product.Get(u => u.Id == id);
            if (productToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            var oldImagePath =
                           Path.Combine(_webHostEnvironment.WebRootPath+
                           productToBeDeleted.ImageUrl.TrimStart('\\'));
            
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
           

            _unitOfWorks.Product.Remove(productToBeDeleted);
            _unitOfWorks.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }

        #endregion
    }
}
