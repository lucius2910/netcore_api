using AutoMapper;
using Business.Core.Contracts;
using Business.Core.Interfaces;
using Framework.Core.Abstractions;
using Framework.Core.Collections;
using Framework.Core.Extensions;
using Seisankanri.Model.Entities;

namespace Business.Core.Services
{
    public class ClassificationServices : BaseServices, IClassificationServices
    {
        private readonly IRepository<Classification> classificationRepository;

        public ClassificationServices(IUnitOfWork _unitOfWork, IMapper _mapper) : base(_unitOfWork, _mapper)
        {
            classificationRepository = _unitOfWork.GetRepository<Classification>();
        }
        
        public async Task<IPagedList<ClassificationResponse>> GetList(ClassificationSearchRequest request)
        {
            IPagedList<Classification> data = null;
            if (request.flag.Equals(1))
            {
                data = await classificationRepository.GetQuery()
                                                   .ExcludeSoftDeleted()
                                                   .Where(x => !String.IsNullOrEmpty(request.name) && !String.IsNullOrEmpty(request.code) ? (x.code1.Contains(request.code) || (x.name1.Contains(request.name) || x.name2.Contains(request.name)) || x.code1.Equals("00")) : true)
                                                   .SortBy(request.sort)
                                                   .ToPagedListAsync(request.page, request.size);
            }
            else if (request.flag.Equals(2))
            {
                data = await classificationRepository.GetQuery()
                    .ExcludeSoftDeleted()
                    .Where(x => x.code2 == request.code)
                    .SortBy(request.sort)
                    .ToPagedListAsync(request.page, request.size);
            }
            var dataMapping = data != null ? _mapper.Map<PagedList<ClassificationResponse>>(data) : null;
            return dataMapping;
        }

        public async Task<int> CreateUpdate(List<ClassificationRequest> requests)
        {
            var count = 0;
            var defaultId = new Guid();
            List<ClassificationRequest> inserted = requests.Where(x => x.id.ToString().ToLower() == defaultId.ToString().ToLower() && x != null).ToList();
            List<ClassificationRequest> updated = requests.Where(x => x.id.ToString().ToLower() != defaultId.ToString().ToLower() && x != null).ToList();
            List<ClassificationRequest> deleted = requests.Where(x => x.id.ToString().ToLower() != defaultId.ToString().ToLower() && x != null && x.is_delete).ToList();

            if (inserted != null && inserted.Count > 0)
            {
                foreach (var item in inserted)
                {
                    var newItem = _mapper.Map<Classification>(item);
                    await classificationRepository.AddEntityAsync(newItem);
                }
            }
            var data = _unitOfWork.GetRepository<Classification>().GetQuery().ExcludeSoftDeleted();
            if (updated != null && updated.Count > 0)
            
            {
                var entity = data.Where(x => updated.Select(y => y.id).Contains(x.id)).ToList();
                if (entity != null || entity.Count > 0)
                {
                    foreach (var item in entity)
                    {
                        var dataUpadte = updated.Where(x => x.id == item.id).FirstOrDefault();
                        _mapper.Map(dataUpadte, item);
                        await classificationRepository.UpdateEntityAsync(item);
                    }
                }
            }
            if (deleted != null && deleted.Count > 0)
            {
                var entity = data.Where(x => deleted.Select(y => y.id).Contains(x.id)).ToList();

                if (entity != null || entity.Count > 0)
                {
                    foreach (var item in entity)
                    {
                        await classificationRepository.DeleteEntityAsync(item);
                    }
                }
            }
            count = await _unitOfWork.SaveChangesAsync();
            return count;
        }

        public async Task<IList<ClassificationResponse>> GetByType(ClassificationGetByTypeRequest request)
        {
            var entity = classificationRepository
                            .GetQuery()
                            .ExcludeSoftDeleted()
                            .Where(x => !string.IsNullOrEmpty(request.type) ? (x.code1.ToLower() == request.type.ToLower()) : true)
                            .ToList();
            var dataMapping = _mapper.Map<List<ClassificationResponse>>(entity);
            return dataMapping;
        }
    }
}
