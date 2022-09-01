namespace GoalsApi.Dtos;

public class UserDbDto
{
    public Guid Id { get; set; } = Guid.Empty;
    public string Name { get; set; } = "";
    public string Email { get; set; } = "";
    public string PasswordHash { get; set; } = "";
    public string Phone { get; set; } = "";

    public override string ToString() => 
        $"Id: {Id.ToString()}, Name: {Name}, Email: {Email}, Phone: {Phone}, " +
        $"PasswordHash: {PasswordHash}, Phone: {Phone}";
}
