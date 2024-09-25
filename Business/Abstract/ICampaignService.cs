using CoreLayer.Entities.Concrete;
using CoreLayer.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICampaignService
    {
        IResult Delete(Campaign campaign);
        IResult Update(Campaign campaign);
        IDataResult<List<Campaign>> GetUsersCampaigns(User user);
    }
}
