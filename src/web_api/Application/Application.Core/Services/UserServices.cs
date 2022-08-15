using AutoMapper;
using Framework.Core.Abstractions;
using Framework.Core.Collections;
using Framework.Core.Extensions;
using Application.Core.Contracts;
using Microsoft.EntityFrameworkCore;
using Application.Core.Interfaces;
using Framework.Core.Helpers.Auth;
using Domain.Entities;
using Application.Common.Abstractions;

namespace Application.Core.Services
{
    public class UserServices : BaseService, IUserServices
    {
        private readonly IRepository<User> userRepository;
        private readonly IRepository<Permission> permissionRepository;

        public UserServices(IUnitOfWork _unitOfWork, IMapper _mapper): base(_unitOfWork, _mapper)
        {
            userRepository = _unitOfWork.GetRepository<User>();
            permissionRepository = _unitOfWork.GetRepository<Permission>();
        }

        public async Task<IPagedList<UserResponse>> GetPaged(UserSearchRequest request)
        {
            var data = userRepository
                        .GetQuery()
                        .ExcludeSoftDeleted()
                        .Where(x => string.IsNullOrEmpty(request.role_cd) || x.role_cd.ToLower().Equals(request.role_cd.ToLower()))
                        .Where(x => string.IsNullOrEmpty(request.code) || x.code.ToLower().Contains(request.code.ToLower()))
                        .Where(x => x.is_actived.Equals(request.is_actived))
                        .Where(x => string.IsNullOrEmpty(request.full_name) || x.full_name.ToLower().Contains(request.full_name.ToLower()))
                        .SortBy(request.sort)
                        .ToPagedList(request.page, request.size);

            var dataMapping = _mapper.Map<PagedList<UserResponse>>(data);
            return dataMapping;
        }

        public async Task<UserResponse> GetById(Guid id)
        {
            var user = await userRepository
                                  .GetQuery()
                                  .ExcludeSoftDeleted()
                                  .FilterById(id)
                                  .FirstOrDefaultAsync();

            var data = _mapper.Map<UserResponse>(user);
            return data;
        }

        public async Task<int> Create(UserRequest request)
        {
            var new_password = "123";
            var user = _mapper.Map<User>(request);
            user.salt = CryptographyProcessor.CreateSalt(20);
            user.hashpass = CryptographyProcessor.GenerateHash(new_password, user.salt);
            await userRepository.AddEntityAsync(user);
            var count = await _unitOfWork.SaveChangesAsync();
            return count;
        }

        public async Task<int> Update(Guid id, UserRequest request)
        {
            var count = 0;
            var user = userRepository
                            .GetQuery()
                            .FindActiveById(id)
                            .FirstOrDefault();

            if (user == null)
                return -1;
            _mapper.Map(request, user);
            await userRepository.UpdateEntityAsync(user);
            count = await _unitOfWork.SaveChangesAsync();
            return count;
        }

        public async Task<int> Delete(Guid id)
        {
            var user = userRepository.GetQuery().FindActiveById(id).FirstOrDefault();

            if (user == null)
                return -1;
            await userRepository.DeleteEntityAsync(user);
            var count = await _unitOfWork.SaveChangesAsync();
            return count;
        }

        public async Task<UserResponse> GetInfoLoginById(Guid id)
        {
            var data = await GetById(id);
            data.permissions = await GetPermissionsByUserId(id);
            return data;
        }

        public string GetUserNameById(Guid id)
        {
            var user = userRepository
                                  .GetQuery()
                                  .ExcludeSoftDeleted()
                                  .FilterById(id)
                                  .FirstOrDefault();

            return user?.user_name ?? string.Empty;
        }
        
        private async Task<List<string>> GetPermissionsByUserId(Guid user_id)
        {
            // get role
            var role_ids = userRepository.GetQuery()
                            .Where(x => x.id == user_id)
                            .ExcludeSoftDeleted()
                            .Select(x => x.role_cd)
                            .ToArray();

            // get permission
            var permissions = permissionRepository.GetQuery()
                                .ExcludeSoftDeleted()
                                .Where(x => role_ids.Contains(x.role_cd.ToString()))
                                .Include(x => x.function)
                                .Select(x => x.function)
                                .ExcludeSoftDeleted()
                                .Select(x => x.code)
                                .ToList();

            return permissions;
        }
    }
}
