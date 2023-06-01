using CargoApi.Model;

namespace CargoApi
{
    public class UserConstants
    {
        public static List<UserModel> Users = new()
            {
               new UserModel(){ Username= "selahattin", Password= "123", Role= "User" }
            };
    }
}
