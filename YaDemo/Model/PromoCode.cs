using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YaDemo.Model
{
    public class PromoCode
    {
        [BsonId]
        public Guid Id { get; set; }
        public string Code { get; set; }
        public decimal Value { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
