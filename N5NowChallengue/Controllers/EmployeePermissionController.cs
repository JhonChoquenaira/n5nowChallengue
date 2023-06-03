using Microsoft.AspNetCore.Mvc;
using N5NowChallengue.BusinessService.DTO;
using N5NowChallengue.BusinessService.Interfaces;
using N5NowChallengue.ErrorHandler;

namespace N5NowChallengue.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeePermissionController: BaseController
    {
        private readonly IEmployeePermissionsBusinessService _employeePermissions;

        public EmployeePermissionController(IEmployeePermissionsBusinessService employeePermissions)
        {
            _employeePermissions = employeePermissions;
        }

        [HttpPost("[Action]")]
        [ProducesResponseType(typeof(BaseResult<MessageDto>), 200)]
        public IActionResult RequestPermission(RequestPermissionDto requestPermissionDto)
            => Result(() => _employeePermissions.RequestPermission(requestPermissionDto));
        
    }
}