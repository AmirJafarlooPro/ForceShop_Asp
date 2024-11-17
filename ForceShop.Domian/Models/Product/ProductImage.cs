using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForceShop.Domian.Models.Entity;

namespace ForceShop.Domian.Models.Product
{
    public class ProductImage:BaseEntity
    {
        public int ProductID { get; set; }

        [ForeignKey("ProductID")]
        public Product? Product { get; set; }

        public string ImageName { get; set; }
    }
}
