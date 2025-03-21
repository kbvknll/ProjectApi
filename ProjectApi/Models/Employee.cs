namespace ProjectApi.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // Внешний ключ на пользователя
        public int UserId { get; set; }
        public User User { get; set; }

        // Связь с задачами (многие ко многим через TaskEmployee)
        public ICollection<TaskEmployee> TaskEmployees { get; set; } = new List<TaskEmployee>();
    }
}
