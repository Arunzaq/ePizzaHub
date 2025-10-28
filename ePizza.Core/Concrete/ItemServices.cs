using AutoMapper;
using ePizza.Core.Contracts;
using ePizza.Models.Response;
using ePizza.Repository.Concrete;
using ePizza.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizza.Core.Concrete
{
    public class ItemServices :IItemServices
    {
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;

        public ItemServices(IItemRepository itemRepository, IMapper mapper ) 
        {
            _itemRepository = itemRepository;
            _mapper = mapper;
        }


        public IEnumerable<ItemResponseModel> GetItems()
        { 
          var items= _itemRepository.GetAll();
            return _mapper.Map<IEnumerable<ItemResponseModel>>( items );
        }
    }
}
