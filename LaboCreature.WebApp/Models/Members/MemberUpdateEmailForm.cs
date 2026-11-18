using System.ComponentModel.DataAnnotations;

namespace LaboCreature.WebApp.Models.Members
{
    public class MemberUpdateEmailForm
    {
        public int Id { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [MinLength(5)]
        [Display(Name = "New email")]
        public string NewEmail { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
    }
}
