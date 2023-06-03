using N5NowChallengue.DataService.Context;
using N5NowChallengue.DataService.Interface;
using N5NowChallengue.DataService.Models;

namespace N5NowChallengue.DataService.Repositories
{
    public class PermissionRepository: GenericRepository<Permission>, IPermissionRepository
    {
        public PermissionRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}