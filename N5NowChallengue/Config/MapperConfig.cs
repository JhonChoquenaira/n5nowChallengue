using AutoMapper;
using N5NowChallengue.BusinessService.DTO;
using N5NowChallengue.DataService.Models;

namespace N5NowChallengue.Config
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
            : base("ConfigurationProfile")
        {
            CreateMap<PermissionDto, Permission>();
            CreateMap<Permission, PermissionDto>();

            CreateMap<RequestPermissionDto, EmployeePermission>();
            CreateMap<EmployeePermission, RequestPermissionDto>();


        }
    }
}