using Business.Core.Contracts;
using Framework.Core.Abstractions;
using Framework.Core.Collections;

namespace Business.Core.Interfaces
{
    public interface ISystemSettingsServices
    {
        Task<IPagedList<SystemSettingsResponse>> GetPaged(SystemSettingsPagedRequest request);

    }
}
