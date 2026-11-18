using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LaboCreature.WebApp.Models.Members
{
    public class MemberUpdatePasswordForm
    {
        public int Id { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Current password")]
        public string CurrentPassword { get; set; } = null!;
        [Required]
        [DataType(DataType.Password)]
        [DisplayName("New password")]
        [MinLength(7, ErrorMessage = "Password must be at least 7 characters.")]
        public string NewPassword { get; set; } = null!;
        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword))]
        [DisplayName("Confirm new password")]
        public string NewPasswordConfirm { get; set; } = null!;
    }
}
