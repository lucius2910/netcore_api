using AutoMapper;
using Framework.Core.Abstractions;
using Framework.Core.Collections;
using Framework.Core.Extensions;
using Business.Core.Contracts;
using Seisankanri.Model.Entities;
using Business.Core.Interfaces;

namespace Business.Core.Services
{
    public class MachineServices : BaseServices, IMachineServices
    {
        private readonly IRepository<Machine> machineRepository;

        public MachineServices(IUnitOfWork _unitOfWork, IMapper _mapper) : base(_unitOfWork, _mapper)
        {
            machineRepository = _unitOfWork.GetRepository<Machine>();
        }

        public async Task<IPagedList<MachineResponse>> GetPaged(MachinePagedRequest request)
        {
            var data = machineRepository
                                  .GetQuery()
                                  .ExcludeSoftDeleted()
                                  .Where(x => string.IsNullOrEmpty(request.search) ? true : (x.code.ToLower().Contains(request.search.ToLower()) || x.name.ToLower().Contains(request.search.ToLower())))
                                  .ToPagedList(request.page, request.size);

            var dataMapping = _mapper.Map<PagedList<MachineResponse>>(data);
            return dataMapping;
        }

        public async Task<MachineResponse> GetById(Guid id)
        {
            var entity = machineRepository
                                 .GetQuery()
                                 .ExcludeSoftDeleted()
                                 .FilterById(id)
                                 .FirstOrDefault();

            var data = _mapper.Map<MachineResponse>(entity);
            return data;
        }
        public async Task<int> Create(MachineRequest request)
        { 
            var machine = _mapper.Map<Machine>(request);
            await machineRepository.AddEntityAsync(machine);
            var count = await _unitOfWork.SaveChangesAsync();

            return count;
        }


        public async Task<int> Update(Guid id, MachineRequest request)
        {
            var count = 0;
            var entity = _unitOfWork
                            .GetRepository<Machine>()
                            .GetQuery()
                            .FindActiveById(id)
                            .FirstOrDefault();

            if (entity == null)
                return count;

            _mapper.Map(request, entity);
            await machineRepository.UpdateEntityAsync(entity);


            count = await _unitOfWork.SaveChangesAsync();
            return count;
        }
        public async Task<int> Delete(Guid id)
        {
            var entity = machineRepository.GetQuery().FindActiveById(id).FirstOrDefault();

            await machineRepository.DeleteEntityAsync(entity);
            var count = await _unitOfWork.SaveChangesAsync();
            return count;
        }

        public async Task<int> Delete(Guid[] ids)
        {
            var entitys = machineRepository.GetQuery().FilterByIds(ids).ToList();

            foreach (var item in entitys)
            {
                await machineRepository.DeleteEntityAsync(item);
            }
            var count = await _unitOfWork.SaveChangesAsync();
            return count;
        }

        public async Task<List<MachineResponse>> GetList()
        {
            var data = machineRepository.GetQuery();
            var dataMapping = _mapper.Map<List<MachineResponse>>(data);
            return dataMapping;
        }
    }
}
