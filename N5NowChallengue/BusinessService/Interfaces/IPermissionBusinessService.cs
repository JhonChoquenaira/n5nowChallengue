using System.Collections.Generic;
using N5NowChallengue.BusinessService.DTO;
using N5NowChallengue.DataService.Models;

namespace N5NowChallengue.BusinessService.Interfaces
{
    public interface IPermissionBusinessService
    {
        List<PermissionDto> GetAll();
        PermissionDto GetById(int permissionId);
        MessageDto UpdatePermission(Permission permissionUpdated);
        MessageDto AddPermission(PermissionDto newPermissionDto);
    }
}