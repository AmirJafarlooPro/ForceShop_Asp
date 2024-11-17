using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForceShop.Domian.Models.Product;

namespace ForceShop.Domian.Interfaces.Group
{
    public interface IGroupRepository
    {
        public Task<List<ProductGroup>> ListAsync();
        public Task<List<ProductGroup>> ListFooterAsync();
    }
}
