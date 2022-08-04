using AutoMapper;
using Business.Core.Contracts;
using Business.Core.Interfaces;
using Framework.Core.Abstractions;
using Framework.Core.Collections;
using Framework.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using Seisankanri.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Core.Services
{
    public class ItemEdiServices : BaseServices, IItemEdiServices
    {
        private readonly IRepository<ItemEdi> _itemEdiRepository;

        public ItemEdiServices(IUnitOfWork _unitOfWork, IMapper _mapper) : base(_unitOfWork, _mapper)
        {
            _itemEdiRepository = _unitOfWork.GetRepository<ItemEdi>();
        }

        public async Task<IPagedList<ItemEdiResponse>> GetPaged(ItemEdiPagedRequest request)
        {
            var data = await _itemEdiRepository.GetQuery()
                                .ExcludeSoftDeleted()
                                .Where(x => string.IsNullOrEmpty(request.company_cd) || x.company_cd.ToLower().Equals(request.company_cd.ToLower()))
                                .SortBy(request.sort)
                                .ToPagedListAsync(request.page, request.size);

            var dataMapping = _mapper.Map<PagedList<ItemEdiResponse>>(data);
            return dataMapping;
        }

        public async Task<IList<ItemEdi>> GetByItemAndCompany(string item_cd, string company_cd)
        {
            var data = await _itemEdiRepository.GetQuery()
                                .ExcludeSoftDeleted()
                                .Where(x => string.IsNullOrEmpty(item_cd) || x.item_cd.ToLower().Equals(item_cd.ToLower()))
                                .Where(x => string.IsNullOrEmpty(company_cd) || x.company_cd.ToLower().Equals(company_cd.ToLower()))
                                .ToListAsync();
            return data;
        }

        public async Task<ItemEdiResponse> GetById(Guid id)
        {
            var data = await _itemEdiRepository.GetQuery()
                                .ExcludeSoftDeleted()
                                .FilterById(id)
                                .FirstOrDefaultAsync();

            var dataMapping = _mapper.Map<ItemEdiResponse>(data);
            return dataMapping;
        }

        public async Task<int> Create(ItemEdiCreateRequest request)
        {
            var itemEdi = _mapper.Map<ItemEdi>(request);
            await _itemEdiRepository.AddEntityAsync(itemEdi);
            var count = await _unitOfWork.SaveChangesAsync();
            return count;
        }

        public async Task<int> Update(Guid id, ItemEdiUpdateRequest request)
        {
            var itemEdi = await _itemEdiRepository.GetQuery()
                                    .FindActiveById(id)
                                    .FirstOrDefaultAsync();

            if (itemEdi == null)
                return -1;

            _mapper.Map(request, itemEdi);
            await _itemEdiRepository.UpdateEntityAsync(itemEdi);
            var count = await _unitOfWork.SaveChangesAsync();
            return count;
        }

        public async Task<int> Delete(Guid id)
        {
            var itemEdi = await _itemEdiRepository.GetQuery()
                                    .FindActiveById(id)
                                    .FirstOrDefaultAsync();

            if (itemEdi == null)
                return -1;

            await _itemEdiRepository.DeleteEntityAsync(itemEdi);
            var count = await _unitOfWork.SaveChangesAsync();
            return count;
        }
    }
}
