using System;

namespace YaDemo.Model
{
    public class NewPromoRequest
    {
        public decimal Value { get; set; }

        public DateTime ExpiryDate { get; set; }

        public string Code { get; set; }
    }

}
