using GoalsApi.DataAccess;
using GoalsApi.Dtos;

namespace GoalsApi.UseCases.Goals;

public class AddGoalUseCase
{
    private readonly UserDataAccess userDataAccess;
    private readonly GoalDataAccess goalDataAccess;

    public AddGoalUseCase(UserDataAccess userDataAccess, GoalDataAccess goalDataAccess) 
    {
        this.userDataAccess = userDataAccess;
        this.goalDataAccess = goalDataAccess;
    }

    public void Execute(CreateGoalDto newGoal, Guid authUserId) 
    {
        ValidateNewGoal(newGoal);
        CheckUserExists(authUserId);
        CreateGoal(newGoal, authUserId);
    }

    private void ValidateNewGoal(CreateGoalDto newGoal) 
    {
        if (string.IsNullOrWhiteSpace(newGoal.Text)) {
            throw new ArgumentException("Please inform all fields for the new goal");
        }
    }
    
    private void CheckUserExists(Guid userId) 
    {
        var user = userDataAccess.FindUserById(userId);
        if (user == null) {
            throw new ArgumentException("User not found by id");
        }
    }

    private void CreateGoal(CreateGoalDto newGoal, Guid userId) 
    {
        goalDataAccess.AddGoal(newGoal, userId);
    }
}
