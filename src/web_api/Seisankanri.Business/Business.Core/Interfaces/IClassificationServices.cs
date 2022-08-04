using Business.Core.Contracts;
using Framework.Core.Abstractions;
using Framework.Core.Collections;

namespace Business.Core.Interfaces
{
    public interface IClassificationServices
    {
        Task<IPagedList<ClassificationResponse>> GetList(ClassificationSearchRequest request);
        Task<int> CreateUpdate(List<ClassificationRequest> requests);
        Task<IList<ClassificationResponse>> GetByType(ClassificationGetByTypeRequest request);
    }
}
