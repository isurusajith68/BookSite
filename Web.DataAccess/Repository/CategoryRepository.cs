using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ZeroToHero.DataAccess.Data;
using ZeroToHero.Models.Models;

namespace ZeroToHero.DataAccess.Repository.IRepository
{
    public class CategoryRepository :Repository<Category>, ICategoryRepository
    {

        private ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db): base(db) { 
        
        _db = db;
        }
        
  

        public void Update(Category obj)
        {
           _db.Categories.Update(obj);
        }
    }
}
