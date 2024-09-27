using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampaignsController : ControllerBase
    {
        ICampaignService _campaignService;
        public CampaignsController(ICampaignService campaignService)
        {
            _campaignService = campaignService;
        }

        [HttpPost("AddCampaign")]
        public IActionResult Add(Campaign campaign)
        {
            var result = _campaignService.Add(campaign);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
        [HttpPost("UpdateCampaign")]
        public IActionResult Update(Campaign campaign)
        {
            var result = _campaignService.Update(campaign);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
        [HttpPost("DeleteCampaign")]
        public IActionResult Delete(Campaign campaign)
        {
            var result = _campaignService.Delete(campaign);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
    }
}
