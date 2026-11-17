using System.ComponentModel.DataAnnotations;

namespace LaboCreature.WebApp.Models.Members
{
    public class MemberUpdateUsernameForm
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [MinLength(7, ErrorMessage = "Username must be at least 7 characters")]
        public string NewUsername { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }=null!;
    }
}
