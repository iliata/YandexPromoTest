using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YaDemo.Dal.Interface;
using YaDemo.Model;

namespace YaDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromoController : ControllerBase
    {
        private List<NewPromoRequest> _promoCodes = new List<NewPromoRequest>();

        private readonly IPromoCodeRepository _promoCodeRepository;

        private readonly IRequestCounterStore _requestCounterStore;

        public PromoController(IPromoCodeRepository promoCodeRepository, IRequestCounterStore requestCounterStore)
        {
            _promoCodeRepository = promoCodeRepository;
            _requestCounterStore = requestCounterStore;
        }

        // GET api/promo/get
        [HttpGet]
        public async Task<ActionResult<bool>> Get(Guid userToken, string promoCode)
        {
            try
            {
                await Task.Run(() => _requestCounterStore.CheckUserRequest(userToken));
                return await _promoCodeRepository.IsPromoCodeValid(promoCode);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/promo/set
        [HttpPost]
        public async Task<ActionResult<PromoCode>> Set([FromBody] NewPromoRequest value)
        {
            try
            {
                return await _promoCodeRepository.GeneratePromoCode(value);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}