namespace ProjectApi.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Description { get; set; }

        // Внешний ключ на проект
        public int ProjectId { get; set; }
        public Project Project { get; set; }

        // Связь с сотрудниками (многие ко многим через TaskEmployee)
        public ICollection<TaskEmployee> TaskEmployees { get; set; } = new List<TaskEmployee>();
    }
}
