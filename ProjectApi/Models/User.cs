using System.Data;

namespace ProjectApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        // Внешний ключ на роль
        public int RoleId { get; set; }
        public Role Role { get; set; }

        // Связь с сотрудником (один к одному)
        public Employee Employee { get; set; }
    }
}
