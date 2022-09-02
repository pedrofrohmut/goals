using GoalsApi.Dtos;

namespace GoalsApi.DataAccess;

public interface GoalDataAccess
{
    void AddGoal(CreateGoalDto newGoal, Guid userId);
}
