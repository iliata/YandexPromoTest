namespace YaDemo.Exception
{
    public class UserRequestException : System.Exception
    {
        public UserRequestException(string message)
            : base(message)
        {
        }
    }
}
