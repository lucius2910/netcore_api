

using AutoMapper;
using Business.Core.Contracts;
using Business.Core.Interfaces;
using Framework.Core.Abstractions;
using Framework.Core.Collections;
using Framework.Core.Extensions;
using Seisankanri.Model.Entities;

namespace Business.Core.Services
{
    public class SeqServices : BaseServices, ISeqServices
    {
        private readonly IRepository<Seq> seqRepository;
        public SeqServices(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            seqRepository = _unitOfWork.GetRepository<Seq>();
        }

        public async Task<string> ApiGenSeqType(string type)
        {
            var entity = seqRepository
                            .GetQuery()
                            .ExcludeSoftDeleted()
                            .Where(x => x.code == type)
                            .FirstOrDefault();
            
            if (entity != null)
            {
                entity.no = entity.no + 1;
                await seqRepository.UpdateEntityAsync(entity);
            }
            else
            {
                entity = new Seq();
                entity.code = type;
                entity.no = 1;
                await seqRepository.AddEntityAsync(entity);
             }
            await _unitOfWork.SaveChangesAsync();
            return $"{entity.code}{entity.no:D7}";
        }
    }
}
