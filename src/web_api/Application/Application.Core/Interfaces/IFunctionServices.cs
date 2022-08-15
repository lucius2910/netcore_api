using Framework.Core.Abstractions;
using Application.Core.Contracts;

namespace Application.Core.Interfaces
{
    public interface IFunctionServices
    {
        Task<IPagedList<FunctionResponse>> GetPaged(FunctionPagedRequest request);
        Task<int> Create(FunctionRequest request);
        Task<FunctionResponse> GetById(Guid id);
        Task<int> Update(Guid id, FunctionRequest request);
        Task<int> Delete(Guid id);
        Task<int> Delete(Guid[] ids);
        Task<int> ChangeIsActive(Guid[] ids);
        Task<List<FunctionResponse>> GetAsTreeView();
    }
}
