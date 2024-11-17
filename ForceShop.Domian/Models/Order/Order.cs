using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForceShop.Domian.Models.Entity;

namespace ForceShop.Domian.Models.Order
{
    public class Order:BaseEntity
    {
        public int UserID { get; set; }

        public bool IsFinally { get; set; }


        public List<OrderDetail>? OrderDetails { get; set; }
    }
}
