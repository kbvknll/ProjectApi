namespace ProjectApi.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Cost { get; set; }
        
        // Связь с задачами (один ко многим)
        public ICollection<Task> Tasks { get; set; } = new List<Task>();
    }
}
