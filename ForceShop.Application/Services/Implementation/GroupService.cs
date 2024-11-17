using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForceShop.Application.Services.Interfaces;
using ForceShop.Domian.Interfaces.Group;
using ForceShop.Domian.Models.Product;

namespace ForceShop.Application.Services.Implementation
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepository;

        public GroupService(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task<List<ProductGroup>> GetAllAsync()
        {
            return await _groupRepository.ListAsync();
        }

        public async Task<List<ProductGroup>> GetAllForFooterAsync()
        {
            return await _groupRepository.ListFooterAsync();
        }
    }
}
