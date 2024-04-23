using DataAccess.Blog.IServices;
using DataAccess.EShop.EntitiesFramework;
using DataAccess.EShop.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EShop.UnitOfWork
{
    public class EShopUnitOfWork : IEShopUnitOfWork
    {
        public IProductRepository _productRepository { get; set; }
        public IUserRepository _userRepository { get; set; }
        public IRoleRepository _roleRepository { get; set; }

        public EShopDBContext _eShopDbContext;
        public EShopUnitOfWork(IProductRepository productRepository, IUserRepository userRepository, IRoleRepository roleRepository, EShopDBContext eShopDbContext)
        {
            _productRepository = productRepository;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
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
