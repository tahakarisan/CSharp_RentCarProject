using Business.Abstract;
using CoreLayer.Aspects.Autofac;
using CoreLayer.Entities.Concrete;
using CoreLayer.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CampaignManager : ICampaignService
    {
        IUserService _userService;
        ICampaignDal _campaignDal;
        public CampaignManager(ICampaignDal campaignDal, IUserService userService)
        {
            _campaignDal = campaignDal;
            _userService = userService;

        }
    
        public IResult Delete(Campaign campaign)
        {
            var query = _campaignDal.FirstOrDefault(c=>c.Id==campaign.Id);
            if (query != null)
            {
                return new SuccesfullResult("Kampanya mevcut değil");
            }
            _campaignDal.Add(campaign);
            return new SuccesfullResult("Kampanya silindi");
        }
        [CacheAspect]
        public IDataResult<List<Campaign>> GetCampaigns(Campaign campaign)
        {
            return new SuccesfulDataResult<List<Campaign>>(_campaignDal.GetAll());
        }

        public IDataResult<List<Campaign>> GetUsersCampaigns(User user)
        {
            var query = _userService.GetByMail(user.Email);
            if (query==null)
            {
                return new ErrorDataResult<List<Campaign>>("Kullanıcı bulunamadı");
            }
            var result = _campaignDal.GetUserCampaigns(user);
            if (result==null)
            {
                return new ErrorDataResult<List<Campaign>>("Kullanıcıya ait Kampanya bulunamadı");
            }
            return new SuccesfulDataResult<List<Campaign>>(result);
        }

        public IResult Update(Campaign campaign)
        {
            var result = _campaignDal.FirstOrDefault(c=>c.Id==campaign.Id);
            if(result != null)
            {
                _campaignDal.Update(campaign);
                return new SuccesfullResult("Kampanya Güncellendi");
            }
            return new ErrorResult("Kampanya mevcut değil");
        }
    }
}
