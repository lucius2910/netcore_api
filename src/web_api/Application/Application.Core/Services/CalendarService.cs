using AutoMapper;
using Application.Core.Contracts;
using Application.Core.Interfaces;
using Domain.Entities;
using Framework.Core.Extensions;
using Application.Common.Abstractions;

namespace Application.Core.Services
{
    public class CalendarService : BaseService, ICalendarService
    {
        private readonly IRepository<Calendar> calendarRepository;
        private readonly IRepository<Company> companyRepository;

        public CalendarService(IUnitOfWork _unitOfWork, IMapper _mapper) : base(_unitOfWork, _mapper)
        {
            calendarRepository = _unitOfWork.GetRepository<Calendar>();
            companyRepository = _unitOfWork.GetRepository<Company>();
        }
        public async Task<List<CalendarResponse>> GetByCompany(CalendarSearchRequest calendar)
        {
            var entity = calendarRepository
                                  .GetQuery()
                                  .ExcludeSoftDeleted().Where(x => x.company_cd == calendar.company).ToList();
           
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