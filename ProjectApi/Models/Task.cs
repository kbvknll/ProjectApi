namespace ProjectApi.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public int ProjectId { get; set; }
        public Project Project { get; set; }

        public ICollection<TaskEmployee> TaskEmployees { get; set; } = new List<TaskEmployee>();
    }
}
