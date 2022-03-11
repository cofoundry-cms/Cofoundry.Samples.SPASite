using Cofoundry.Domain.CQS;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Cofoundry.Samples.SPASite
{
    public class RegisterMemberAndLogInCommand : ICommand
    {
        [StringLength(150)]
        [EmailAddress(ErrorMessage = "Please use a valid email address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string DisplayName { get; set; }

        [Required]
        [StringLength(300, MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [OutputValue]
        public int OutputMemberId { get; set; }
    }
}