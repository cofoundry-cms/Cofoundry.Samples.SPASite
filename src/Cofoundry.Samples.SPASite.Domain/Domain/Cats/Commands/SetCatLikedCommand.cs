using Cofoundry.Core.Validation;
using Cofoundry.Domain.CQS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cofoundry.Samples.SPASite.Domain
{
    public class SetCatLikedCommand : ICommand, ILoggableCommand
    {
        [PositiveInteger]
        [Required]
        public int CatId { get; set; }

        public bool IsLiked { get; set; }
    }

}
