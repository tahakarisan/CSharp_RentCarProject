using CoreLayer.DataAccess;
using CoreLayer.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.Context;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EFCampaignDal : EFEntityRepositoryBase<Campaign,RentCarContext>,ICampaignDal
    {
        public bool AddUserCampaign(UserCampaign userCampaign)
        {
            using (var context = new RentCarContext())
            {
                var add = context.Entry(userCampaign);
                add.State = EntityState.Added;
                return context.SaveChanges() > 0;
            }
        }

        public List<Campaign> GetUserCampaigns(User user)
        {
            using (var context = new RentCarContext())
            {
                var result = from campaigns in context.Campaigns
                             join userCampaigns in context.UserCampaigns
                             on campaigns.Id equals userCampaigns.CampaignId
                             where userCampaigns.Id == user.Id
                             select new Campaign
                             {
                                 Id = campaigns.Id,
                                 Name = campaigns.Name
                             };
                return result.ToList();
            }
        }
    }
}
