using ReadingBookApi.Model;


namespace ReadingBookApi.Repository
{
    public interface IUser
    {
        Task<UserManageResponse> RegisterAsync(RegisterVM register);
        Task<UserManageResponse> LoginAsync(LoginVM login);
    }
}
