using AutoMapper;
using Business.Core.Contracts;
using Business.Core.Interfaces;
using Framework.Core.Abstractions;
using Framework.Core.Collections;
using Framework.Core.Extensions;
using Seisankanri.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Core.Services
{
    public class CategoryServices : BaseServices,ICategoryServices
    {
        private readonly IRepository<Category> categoryRepository;
        public CategoryServices(IUnitOfWork _unitOfWork, IMapper _mapper) : base(_unitOfWork, _mapper)
        {
            categoryRepository = _unitOfWork.GetRepository<Category>();
        }

        public async Task<IPagedList<CategoryResponse>> GetPaged(CategoryPagedRequest request)
        {
            var data = categoryRepository
                        .GetQuery()
                        .ExcludeSoftDeleted()
                        .Where(x => !string.IsNullOrEmpty(request.search) ? (x.code.ToLower().Contains(request.search.ToLower()) || x.name.ToLower().Contains(request.search.ToLower())) : true)
                        .ToPagedList(request.page, request.size);
            var dataMapping = _mapper.Map<PagedList<CategoryResponse>>(data);
            return dataMapping;
        }
        public async Task<int> Create(CategoryRequest request)
        {
            var category = _mapper.Map<Category>(request);
            await categoryRepository.AddEntityAsync(category);
            var count = await _unitOfWork.SaveChangesAsync();

            return count;
        }

        public async Task<CategoryResponse> GetById(Guid id)
        {
            var category = categoryRepository
                            .GetQuery()
                            .ExcludeSoftDeleted()
                            .FilterById(id)
                            .FirstOrDefault();
            var count = _mapper.Map<CategoryResponse>(category);
            return count;
        }

        public async Task<int> Update(Guid id, CategoryRequest request)
        {
            var count = 0;
            var entity = _unitOfWork
                            .GetRepository<Category>()
                            .GetQuery()
                            .FindActiveById(id)
                            .FirstOrDefault();
            if (entity == null)
                return count;
            _mapper.Map(request, entity);
            await categoryRepository.UpdateEntityAsync(entity);
            count = await _unitOfWork.SaveChangesAsync();
            return count;
        }

        public async Task<int> Delete(Guid id)
        {
            var entity = categoryRepository.GetQuery().FindActiveById(id).FirstOrDefault();
            await categoryRepository.DeleteEntityAsync(entity);
            var count = await _unitOfWork.SaveChangesAsync();
            return count;
        }

        public async Task<int> Delete(Guid[] ids)
        {
            var entitys = categoryRepository.GetQuery().FilterByIds(ids).ToList();

            foreach (var item in entitys)
            {
                await categoryRepository.DeleteEntityAsync(item);
            }
            var count = await _unitOfWork.SaveChangesAsync();
            return count;
        }
    }
}
