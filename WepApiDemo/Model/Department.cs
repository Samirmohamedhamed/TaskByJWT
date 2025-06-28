namespace WepApiDemo.Model
{
    public class Department
    {
        public int Id { get; set; }
        public string DeptName { get; set; }
        public string ManagerName {  get; set; }
        public List<Employee>? Employees { get; set; } = new List<Employee>();
    }
}
