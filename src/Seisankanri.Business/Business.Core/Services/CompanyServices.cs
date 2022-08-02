using AutoMapper;
using Business.Core.Contracts;
using Business.Core.Extensions;
using Business.Core.Interfaces;
using Framework.Core.Abstractions;
using Framework.Core.Collections;
using Framework.Core.Extensions;
using Seisankanri.Model.Entities;
namespace Business.Core.Services
{
    public class CompanyServices : BaseServices, ICompanyServices
    {
        private readonly IRepository<Company> companyRepository;

        public CompanyServices(IUnitOfWork _unitOfWork, IMapper _mapper) : base(_unitOfWork, _mapper)
        {
            companyRepository = _unitOfWork.GetRepository<Company>();
        }

        public async Task<IPagedList<CompanyResponse>> GetPaged(CompanyPagedRequest request)
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
            var data = companyRepository
                                  .GetQuery()
                                  .ExcludeSoftDeleted()
                                  .SortBy(request.sort);

            if (!string.IsNullOrEmpty(request.type))
            {
                switch (request.type)
                {
                    case CompanyType.PLACE:
                        data = data.Where(x => x.place_f == 1);
                        break;
                    case CompanyType.BRANCH:
                        data = data.Where(x => x.branch_f == 1);
                        break;
                    case CompanyType.CUSTOMER:
                        data = data.Where(x => x.customer_f == 1);
                        break;
                    case CompanyType.SUPPLIER:
                        data = data.Where(x => x.supplier_f == 1);
                        break;
                    case CompanyType.OUTSOURCER:
                        data = data.Where(x => x.outsourcer_f == 1);
                        break;
                    case CompanyType.DESTINATION:
                        data = data.Where(x => x.destination_f == 1);
                        break;
                    case CompanyType.TRANSPOST:
                        data = data.Where(x => x.transpost_f == 1);
                        break;
                    case CompanyType.MAKER:
                        data = data.Where(x => x.maker_f == 1);
                        break;
                    default:
                        data = data.Where(x => x.id == Guid.Empty);
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
