using AutoMapper;
using Framework.Core.Abstractions;
using Framework.Core.Collections;
using Application.Core.Contracts;
using Domain.Entities;
using Framework.Core.Extensions;

namespace WebAPI.ProfileMapper
{
    /// <summary>
    /// 
    /// </summary>
    public class CoreProfile : Profile
    {
        /// <summary>
        /// 
        /// </summary>
        public CoreProfile()
        {
            CreateMap<UserRequest, User>();
            CreateMap<IPagedList<User>, PagedList<UserResponse>>();
            CreateMap<User, UserResponse>();

            CreateMap<RoleRequest, Role>()
                .ForMember(dest => dest.permissions, opt => opt.Ignore());
            CreateMap<IPagedList<Role>, PagedList<RoleResponse>>();
            CreateMap<Role, RoleResponse>()
                .ForMember(dest => dest.permissions, opt => opt.MapFrom(src => src.permissions.Select(x => x.function.code).ToList()));

            CreateMap<FunctionRequest, Function>();
            CreateMap<IPagedList<Function>, PagedList<FunctionResponse>>();
            CreateMap<Function, FunctionResponse>()
                .ForMember(dest => dest.parent, opt => opt.MapFrom(src => src.parent.code));

            CreateMap<ResourceRequest, Resource>();
            CreateMap<IPagedList<Resource>, PagedList<ResourceResponse>>();
            CreateMap<Resource, ResourceResponse>();
            
            CreateMap<MasterCodeRequest, MasterCode>();
            CreateMap<IPagedList<MasterCode>, PagedList<MasterCodeRespose>>();
            CreateMap<MasterCode, MasterCodeRespose>();

        }
    }
}
