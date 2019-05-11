using System;
using System.Threading.Tasks;
using YaDemo.Model;

namespace YaDemo.Dal.Interface
{
    public interface IPromoCodeRepository
    {
        Task<PromoCode> GeneratePromoCode(NewPromoRequest newPromoCode);
        Task<bool> IsPromoCodeValid(string promoCode);
    }
}
