using AutoMapper;
using Business.Core.Contracts;
using Business.Core.Interfaces;
using Framework.Core.Abstractions;
using Framework.Core.Collections;
using Framework.Core.Extensions;
using Seisankanri.Model.Entities;


namespace Business.Core.Services
{
    public class ItemServices : BaseServices, IItemServices
    {
        private readonly IRepository<Item> _itemRepository;

        public ItemServices(IUnitOfWork _unitOfWork, IMapper _mapper) : base(_unitOfWork, _mapper)
        {
            _itemRepository = _unitOfWork.GetRepository<Item>();
        }

        public async Task<IPagedList<GetItemByTypeResponse>> GetListByType(ItemTypePagedRequest request)
        {
            var data = await _itemRepository.GetQuery()
                                .ExcludeSoftDeleted()
                                .Where(x => string.IsNullOrEmpty(request.name) || (x.name1.ToLower().Contains(request.name.ToLower()) || x.name2.ToLower().Contains(request.name.ToLower())))
                                .Where(x => string.IsNullOrEmpty(request.type) || x.item_type.ToLower().Equals(request.type.ToLower()))
                                .Where(x => string.IsNullOrEmpty(request.code) || x.code.ToLower().Equals(request.code.ToLower()))                                
                                .SortBy(request.sort)
                                .ToPagedListAsync(request.page, request.size);

            var dataMapping = _mapper.Map<PagedList<GetItemByTypeResponse>>(data);
            return dataMapping;
        }

        public async Task<IPagedList<ItemResponse>> GetPaged(ItemSearchRequest request)
        {
            var data = await _itemRepository.GetQuery()
                                .ExcludeSoftDeleted()
                                .Where(x => string.IsNullOrEmpty(request.item_type) || x.item_type.ToLower().Equals(request.item_type.ToLower()))
                                .Where(x => string.IsNullOrEmpty(request.code) || x.code.ToLower().Equals(request.code.ToLower()))
                                .Where(x => string.IsNullOrEmpty(request.name) || ( x.name1.ToLower().Contains(request.name.ToLower()) || x.name2.ToLower().Contains(request.name.ToLower()) ))
                                .Where(x => string.IsNullOrEmpty(request.name1) || x.name1.ToLower().Contains(request.name1.ToLower()))
                                .Where(x => string.IsNullOrEmpty(request.name2) || x.name2.ToLower().Contains(request.name2.ToLower()))
                                .SortBy(request.sort)
                                .ToPagedListAsync(request.page, request.size);
            var dataMapping = _mapper.Map<PagedList<ItemResponse>>(data);
            return dataMapping;
        }

        public async Task<ItemResponse> GetById(Guid id)
        {
            var data = _itemRepository.GetQuery()
                                .ExcludeSoftDeleted()
                                .FilterById(id)
                                .FirstOrDefault();

            var dataMapping = _mapper.Map<ItemResponse>(data);
            return dataMapping;
        }
    }
}
