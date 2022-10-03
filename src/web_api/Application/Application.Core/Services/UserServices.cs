using AutoMapper;
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
        private readonly IRepository<UserRole> userRoleRepository;
        private readonly IRepository<Permission> permissionRepository;

        public UserServices(IUnitOfWork _unitOfWork, IMapper _mapper): base(_unitOfWork, _mapper)
        {
            userRepository = _unitOfWork.GetRepository<User>();
            userRoleRepository = _unitOfWork.GetRepository<UserRole>();
            permissionRepository = _unitOfWork.GetRepository<Permission>();
        }

        public async Task<PagedList<UserResponse>> GetPaged(UserSearchRequest request)
        {
            var data = userRepository
                        .GetQuery()
                        .ExcludeSoftDeleted()
                        .Where(x => string.IsNullOrEmpty(request.search) || x.code.ToLower().Contains(request.search.ToLower()) || x.full_name.ToLower().Contains(request.search.ToLower()))
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
            var userRole = userRoleRepository.GetQuery().ExcludeSoftDeleted()
                            .Include(x => x.user)
                            .Where(x => x.user.id == user_id)
                            .Select(x => x.role_cd)
                            .Distinct()
                            .ToList();

            // get permission
            var permissions = permissionRepository.GetQuery()
                                .ExcludeSoftDeleted()
                                .Where(x => userRole.Contains(x.role_cd))
                                .Select(x => x.function_cd)
                                .ToList();

            return permissions;
        }
    }
}
