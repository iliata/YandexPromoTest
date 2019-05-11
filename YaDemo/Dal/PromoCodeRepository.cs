using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YaDemo.Constants;
using YaDemo.Dal.Context;
using YaDemo.Dal.Interface;
using YaDemo.Helpers;
using YaDemo.Model;

namespace YaDemo.Dal
{
    public class PromoCodeRepository : IPromoCodeRepository
    {
        private readonly PromoCodeContext _context;

        public PromoCodeRepository(IOptions<MongoSettings> settings)
        {
            _context = new PromoCodeContext(settings);
        }

        public async Task<PromoCode> GeneratePromoCode(NewPromoRequest newPromoCode)
        {
            var promoCode = new PromoCode
            {
                Id = Guid.NewGuid(),
                ExpiryDate = newPromoCode.ExpiryDate,
                Value = newPromoCode.Value,
                Code = string.IsNullOrEmpty(newPromoCode.Code) 
                    ? PromoCodeHelper.RandomString(PromoCodeConstant.PromoCodeLength, true) 
                    : newPromoCode.Code
            };

            await _context.PromoCodes.InsertOneAsync(promoCode);

            return promoCode;
        }

        public async Task<bool> IsPromoCodeValid(string promoCode)
        {
            var filter = Builders<PromoCode>.Filter.Gt(q => q.ExpiryDate, DateTime.UtcNow);
            var foundedPromoCode = await _context.PromoCodes.Find(filter).FirstOrDefaultAsync();
            if (foundedPromoCode != null)
            {
                return true;
            }
            return false;
        }
    }
}
