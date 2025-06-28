using WepApiDemo.Model;
using WepApiDemo.Repositories.IRepositories;

namespace WepApiDemo.Repositories
{
    public class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        private readonly ApplicationDbContext context;

        public DepartmentRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }
    }
}
