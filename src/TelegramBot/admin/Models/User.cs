namespace WebApplication_Vitalik.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? UserLogin { get; set; }
        public bool Block { get; set; }
        public int Role { get; set; }
        public int Campus { get; set; }

        public User()
        {
        }

        public User(string username)
        {
            UserLogin = username;
        }
    }
}
