using Framework.Core.Abstractions;
using Business.Core.Contracts;

namespace Business.Core.Interfaces
{
    public interface IUserServices
    {
        Task<IPagedList<UserResponse>> GetPaged(UserSearchRequest request);
        Task<UserResponse> GetById(Guid id);
        Task<UserResponse> GetInfoLoginById(Guid id);
        Task<int> Create(UserRequest request);
        Task<int> Update(Guid id, UserRequest request);
        Task<int> Delete(Guid id);
        string GetUserNameById(Guid id);
    }
}
