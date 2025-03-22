namespace ProjectApi.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<TaskEmployee> TaskEmployees { get; set; } = new List<TaskEmployee>();
    }
}
