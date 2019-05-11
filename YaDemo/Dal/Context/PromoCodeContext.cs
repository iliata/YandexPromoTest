using Microsoft.Extensions.Options;
using MongoDB.Driver;
using YaDemo.Model;

namespace YaDemo.Dal.Context
{
    public class PromoCodeContext
    {
        private readonly IMongoDatabase _database;
        
        public PromoCodeContext(IOptions<MongoSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<PromoCode> PromoCodes => _database.GetCollection<PromoCode>("PromoCodeDB");
    }
}
