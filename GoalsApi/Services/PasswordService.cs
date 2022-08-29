namespace GoalsApi.Services
{
    public interface PasswordService
    {
        string GetPasswordHash(string password);
        bool ComparePasswordAndHash(string password, string hash);
    }
}
