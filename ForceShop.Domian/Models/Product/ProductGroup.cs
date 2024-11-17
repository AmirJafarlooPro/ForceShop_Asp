using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForceShop.Domian.Models.Entity;

namespace ForceShop.Domian.Models.Product
{
    public class ProductGroup : BaseEntity
    {
        #region Properties


        [Display(Name = "عنوان گروه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string GroupTitle { get; set; }

        #endregion

        //----------------------------

        #region NavicationProperties
        public List<Product>? Products { get; set; }

        #endregion
    }
}
