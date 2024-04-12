using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EShop.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Shop_Id { get; set; }
        public string Description { get; set; }
        //public DateTime CreateDate { get; set;}
        public int Quantity { get; set; }
        public string Base64Image { get; set; }
    }
    
    public class ProductDeleteRequestData
    {
        public int Id { get; set; }
    }
    public class GetProduct
    {
        public string? ProductName { get; set; }
    }
}
