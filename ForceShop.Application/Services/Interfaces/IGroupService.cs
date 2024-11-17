using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForceShop.Domian.Models.Product;

namespace ForceShop.Application.Services.Interfaces
{
    public interface IGroupService
    {
        public Task<List<ProductGroup>> GetAllAsync();
        public Task<List<ProductGroup>> GetAllForFooterAsync();
    }
}
