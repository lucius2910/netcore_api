using Application.Core.Contracts;
using Framework.Core.Abstractions;
using Framework.Core.Collections;

namespace Application.Core.Interfaces
{
    public interface IClassificationServices
    {
        Task<IPagedList<ClassificationResponse>> GetList(ClassificationSearchRequest request);
        Task<int> CreateUpdate(List<ClassificationRequest> requests);
    }
}
