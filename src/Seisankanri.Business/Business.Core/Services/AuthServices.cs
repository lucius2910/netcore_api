using AutoMapper;
using Framework.Api.Contracts;
using Framework.Core.Abstractions;
using Framework.Core.Extensions;
using Framework.Core.Helpers.Auth;
using Microsoft.Extensions.Options;
using Business.Core.Contracts;
using Seisankanri.Model.Entities;
using Business.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Framework.Core.Collections;
using Framework.Core.Helpers.Cache;

namespace Business.Core.Services
{
    public class AuthServices : BaseServices, IAuthServices
    {
        private readonly AuthSetting settings;
        private readonly IRepository<User> userRepository;
        private readonly IRepository<Role> roleRepository;
        private readonly ICachingService cachingService;

        public AuthServices(ICachingService _cachingService, IOptions<AuthSetting> _settings, IUnitOfWork _unitOfWork, IMapper _mapper) : base(_unitOfWork, _mapper)
        {
            settings = _settings.Value;
            cachingService = _cachingService;
            userRepository = _unitOfWork.GetRepository<User>();
            roleRepository = _unitOfWork.GetRepository<Role>();
        }

        public async Task<AuthLoginResponse> Login(AuthLoginRequest request)
        {
            // Check user login
            var user = userRepository
                        .GetQuery()
                        .ExcludeSoftDeleted()
                        .Where(x => x.user_name == request.user_name)
                        .FirstOrDefault();

            // Response UnAuthorized
            if(user == null || !CryptographyProcessor.AreEqual(request.password, user.hashpass, user.salt))
                return null;

            // Generate token
            var data = new AuthLoginResponse()
            {
                access_token = JwtHelpers.GenerateToken(user.id, settings.JWTSecret),
                refresh_token = JwtHelpers.GenerateRefreshToken(user.id, settings.JWTSecret)
            };

            return data;
        }

        public async Task<AuthLoginResponse> Refresh(string refresh_token)
        {
            // Check refresh token
            var id = JwtHelpers.ValidateToken(refresh_token, settings.JWTSecret);

            if (id == null)
                return null;

            // Check user id
            var user = userRepository
                        .GetQuery()
                        .ExcludeSoftDeleted()
                        .FilterById((Guid)id)
                        .FirstOrDefault();

            // Response UnAuthorized
            if (user == null)
                return null;

            // Generate new token
            var data = new AuthLoginResponse()
            {
                access_token = JwtHelpers.GenerateToken(user.id, settings.JWTSecret),
                refresh_token = JwtHelpers.GenerateRefreshToken(user.id, settings.JWTSecret)
            };
            return data;
        }

        public Task<int> ChangePassword(Guid id, AuthChangePassRequest request)
        {
            var user = userRepository
                        .GetQuery()
                        .FindActiveById(id)
                        .FirstOrDefault();


            if (user == null || !CryptographyProcessor.AreEqual(request.current_password, user.hashpass, user.salt))
                return Task.FromResult(0);

            user.salt = CryptographyProcessor.CreateSalt(20);
            user.hashpass = CryptographyProcessor.GenerateHash(request.new_password, user.salt);
            userRepository.UpdateEntityAsync(user);

            return _unitOfWork.SaveChangesAsync();
        }

        public bool CheckUserAuthorized(Guid user_id, string path, string action)
        {
            var userRole = userRepository.GetQuery().ExcludeSoftDeleted().Where(x => x.id == user_id).Select(x => x.role_cd).FirstOrDefault();
            var permissions = roleRepository
                               .GetQuery()
                               .ExcludeSoftDeleted()
                               .Where(x => x.code == userRole)
                               .Include(x => x.permissions)
                               .SelectMany(x => x.permissions)
                               .ExcludeSoftDeleted()
                               .Include(x => x.function)
                               .Select(x => x.function)
                               .Where(x => !string.IsNullOrEmpty(x.method))
                               .Distinct()
                               .OrderBy(x => x.code)
                               .ToArray();
            var check = permissions.Where(x => x.path.ToLower() == path.ToLower() && x.method.ToUpper() == action).FirstOrDefault();
            return check != null ? true : false;
        }
    }
}
