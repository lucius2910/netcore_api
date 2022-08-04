using AutoMapper;
using Business.Core.Contracts;
using Business.Core.Interfaces;
using Framework.Core.Abstractions;
using Framework.Core.Collections;
using Framework.Core.Extensions;
using Seisankanri.Model.Entities;

namespace Business.Core.Services
{
    public class SystemSettingsServices : BaseServices, ISystemSettingsServices
    {
        private readonly IRepository<SystemSettings> systemsettingsRepository;

        public SystemSettingsServices(IUnitOfWork _unitOfWork, IMapper _mapper) : base(_unitOfWork, _mapper)
        {
            systemsettingsRepository = _unitOfWork.GetRepository<SystemSettings>();
        }
     
        public async Task<IPagedList<SystemSettingsResponse>> GetPaged(SystemSettingsPagedRequest request)
        {
            var data = systemsettingsRepository
                      .GetQuery()
                      .ExcludeSoftDeleted()
                      .SortBy(request.sort)
                      .ToPagedList(request.page, request.size);

            var dataMapping = _mapper.Map<PagedList<SystemSettingsResponse>>(data);
            return dataMapping;
        }


    
    }
}
