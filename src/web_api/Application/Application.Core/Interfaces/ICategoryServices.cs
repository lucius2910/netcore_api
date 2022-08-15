using Application.Core.Contracts;
using Framework.Core.Abstractions;
using Framework.Core.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Interfaces
{
    public interface ICategoryServices
    {
        Task<IPagedList<CategoryResponse>> GetPaged(PagedRequest request);
        Task<int> Create(CategoryRequest request);
        Task<CategoryResponse> GetById(Guid id);
        Task<int> Update(Guid id, CategoryRequest request);
        Task<int> Delete(Guid id);
        Task<int> Delete(Guid[] ids);
    }
}
