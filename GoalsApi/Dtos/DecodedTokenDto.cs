namespace GoalsApi.Dtos;

public class DecodedTokenDto
{
    public Guid UserId { get; set; }

    public override string ToString() => $"UserId: {UserId}";
}
