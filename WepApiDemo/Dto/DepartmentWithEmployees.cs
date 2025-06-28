namespace WepApiDemo.Dto
{
    public class DepartmentWithEmployees
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<EmplyoeeDto> EmplyoeeDtos { get; set; } = new List<EmplyoeeDto>();
    }
    public class EmplyoeeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
