using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using N5NowChallengue.BusinessService.DTO;
using N5NowChallengue.BusinessService.Interfaces;
using N5NowChallengue.DataService.Interface;
using N5NowChallengue.DataService.Models;
using N5NowChallengue.ErrorHandler;

namespace N5NowChallengue.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController: BaseController
    {
        private readonly IPermissionBusinessService _permissionBusinessService;

        public PermissionController(IPermissionBusinessService permissionBusinessService)
        {
            _permissionBusinessService = permissionBusinessService;
        }
        
        [HttpGet("[Action]")]
        [ProducesResponseType(typeof(BaseResult<List<PermissionDto>>), 200)]
        public IActionResult GetAll()
            => Result(() => _permissionBusinessService.GetAll());
        
        [HttpGet("")]
        [ProducesResponseType(typeof(BaseResult<PermissionDto>), 200)]
        public IActionResult GetById(int id)
            => Result(() => _permissionBusinessService.GetById(id));
        
        [HttpPut("")]
        [ProducesResponseType(typeof(BaseResult<MessageDto>), 200)]
        public IActionResult UpdatePermission(Permission permissionUpdated)
            => Result(() => _permissionBusinessService.UpdatePermission(permissionUpdated));
        
        [HttpPost("")]
        [ProducesResponseType(typeof(BaseResult<MessageDto>), 200)]
        public IActionResult AddPermission(PermissionDto permissionDto)
            => Result(() => _permissionBusinessService.AddPermission(permissionDto));
        
        
    }
}