using DataAccess.EShop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EShop.IServices
{
    public interface IProductRepository
    {
        Task<List<Product>> GetListProduct(GetProduct requestData);
        Task<Product> GetProductById (int id);
        Task<ReturnData> Product_InsertUpdate (Product product);
        Task<ReturnData> Product_Delete(ProductDeleteRequestData requestData);
    }
}
