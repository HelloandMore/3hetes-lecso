namespace Solution.Core.Interfaces;

public interface ITeamService
{
    Task<ErrorOr<TeamModel>> CreateAsync(TeamModel model);
    Task<ErrorOr<Success>> UpdateAsync(TeamModel model);
    Task<ErrorOr<Success>> DeleteAsync(string competitionId);
}
