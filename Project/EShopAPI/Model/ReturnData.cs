namespace EShopAPIClone.Model
{
    public class ReturnData
    {
    }
    public class UserLoginReturnData
    {
        public string userName { get; set; }
        public string token { get; set; }
        public string refreshToken { get; set; }
        public int IsAdmin { get; set; }
    }
}
