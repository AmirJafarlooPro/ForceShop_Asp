using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForceShop.Domian.Models.Entity;

namespace ForceShop.Domian.Models.Order
{
    public class OrderDetail:BaseEntity
    {
        public int OrderID { get; set; }

        [ForeignKey("OrderID")]
        public Order? Order { get; set; }


        //---------------------------

        public int ProductID { get; set; }

        [ForeignKey("ProductID")]
        public Product.Product?  Product { get; set; }


        //---------------------------

        public int Count { get; set; }
        public decimal Price { get; set; }



    }
}
