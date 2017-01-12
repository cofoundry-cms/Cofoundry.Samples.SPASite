using Cofoundry.Domain.CQS;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Xml.Serialization;

namespace Cofoundry.Samples.SPASite
{
    public class LogMemberInCommand : ICommand
    {
        [Required]
        [EmailAddress(ErrorMessage = "Please use a valid email address")]
        public string Email { get; set; }
        
        [Required]
        [StringLength(300, MinimumLength = 8)]
        [DataType(DataType.Password)]
        [AllowHtml]
        public string Password { get; set; }
    }
}
