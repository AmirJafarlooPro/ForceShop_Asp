using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForceShop.Domian.Models.Entity;
using ForceShop.Domian.Models.Order;
using Microsoft.EntityFrameworkCore;



namespace ForceShop.Domian.Models.Product
{
    public class Product : BaseEntity
    {

        #region Title

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Title { get; set; }

        #endregion

        #region ShortDescription

        [Display(Name = "توضیحات کوتاه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(400)]
        [DataType(DataType.MultilineText)]
        public string ShortDescription { get; set; }

        #endregion

        #region Description

        [Display(Name = "توضیحات کامل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.MultilineText)]

        public string Description { get; set; }

        #endregion

        #region GroupID

        [Display(Name = "عنوان گروه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int GroupID { get; set; }

        #endregion

        #region Price

        [Display(Name = "قیمت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Precision(18, 2)]
        public decimal Price { get; set; }


        #endregion

        #region Tags

        [Display(Name = "کلمات کلیدی")]
        public string? Tags { get; set; }

        #endregion

        #region Image



        #endregion

        //----------------------------

        #region NavicationProperties

        [ForeignKey("GroupID")]
        public ProductGroup? ProductGroup { get; set; }

        public List<ProductImage>? ProductImages { get; set; }

        public List<OrderDetail>? OrderDetails { get; set; }

        #endregion
    }
}
