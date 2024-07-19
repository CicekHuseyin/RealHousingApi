using Microsoft.EntityFrameworkCore;
using RealHousing.DataAccessLayer.Abstract;
using RealHousing.DataAccessLayer.Concreate;
using RealHousing.DataAccessLayer.Repository;
using RealHousing.EntityLayer.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealHousing.DataAccessLayer.EntityFreamework
{
    public class EfProductDal : GenericRepository<Product>, IProductDal
    {
        public List<Product> GetProductsWithCategories()
        {
            var context = new Context();
            var values = context.Products.Include(x => x.Category).ToList();
            return values;
        }
    }
}
