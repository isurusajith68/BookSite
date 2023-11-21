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
    public class ProductRepository : Repository<Product>, IProductRepository
    {

        private ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db): base(db) { 
        
        _db = db;
        }
        
  

        public void Update(Product obj)
        {
           _db.Product.Update(obj);
        }
    }
}
