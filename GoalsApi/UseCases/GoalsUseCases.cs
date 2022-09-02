using GoalsApi.Dtos;

namespace GoalsApi.UseCases;

public class GoalsUseCases
{
    public string AddGoal(CreateGoalDto newGoal) {
        return "Hello, Add Goal";
    }

    public string GetGoals(Guid id) {
        return "Hello, Get Goals";
    }

    public string UpdateGoal(Guid id) {
        return "Hello, Update Goal";
    }

    public string DeleteGoal(Guid id) {
        return "Hello, Delete Goal";
    }
}
