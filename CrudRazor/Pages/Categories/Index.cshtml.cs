using CrudRazor.Data;
using CrudRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CrudRazor.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly DBContext _dbContext;
        public List<Category> CategoryList { get; set; }

        public IndexModel(DBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void OnGet()
        {
            CategoryList = _dbContext.Categories.ToList();
        }
    }
}
