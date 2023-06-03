using N5NowChallengue.BusinessService.DTO;

namespace N5NowChallengue.BusinessService.Interfaces
{
    public interface IEmployeePermissionsBusinessService
    {
        MessageDto RequestPermission(RequestPermissionDto requestPermissionDto);
    }
}