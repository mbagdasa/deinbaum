namespace deinBaum.WebAPI.Models
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public bool IstAdminBerechtigt { get; set; } = false;
    }
}
