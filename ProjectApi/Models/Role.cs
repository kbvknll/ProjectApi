namespace ProjectApi.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Связь с пользователями (один ко многим)
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
