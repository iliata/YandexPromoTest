using System;

namespace YaDemo.Model
{
    [Serializable]
    public class UserRequestData
    {
        public int RequestCounter { get; set; }

        public DateTime RequestDate { get; set; }
    }
}
