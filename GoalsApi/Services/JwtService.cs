namespace GoalsApi.Services 
{
    public interface JwtService
    {
        string GenerateJWT(Guid id);
    }
}
