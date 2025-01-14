using Microsoft.AspNetCore.Identity;

namespace API_Auth.Modules.Shared
{
    public class UserHelper : IUserHelper
    {
        
        private readonly UserManager<MyUser> _manager;
        private readonly IHttpContextAccessor _httpContextAccessor; 

        public UserHelper(IHttpContextAccessor httpContextAccessor, UserManager<MyUser> manager)
        {
            _manager = manager;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<MyUser> GetMyUser()
        {
            return await _manager.GetUserAsync(_httpContextAccessor.HttpContext.User);
        }
    }
}
