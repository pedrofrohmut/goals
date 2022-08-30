using GoalsApi.Dtos;

namespace GoalsApi.UseCases;

public class GoalsUseCases
{
    public async Task<string> AddGoal(CreateGoalDto newGoal) {
        return "Hello, Add Goal";
    }

    public async Task<string> GetGoals() {
        return "Hello, Get Goals";
    }

    public async Task<string> UpdateGoal(string id) {
        return "Hello, Update Goal";
    }

    public async Task<string> DeleteGoal(string id) {
        return "Hello, Delete Goal";
    }
}
