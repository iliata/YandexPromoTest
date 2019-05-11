using Microsoft.Extensions.Options;
using ServiceStack.Redis;
using System;
using YaDemo.Constants;
using YaDemo.Dal.Interface;
using YaDemo.Exception;
using YaDemo.Model;

namespace YaDemo.Dal
{
    public class RequestCounterStore : IRequestCounterStore
    {
        private readonly string _host;
        
        public RequestCounterStore(IOptions<RedisSettings> settings)
        {
            _host = settings.Value.Host;
        }

        public void CheckUserRequest(Guid userToken)
        {
            try
            {
                using (RedisClient redisClient = new RedisClient(_host))
                {
                    var redis = redisClient.As<UserRequestData>();
                    var userRequestData = new UserRequestData();
                    var requestData = redis.GetValue(userToken.ToString());
                    if (requestData == null)
                    {
                        userRequestData.RequestCounter = 1;
                    }
                    else
                    {
                        userRequestData = requestData;
                        var dateTimeDiff = (DateTime.UtcNow - userRequestData.RequestDate).TotalSeconds;

                        if (userRequestData.RequestCounter >= UserRequestConstant.RequestTryCounter)
                        {
                            if (dateTimeDiff >= UserRequestConstant.MaxDelayTimer)
                            {
                                userRequestData.RequestCounter = 1;
                            }
                            else
                            {
                                throw new UserRequestException($"Too many request from user with token {userToken}. " +
                                    $"Wait 2 minutes.");
                            }
                        }
                        else
                        {
                            if (dateTimeDiff > UserRequestConstant.MinDelayTimer 
                                && userRequestData.RequestCounter < UserRequestConstant.RequestTryCounter)
                            {
                                userRequestData.RequestCounter = 1;
                            }
                            else
                            {
                                userRequestData.RequestCounter++;
                            }
                        }
                    }
                    userRequestData.RequestDate = DateTime.UtcNow;
                    redis.SetValue(userToken.ToString(), userRequestData);
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
