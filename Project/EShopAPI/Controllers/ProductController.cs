using DataAccess.EShop.Entities;
using DataAccess.EShop.UnitOfWork;
using EShop.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EShopAPIClone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public IEShopUnitOfWord _unitOfWork;
        private IConfiguration _configuration;
        public ProductController(IEShopUnitOfWord eShopUnitOfWord, IConfiguration configuration)
        {
            _unitOfWork = eShopUnitOfWord;
            _configuration = configuration;
        }

        [HttpPost("GetListProduct")]
        public async Task<ActionResult> GetListProduct(GetProduct requestData)
        {
            var list = new List<Product>();
            list = await _unitOfWork._productRepository.GetListProduct(requestData);
            return Ok(list);
        }
        [HttpPost("Product_Delete")]
        public async Task<ActionResult> Product_Delete(ProductDeleteRequestData requestData)
        {
            var returnData = new ReturnData();
            returnData = await _unitOfWork._productRepository.Product_Delete(requestData);
            return Ok(returnData);
        }
        [HttpPost("Product_InsertUpdate")]
        public async Task<ActionResult> Product_InsertUpdate(Product product)
        {
            var result = new ReturnData();
            try
            {
                //gọi Media để lấy ảnh từ base 64
                var media_url = _configuration["MEDIA:URL"] ?? "http://localhost:55587/";
                var base_url = "api/UploadImage/SaveImage_Data";
                var returnData = new ReturnData();
                if (!string.IsNullOrEmpty(product.Base64Image))
                {
                    //kiểm tra xem chữ ký có hợp lệ ko
                    var SecretKey = _configuration["SecretKey:IMAGE_DOWN_UPLOAD"] ?? "";
                    var plantext = product.Base64Image + SecretKey;
                    var Sign = EShop.Common.Security.MD5(plantext);

                    var req = new SaveImage_DataRequestData
                    {
                        Base64Image = product.Base64Image,
                        Sign = Sign,

                    };

                    var jsonBody = JsonConvert.SerializeObject(req);

                    var result_media = await EShop.Common.HttpRequestHelper.SendHttpClient(media_url, base_url, jsonBody);
                    returnData = JsonConvert.DeserializeObject<ReturnData>(result_media);

                    if (returnData.returnCode < 0)
                    {
                        result.returnCode = -1;
                        result.returnMessage = returnData.returnMessage;
                        return Ok(result);
                    }
                }
                //gán lại tên ảnh vào Base64Image
                product.Base64Image = returnData.returnMessage;

                result = await _unitOfWork._productRepository.Product_InsertUpdate(product);
            }
            catch (Exception)
            {

                throw;
            }


            return Ok(result);
            //var returnData = new ReturnData();
            //returnData = await _unitOfWork._productRepository.Product_InsertUpdate(product);
            //return Ok(returnData);
        }
        [HttpPost("GetProductById")]
        public async Task<ActionResult> GetProductById(int id)
        {
            var product = new Product();
            product = await _unitOfWork._productRepository.GetProductById(id);
            return Ok(product);
        }
    }
}
