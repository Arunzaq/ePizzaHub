using System.ComponentModel.DataAnnotations;

namespace ePizza.UI.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage ="Please Enter UserName")]
        [EmailAddress(ErrorMessage ="Please Enter Correct Email")]
        public string UserName {  get; set; }
        [Required]
        public string Password { get; set; }
    }
}
