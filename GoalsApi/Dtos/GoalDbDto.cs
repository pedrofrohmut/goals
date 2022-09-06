namespace GoalsApi.Dtos;

public class GoalDbDto
{
    public Guid Id { get; set; } = Guid.Empty;
    public string Text { get; set; } = "";
    public Guid UserId { get; set; } = Guid.Empty;

    public override string ToString() => $"Id: {Id}, Text: {Text}, UserId: {UserId}";
}
