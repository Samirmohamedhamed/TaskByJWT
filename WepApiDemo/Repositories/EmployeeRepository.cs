using WepApiDemo.Model;
using WepApiDemo.Repositories.IRepositories;

namespace WepApiDemo.Repositories
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        private readonly ApplicationDbContext context;

        public EmployeeRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }
    }
}
