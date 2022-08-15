using AutoMapper;
using Application.Core.Contracts;
using Application.Core.Interfaces;
using Domain.Entities;
using Framework.Core.Abstractions;
using Framework.Core.Collections;
using Framework.Core.Extensions;
using Application.Common.Abstractions;
using Application.Common.Extensions;

namespace Application.Core.Services
{
    public class CompanyServices : BaseService, ICompanyServices
    {
        private readonly IRepository<Company> companyRepository;
        private readonly IRepository<Branch> branchRepository;
        private readonly IRepository<Place> placeRepository;
        private readonly IRepository<Customer> customerRepository;
        private readonly IRepository<Supplier> supplierRepository;
        private readonly IRepository<OutSourcer> outsourceRepository;
        private readonly IRepository<Destination> destinationRepository;
        private readonly IRepository<Transpost> transportRepository;
        private readonly IRepository<Maker> makerRepository;

        public CompanyServices(IUnitOfWork _unitOfWork, IMapper _mapper) : base(_unitOfWork, _mapper)
        {
            companyRepository = _unitOfWork.GetRepository<Company>();
            branchRepository = _unitOfWork.GetRepository<Branch>();
            placeRepository = _unitOfWork.GetRepository<Place>();
            customerRepository = _unitOfWork.GetRepository<Customer>();
            supplierRepository = _unitOfWork.GetRepository<Supplier>();
            outsourceRepository = _unitOfWork.GetRepository<OutSourcer>();
            destinationRepository = _unitOfWork.GetRepository<Destination>();
            transportRepository = _unitOfWork.GetRepository<Transpost>();
            makerRepository = _unitOfWork.GetRepository<Maker>();
        }

        public async Task<IPagedList<CompanyResponse>> GetPaged(PagedRequest request)
        {
            var data = companyRepository
                                  .GetQuery()
                                  .ExcludeSoftDeleted()
                                  .Where(x => string.IsNullOrEmpty(request.search) ? true : (x.code.ToLower().Contains(request.search.ToLower()) ||
                                  x.company_name1.ToLower().Contains(request.search.ToLower()) || x.kana.ToLower().Contains(request.search.ToLower())))
                                  .SortBy(request.sort)
                                  .ToPagedList(request.page, request.size);

            var dataMapping = _mapper.Map<PagedList<CompanyResponse>>(data);
            return dataMapping;
        }
        public async Task<CompanyResponse> GetById(Guid id)
        {
            var entity = companyRepository
                                 .GetQuery()
                                 .ExcludeSoftDeleted()
                                 .FilterById(id)
                                 .FirstOrDefault();

            var data = _mapper.Map<CompanyResponse>(entity);
            return data;
        }

        public async Task<int> Create(CompanyRequest request)
        {
            var company = _mapper.Map<Company>(request);
            await companyRepository.AddEntityAsync(company);
            var count = await _unitOfWork.SaveChangesAsync();

            return count;
        }

        public async Task<int> Update(Guid id, CompanyRequest request)
        {
            var count = 0;
            var entity = _unitOfWork
                            .GetRepository<Company>()
                            .GetQuery()
                            .FindActiveById(id)
                            .FirstOrDefault();

            if (entity == null)
                return count;

            _mapper.Map(request, entity);
            await companyRepository.UpdateEntityAsync(entity);
            count = await _unitOfWork.SaveChangesAsync();
            return count;
        }
        public async Task<int> Delete(Guid id)
        {
            var entity = companyRepository.GetQuery().FindActiveById(id).FirstOrDefault();

            await companyRepository.DeleteEntityAsync(entity);
            var count = await _unitOfWork.SaveChangesAsync();
            return count;
        }

        public async Task<IPagedList<CompanyResponse>> GetListByType(CompanyTypePagedRequest request)
        {
            IEnumerable<Company> data = Enumerable.Empty<Company>();
            if (!string.IsNullOrEmpty(request.type))
            {
                switch (request.type)
                {
                    case CompanyType.PLACE:
                        data = placeRepository.GetQuery().ExcludeSoftDeleted().SortBy(request.sort);
                        break;
                    case CompanyType.BRANCH:
                        data = branchRepository.GetQuery().ExcludeSoftDeleted().SortBy(request.sort);
                        break;
                    case CompanyType.CUSTOMER:
                        data = customerRepository.GetQuery().ExcludeSoftDeleted().SortBy(request.sort);
                        break;
                    case CompanyType.SUPPLIER:
                        data = supplierRepository.GetQuery().ExcludeSoftDeleted().SortBy(request.sort);
                        break;
                    case CompanyType.OUTSOURCER:
                        data = outsourceRepository.GetQuery().ExcludeSoftDeleted().SortBy(request.sort);
                        break;
                    case CompanyType.DESTINATION:
                        data = destinationRepository.GetQuery().ExcludeSoftDeleted().SortBy(request.sort);
                        break;
                    case CompanyType.TRANSPOST:
                        data = transportRepository.GetQuery().ExcludeSoftDeleted().SortBy(request.sort);
                        break;
                    case CompanyType.MAKER:
                        data = makerRepository.GetQuery().ExcludeSoftDeleted().SortBy(request.sort);
                        break;
                    default:
                        data = companyRepository.GetQuery().ExcludeSoftDeleted().Where(x => x.id == Guid.Empty).SortBy(request.sort);
                        break;

                }
            }
            if (!string.IsNullOrEmpty(request.name))
            {
                data = data.Where(x => x.company_name1.ToLower().Contains(request.name.ToLower()) && !string.IsNullOrEmpty(x.company_name1));
            }
            if (!string.IsNullOrEmpty(request.code))
            {
                data = data.Where(x => x.code.ToLower().Contains(request.code.ToLower()) && !string.IsNullOrEmpty(x.code));
            }
            if (!string.IsNullOrEmpty(request.tel_no))
            {
                data = data.Where(x => x.tel_no.ToLower().Contains(request.tel_no.ToLower()) && !string.IsNullOrEmpty(x.tel_no));
            }
            if (!string.IsNullOrEmpty(request.fax_no))
            {
                data = data.Where(x => x.fax_no.ToLower().Contains(request.fax_no.ToLower()) && !string.IsNullOrEmpty( x.fax_no));
            }

            var res = data.ToPagedList(request.page, request.size);
            var dataMapping = _mapper.Map<PagedList<CompanyResponse>>(res);
            return dataMapping;

        }
    }
}
