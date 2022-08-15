﻿using Application.Core.Contracts;
using Framework.Core.Abstractions;
using Framework.Core.Collections;

namespace Application.Core.Interfaces
{
    public interface IResourceServices
    {
        Task<IPagedList<ResourceResponse>> GetPaged(ResourcePagedRequest request);
        List<ResourceResponse> GetByScreen(string lang, string module, string screen);
        Task<int> Create(ResourceRequest request);
        Task<ResourceResponse> GetById(Guid id);
        Task<int> Update(Guid id, ResourceRequest request);
        Task<int> Delete(Guid id);
        Task<IPagedList<string>?> GetListScreen(PagedRequest request);
        Task<IPagedList<string>?> GetScreensByModule(ResourcePagedRequest request);
    }
}