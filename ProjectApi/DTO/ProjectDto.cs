namespace ProjectApi.DTO
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Cost { get; set; }
        public List<TaskDto> Tasks { get; set; } = new List<TaskDto>();
        public List<EmployeeDto> Employees { get; set; } = new List<EmployeeDto>();
    }
}
