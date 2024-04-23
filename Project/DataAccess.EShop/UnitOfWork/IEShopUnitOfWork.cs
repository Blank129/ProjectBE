using DataAccess.Blog.IServices;
using DataAccess.EShop.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EShop.UnitOfWork
{
    public interface IEShopUnitOfWork
    {
        public IProductRepository _productRepository { get; set; }
        public IUserRepository _userRepository { get; set; }

        public IRoleRepository _roleRepository { get; set; }
        void SaveChange();
    }
}
