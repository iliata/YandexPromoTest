using System;
using System.Text;

namespace YaDemo.Helpers
{
    public static class PromoCodeHelper
    {
        public static string RandomString(int size, bool isUpperCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (isUpperCase)
                return builder.ToString().ToUpper();
            return builder.ToString();
        }
    }
}
