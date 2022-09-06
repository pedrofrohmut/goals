using GoalsApi.DataAccess;

namespace GoalsApi.UseCases.Goals;

public class DeleteGoalUseCase
{
    private readonly UserDataAccess userDataAccess;
    private readonly GoalDataAccess goalDataAccess;

    public DeleteGoalUseCase(UserDataAccess userDataAccess, GoalDataAccess goalDataAccess) 
    {
        this.userDataAccess = userDataAccess;
        this.goalDataAccess = goalDataAccess;
    }

    public void Execute(Guid goalId, Guid authUserId) 
    {
        CheckUserExists(authUserId);
        DeleteGoal(goalId);
    }

    private void CheckUserExists(Guid userId) 
    {
        var user = userDataAccess.FindUserById(userId);
        if (user == null) {
            throw new ArgumentException("User not found by id");
        }
    }
    
    private void DeleteGoal(Guid goalId) 
    {
        goalDataAccess.DeleteGoalById(goalId);
    }
}
