using Cofoundry.Domain.CQS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cofoundry.Samples.SPASite.Domain
{
    public class SetCatLikedCommand : ICommand, ILoggableCommand
    {
        public int CatId { get; set; }

        public bool IsLiked { get; set; }
    }

}
