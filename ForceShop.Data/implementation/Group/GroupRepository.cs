using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForceShop.Data.Contex;
using ForceShop.Domian.Interfaces.Group;
using ForceShop.Domian.Models.Product;
using Microsoft.EntityFrameworkCore;

namespace ForceShop.Data.implementation.Group
{
    public class GroupRepository : IGroupRepository
    {

        #region DependecyInjection

        private readonly ForceShopContex _contex;

        public GroupRepository(ForceShopContex contex)
        {
            _contex = contex;
        }

        #endregion

        //-------------------------------

        #region List

        public async Task<List<ProductGroup>> ListAsync()
        {
            
            return await _contex.ProductGroups
               .AsNoTracking()
               .Where(p => !p.IsDelete)
               .ToListAsync();
        }

        public async Task<List<ProductGroup>> ListFooterAsync()
        {
            return await _contex.ProductGroups
                .AsNoTracking()
                .Where(p => !p.IsDelete)
                .ToListAsync();
        }

        #endregion

    }
}
