using GoalsApi.Dtos;

namespace GoalsApi.UseCases;

public class GoalsUseCases
{
    public string AddGoal(CreateGoalDto newGoal) {
        return "Hello, Add Goal";
    }

    public string GetGoals() {
        return "Hello, Get Goals";
    }

    public string UpdateGoal(string id) {
        return "Hello, Update Goal";
    }

    public string DeleteGoal(string id) {
        return "Hello, Delete Goal";
    }
}
