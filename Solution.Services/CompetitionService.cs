using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Solution.DataBase;

namespace Solution.Services
{
    public class CompetitionService(AppDbContext dbContext) : ICompetitionService
    {
        private const int ROW_COUNT = 10;

        public async Task<ErrorOr<CompetitionModel>> CreateAsync(CompetitionModel model)
        {
            bool exists = await dbContext.Competitions.AnyAsync(c => c.LocationId == model.Location.Value.Id &&
                                                                     c.Date == model.Date.Value &&
                                                                     c.Name == model.Name.Value);

            if (exists)
            {
                return Error.Conflict(description: "Competition already exists");
            }

            var competition = model.ToEntity();
            competition.PublicId = Guid.NewGuid().ToString();

            await dbContext.Competitions.AddAsync(competition);
            await dbContext.SaveChangesAsync();

            return new CompetitionModel(competition)
            {
                Location = model.Location
            };
        }

        public async Task<ErrorOr<Success>> UpdateAsync(CompetitionModel model)
        {
            var result = await dbContext.Competitions.AsNoTracking()
                                                     .Include(c => c.Location)
                                                     .Where(c => c.PublicId == model.Id)
                                                     .ExecuteUpdateAsync(c => c.SetProperty(p => p.PublicId, model.Id)
                                                                               .SetProperty(p => p.Name, model.Name.Value)
                                                                               .SetProperty(p => p.Date, model.Date.Value));

            return result > 0 ? Result.Success : Error.NotFound();
        }

        public async Task<ErrorOr<Success>> DeleteAsync(string competitionId)
        {
            var result = await dbContext.Competitions.AsNoTracking()
                                                     .Include(c => c.Location)
                                                     .Where(c => c.PublicId == competitionId)
                                                     .ExecuteDeleteAsync();

            return result > 0 ? Result.Success : Error.NotFound();
        }

        public async Task<ErrorOr<CompetitionModel>> GetByIdAsync(string competitionId)
        {
            var competition = await dbContext.Competitions.Include(c => c.Location)
                                                          .FirstOrDefaultAsync(c => c.PublicId == competitionId);

            if (competition is null)
            {
                return Error.NotFound(description: "Competition not found");
            }

            return new CompetitionModel(competition);
        }

        public async Task<ErrorOr<List<CompetitionModel>>> GetAllAsync() =>
            await dbContext.Competitions.AsNoTracking()
                                        .Include(c => c.Location)
                                        .Select(c => new CompetitionModel(c))
                                        .ToListAsync();

        public async Task<ErrorOr<PaginationModel<CompetitionModel>>> GetPagedAsync(int page = 0)
        {
            page = page < 0 ? 0 : page - 1;

            var competitions = await dbContext.Competitions.AsNoTracking()
                                                          .Include(c => c.Location)
                                                          .Skip(page * ROW_COUNT)
                                                          .Take(ROW_COUNT)
                                                          .Select(c => new CompetitionModel(c))
                                                          .ToListAsync();

            var paginationModel = new PaginationModel<CompetitionModel>
            {
                Items = competitions,
                Count = await dbContext.Competitions.CountAsync()
            };

            return paginationModel;
        }
    }
}
