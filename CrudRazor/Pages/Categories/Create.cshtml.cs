using CrudRazor.Data;
using CrudRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CrudRazor.Pages.Categories
{
    public class CreateModel : PageModel
    {
        private readonly DBContext _dbContext;
        public Category Categyory { get; set; }

        public CreateModel(DBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult OnPost(Category obj)
        {
            _dbContext.Add(obj);

            _dbContext.SaveChanges();
            return RedirectToAction("Index");
            
        }
    }
}
