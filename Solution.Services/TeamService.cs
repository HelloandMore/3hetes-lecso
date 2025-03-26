using Microsoft.EntityFrameworkCore;
using Solution.DataBase;

namespace Solution.Services
{
    public class TeamService : ITeamService
    {
        private readonly AppDbContext dbContext;

        public TeamService(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ErrorOr<TeamModel>> CreateAsync(TeamModel model)
        {
            bool exists = await dbContext.Teams.AnyAsync(t => t.Name == model.Name.Value &&
                                                              t.CompetitionId == uint.Parse(model.Competition.Value.Id));

            if (exists)
            {
                return Error.Conflict(description: "Team already exists");
            }

            var team = model.ToEntity();
            team.PublicId = Guid.NewGuid().ToString();

            await dbContext.Teams.AddAsync(team);
            await dbContext.SaveChangesAsync();

            return new TeamModel(team)
            {
                Competition = model.Competition
            };
        }

        public async Task<ErrorOr<Success>> UpdateAsync(TeamModel model)
        {
            var result = await dbContext.Teams.AsNoTracking()
                                              .Include(t => t.Members)
                                              .Include(t => t.Competition)
                                              .Where(t => t.PublicId == model.Id)
                                              .ExecuteUpdateAsync(t => t.SetProperty(p => p.PublicId, model.Id)
                                                                        .SetProperty(p => p.Name, model.Name.Value)
                                                                        );

            return result > 0 ? Result.Success : Error.NotFound();
        }

        public async Task<ErrorOr<Success>> DeleteAsync(string teamId)
        {
            var result = await dbContext.Teams.AsNoTracking()
                                              .Include(t => t.Members)
                                              .Include(t => t.Competition)
                                              .Where(t => t.PublicId == teamId)
                                              .ExecuteDeleteAsync();
            return result > 0 ? Result.Success : Error.NotFound();
        }
    }
}
