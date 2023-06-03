using System.Threading.Tasks;
using N5NowChallengue.DataService.Models;

namespace N5NowChallengue.DataService.Interface
{
    public interface IEmployeePermissionRepository: IGenericRepository<EmployeePermission>
    {
        Task<EmployeePermission> Get(int employeeId, int permissionId);

    }
}