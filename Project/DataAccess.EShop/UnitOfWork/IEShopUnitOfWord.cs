using DataAccess.EShop.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EShop.UnitOfWork
{
    public interface IEShopUnitOfWord
    {
        public IProductRepository _productRepository { get; set; }
        void SaveChange();
    }
}
