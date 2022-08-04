using Business.Core.Contracts;
using Framework.Core.Abstractions;

namespace Business.Core.Interfaces
{
    public interface ICategoryServices
    {
        Task<IPagedList<CategoryResponse>> GetPaged(CategoryPagedRequest request);
        Task<int> Create(CategoryRequest request);
        Task<CategoryResponse> GetById(Guid id);
        Task<int> Update(Guid id, CategoryRequest request);
        Task<int> Delete(Guid id);
        Task<int> Delete(Guid[] ids);
    }
}
