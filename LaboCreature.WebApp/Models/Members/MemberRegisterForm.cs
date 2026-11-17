using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LaboCreature.WebApp.Models.Members
{
    public class MemberRegisterForm
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [MinLength(5, ErrorMessage ="Username must be at least 5 characters.")]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MinLength(7, ErrorMessage ="Password must be at least 7 characters.")]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        [DisplayName("Confirm password")]
        public string PasswordConfirm { get; set; }
    }
}
