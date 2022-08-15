using Application.Common.Abstractions;
using Application.Sale.Interfaces;
using AutoMapper;
using Domain.Entities;

namespace Application.Sale.Services
{
    public class SalePlanServices : BaseService, ISalePlanServices
    {
        private readonly IRepository<SalePlan> _salePlanRepository;

        public SalePlanServices(IUnitOfWork _unitOfWork, IMapper _mapper) : base(_unitOfWork, _mapper)
        {
            _salePlanRepository = _unitOfWork.GetRepository<SalePlan>();
        }
    }
}
