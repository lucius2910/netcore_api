using AutoMapper;
using Application.Core.Contracts;
using Application.Core.Interfaces;
using Domain.Entities;
using Framework.Core.Abstractions;
using Framework.Core.Collections;
using Framework.Core.Extensions;
using Application.Common.Abstractions;

namespace Application.Core.Services
{
    public class ItemServices : BaseService, IItemServices
    {
        private readonly IRepository<Item> _itemRepository;

        public ItemServices(IUnitOfWork _unitOfWork, IMapper _mapper) : base(_unitOfWork, _mapper)
        {
            _itemRepository = _unitOfWork.GetRepository<Item>();
        }

        public async Task<IPagedList<ItemResponse>> GetListByType(ItemTypePagedRequest request)
        {
            var data = await _itemRepository.GetQuery()
                                .ExcludeSoftDeleted()
                                .Where(x => string.IsNullOrEmpty(request.type) || x.item_type.ToLower().Equals(request.type.ToLower()))
                                .SortBy(request.sort)
                                .ToPagedListAsync(request.page, request.size);

            var dataMapping = _mapper.Map<PagedList<ItemResponse>>(data);
            return dataMapping;
        }

        public async Task<IPagedList<ItemResponse>> GetPaged(PagedRequest request)
        {
            var data = await _itemRepository.GetQuery()
                                .ExcludeSoftDeleted()
                                .SortBy(request.sort)
                                .ToPagedListAsync(request.page, request.size);
            var dataMapping = _mapper.Map<PagedList<ItemResponse>>(data);
            return dataMapping;
        }
    }
}
