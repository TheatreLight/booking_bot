namespace WebApplication_Vitalik.Models
{
    public class LoginViewModel
    {
        public string? login { get; set; } // имя пользователя
        public string? password { get; set; } // имя пользователя

        public LoginViewModel()
        {
        }

        public LoginViewModel(string alogin, string apassword)
        {
            login = alogin;
            password = apassword;
        }
    }
}
