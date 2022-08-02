using AutoMapper;
using Framework.Core.Abstractions;

namespace Framework.Core.Collections
{
    public class BaseServices : IServices
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;

        public BaseServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
    }
}
