using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForceShop.Domian.Models.Order;
using ForceShop.Domian.Models.Product;
using ForceShop.Domian.Models.User;
using Microsoft.EntityFrameworkCore;

namespace ForceShop.Data.Contex
{
    public class ForceShopContex:DbContext
    {
        public ForceShopContex(DbContextOptions<ForceShopContex> options):base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<ProductGroup> ProductGroups { get; set; }
        public DbSet<Product> Products { get; set; }

        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; } 
    }
}
