using DataAccess.EShop.EntitiesFramework;
using DataAccess.EShop.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EShop.UnitOfWork
{
    public class EShopUnitOfWord : IEShopUnitOfWord
    {
        public IProductRepository _productRepository { get; set; }

        public EShopDBContext _eShopDbContext;
        public EShopUnitOfWord(IProductRepository productRepository,EShopDBContext eShopDbContext)
        {
            _productRepository = productRepository;
            _eShopDbContext = eShopDbContext;
        }

        public void SaveChange()
        {
            _eShopDbContext.SaveChanges();
        }
        public void Dispose()
        {
            _eShopDbContext.Dispose();
        }
    }
}
