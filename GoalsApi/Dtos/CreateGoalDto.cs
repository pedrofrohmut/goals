namespace GoalsApi.Dtos;

public class CreateGoalDto
{
    public string Text { get; set; } = "";

    public override string ToString() => $"Text: {Text}";
}
