namespace API_Auth.Modules.Shared
{
    public interface IUserHelper
    {
        Task<MyUser> GetMyUser();
    }
}
