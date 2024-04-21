using BusinessLayer;

namespace BlazorServer.Authentication
{
    public class UserSession
    {
        public string UserName { get; set;}
        public Role Role {  get; set;}
    }
}
