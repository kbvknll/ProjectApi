namespace ProjectApi.DTO
{
    public class TaskDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int ProjectId { get; set; } 
        public List<EmployeeDto> Employees { get; set; } = new List<EmployeeDto>();
    }
}
