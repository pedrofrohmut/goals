using GoalsApi.DataAccess;
using GoalsApi.Dtos;

namespace GoalsApi.UseCases.Goals;

public class GetGoalsUseCase
{
    private readonly UserDataAccess userDataAccess;
    private readonly GoalDataAccess goalDataAccess;

    public GetGoalsUseCase(UserDataAccess userDataAccess, GoalDataAccess goalDataAccess) 
    {
        this.userDataAccess = userDataAccess;
        this.goalDataAccess = goalDataAccess;
    }

    public IEnumerable<GoalDbDto> Execute(Guid authUserId) 
    {
        CheckUserExists(authUserId);
        var goals = GetGoals(authUserId);
        return goals;
    }

    private void CheckUserExists(Guid userId) 
    {
        var user = userDataAccess.FindUserById(userId);
        if (user == null) {
            throw new ArgumentException("User not found by id");
        }
    }
    
    private IEnumerable<GoalDbDto> GetGoals(Guid authUserId) 
    {
        var goals = goalDataAccess.GetGoalsByUserId(authUserId);
        return goals;
    }
}
