using DataAccess.EShop.Entities;
using DataAccess.EShop.EntitiesFramework;
using DataAccess.EShop.IServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EShop.Services
{
    public class ProductRepository : IProductRepository
    {
        private EShopDBContext dBContext;
        public ProductRepository(EShopDBContext dBContext)
        {
            this.dBContext = dBContext;
        }
        public async Task<List<Product>> GetListProduct(GetProduct requestData)
        {
            var list = new List<Product>();
            try
            {
                list = dBContext.Product.ToList();
                if(!string.IsNullOrEmpty(requestData.ProductName))
                {
                    list.FindAll(s=>s.ProductName.Contains(requestData.ProductName)).ToList();
                }
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Product> GetProductById(int id)
        {
            var model = new Product();
            try
            {
                model = dBContext.Product.Where(s=>s.ProductId == id).FirstOrDefault();
                return model;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ReturnData> Product_Delete(ProductDeleteRequestData requestData)
        {
           var returnData = new ReturnData();
            try
            {
                if (requestData == null && requestData.Id == 0)
                {
                    returnData.returnCode = -1;
                    returnData.returnMessage = "Dữ liệu đầu vào ko hợp lệ";
                    return returnData;
                }
                var product = dBContext.Product.Where(s => s.ProductId == requestData.Id).FirstOrDefault();
                if (product == null || product.ProductId <= 0)
                {
                    returnData.returnCode = -2;
                    returnData.returnMessage = "Không tồn tại sản phẩm";
                    return returnData;
                }

                dBContext.Product.Remove(product);
                var result = dBContext.SaveChanges();

                if (result < 0)
                {
                    returnData.returnCode = -3;
                    returnData.returnMessage = "Xóa sản phẩm thất bại";
                    return returnData;
                }

                returnData.returnCode = 1;
                returnData.returnMessage = "Xóa sản phẩm thành công";
                return returnData;

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<ReturnData> Product_InsertUpdate(Product product)
        {
            var returnData = new ReturnData();
            try
            {
                if (product.ProductId <= 0)
                {
                    dBContext.Product.Add(product);
                    dBContext.SaveChanges();
                    returnData.returnCode = 1;
                    returnData.returnMessage = "Thêm sản phẩm thành công";
                    return returnData;
                }
                else
                {
                    dBContext.Product.Update(product);
                    dBContext.SaveChanges();
                    returnData.returnCode = 1;
                    returnData.returnMessage = "Sửa sản phẩm thành công";
                    return returnData;
                }

            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
