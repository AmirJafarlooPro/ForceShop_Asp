using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForceShop.Domian.Models.Entity
{
    public class BaseEntity
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "تاریخ ساخت")]
        public DateTime CreateDate { get; set; }


        [Display(Name = "پاک شده ؟")]
        public bool IsDelete { get; set; } 

    }
}
