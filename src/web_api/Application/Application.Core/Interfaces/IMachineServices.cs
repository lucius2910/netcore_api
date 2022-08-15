﻿using Framework.Core.Abstractions;
using Framework.Core.Collections;
using Application.Core.Contracts;

namespace Application.Core.Interfaces
{
    public interface IMachineServices
    {
        Task<IPagedList<MachineResponse>> GetPaged(PagedRequest request);
        Task<int> Create(MachineRequest request);
        Task<MachineResponse> GetById(Guid id);
        Task<int> Update(Guid id, MachineRequest request);
        Task<int> Delete(Guid id);
        Task<int> Delete(Guid[] ids);
        Task<List<MachineResponse>> GetList();
    }
}