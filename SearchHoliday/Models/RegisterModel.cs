using System.ComponentModel.DataAnnotations;

namespace SearchHoliday.Models
{
    public class RegisterModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
