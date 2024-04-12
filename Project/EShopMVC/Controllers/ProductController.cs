using EShop.Common;
using EShopMVC.Models;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EShopMVC.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ListPartialView(GetProduct requestData)
        {
            var list = new List<Product>();
            try
            {
                //var token_lay_tu_trinh_duyet = Request.Cookies["TEN_CooKIE_LUU_Token"] != null ? Request.Cookies["TEN_CooKIE_LUU_Token"].Value : string.Empty;
                //URL_API là cái addkey bên webconfig
                
                var url = System.Configuration.ConfigurationManager.AppSettings["URL_API"] ?? "";
                var baseUrl = "Product/GetListProduct";
                var bodyJson = JsonConvert.SerializeObject(requestData);

                var result = HttpRequestHelper.SendPostNoToken(url, baseUrl, bodyJson);
                list = JsonConvert.DeserializeObject<List<Product>>(result);

                //list = new DataAccess.ProductNetFramework.DAOimpl.ProductDAOimpl().GetProduct(requestData);

            }
            catch (Exception ex)
            {

                throw;
            }
            return PartialView(list);
        }
        [HttpPost]
        public JsonResult ProductDelete(ProductDeleteRequestData requestData)
        {
            var returnData = new ReturnData();
            try
            {
                var url = System.Configuration.ConfigurationManager.AppSettings["URL_API"] ?? "";
                var baseUrl = "Product/Product_Delete";
                var bodyJson = JsonConvert.SerializeObject(requestData);

                var result = HttpRequestHelper.SendPostNoToken(url, baseUrl, bodyJson);
                returnData = JsonConvert.DeserializeObject<ReturnData>(result);
                //var rs = new DataAccess.ProductNetFramework.DAOimpl.ProductDAOimpl().ProductDelete(requestData);
                return Json(returnData, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                returnData.ReturnCode = -999;
                returnData.ReturnMessage = "Hệ thống đang bận";
                return Json(returnData, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetProductbyId(int? id)
        {
            var model = new Product();
            try
            {
                if (id != null)
                {
                    var url = System.Configuration.ConfigurationManager.AppSettings["URL_API"] ?? "";
                    var baseUrl = "Product/GetProductById?id=" + id;
                    var bodyJson = JsonConvert.SerializeObject(id);

                    var result = HttpRequestHelper.SendPostNoToken(url, baseUrl, bodyJson);
                    model = JsonConvert.DeserializeObject<Product>(result);
                }

            }
            catch (Exception ex)
            {

                throw;
            }
            return View(model);
        }

        public JsonResult ProductInsertUpdate(Product product)
        {
            var returnData = new ReturnData();
            try
            {
                var url = System.Configuration.ConfigurationManager.AppSettings["URL_API"] ?? "";
                var baseUrl = "Product/Product_InsertUpdate";
                var bodyJson = JsonConvert.SerializeObject(product);

                var result = HttpRequestHelper.SendPostNoToken(url, baseUrl, bodyJson);
                returnData = JsonConvert.DeserializeObject<ReturnData>(result);
                //var rs = new DataAccess.ProductNetFramework.DAOimpl.ProductDAOimpl().ProductInsertUdpate(product);
                return Json(returnData, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                returnData.ReturnCode = -999;
                returnData.ReturnMessage = "Hệ thống đang bận";
                return Json(returnData, JsonRequestBehavior.AllowGet);
            }
        }
    }
}