using CoreLayer.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class UserCampaign : BaseEntity
    {
        public int CampaignId { get; set; }
        public int UserId { get; set; }
    }
}
