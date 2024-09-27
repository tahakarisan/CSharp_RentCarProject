using Business.Abstract;
using CoreLayer.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ManagementManager : IManagementService
    {
        ICampaignService _campaignService;
        public ManagementManager(ICampaignService campaignService)
        {
            _campaignService = campaignService;
        }
        public IResult CampaignDefine(UserCampaign userCampaign)
        {
            return new SuccesfullResult();
        }
    }
}
