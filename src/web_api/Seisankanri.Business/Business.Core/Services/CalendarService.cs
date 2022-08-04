using AutoMapper;
using Business.Core.Contracts;
using Business.Core.Interfaces;
using Framework.Core.Abstractions;
using Framework.Core.Collections;
using Framework.Core.Extensions;
using Seisankanri.Model.Entities;

namespace Business.Core.Services
{
    public class CalendarService : BaseServices, ICalendarService
    {
        private readonly IRepository<Calendar> calendarRepository;
        private readonly IRepository<Company> companyRepository;

        public CalendarService(IUnitOfWork _unitOfWork, IMapper _mapper) : base(_unitOfWork, _mapper)
        {
            calendarRepository = _unitOfWork.GetRepository<Calendar>();
            companyRepository = _unitOfWork.GetRepository<Company>();
        }
        public async Task<List<CalendarResponse>> GetByCompany(CalendarSearchRequest request)
        {
            var entity = calendarRepository
                                  .GetQuery()
                                  .ExcludeSoftDeleted()
                                  .Where(x => x.company_cd == request.company_cd)
                                  .Where(x=> request.date_from != null ? x.calendar_date >= request.date_from :true)
                                  .Where(x => request.date_to != null ? x.calendar_date <= request.date_to : true)
                                  .ToList();
           
            var data = _mapper.Map<List<CalendarResponse>>(entity);
            return data;
        }

        public async Task<int> Create(CalendarRequest request)
        {
            var count = 0;
            var entity = _mapper.Map<Calendar>(request);
            await calendarRepository.AddEntityAsync(entity);
             count = await _unitOfWork.SaveChangesAsync();

            return count;
        }

        public async Task<int> Update(Guid id, CalendarRequest request)
        {
            var count = 0;
            var entity = _unitOfWork
                            .GetRepository<Calendar>()
                            .GetQuery()
                            .FindActiveById(id)
                            .FirstOrDefault();
            if (entity == null)
                return count;
            _mapper.Map(request, entity);
            await calendarRepository.UpdateEntityAsync(entity);
            count = await _unitOfWork.SaveChangesAsync();
            return count;
        }
    }
}