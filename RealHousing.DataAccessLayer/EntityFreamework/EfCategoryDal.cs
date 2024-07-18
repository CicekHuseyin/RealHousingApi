using RealHousing.DataAccessLayer.Abstract;
using RealHousing.DataAccessLayer.Repository;
using RealHousing.EntityLayer.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealHousing.DataAccessLayer.EntityFreamework
{
    public class EfCategoryDal : GenericRepository<Category>,ICategoryDal
    {
    }
}
