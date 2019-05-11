using System;

namespace YaDemo.Dal.Interface
{
    public interface IRequestCounterStore
    {
        void CheckUserRequest(Guid userToken);
    }
}
