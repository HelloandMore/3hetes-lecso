using Solution.Database;
using Microsoft.EntityFrameworkCore;
using Solution.DataBase;

namespace Solution.Services;

public class RefService : IRefService
{
    private readonly AppDbContext dbContext;

    public RefService(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<ErrorOr<RefModel>> CreateAsync(RefModel model)
    {
        bool exists = await dbContext.Refs.AnyAsync(r => r.Name == model.Name.Value &&
                                                          r.PhoneNumber == model.PhoneNumber.Value &&
                                                          r.Email == model.Email.Value &&
                                                          r.CompetitionId == uint.Parse(model.Competition.Value.Id));
    
        if(exists)
        {
            return Error.Conflict(description: "Team already exists");
        }

        var reff = model.ToEntity();
        reff.PublicId = Guid.NewGuid().ToString();

        await dbContext.Refs.AddAsync(reff);
        await dbContext.SaveChangesAsync();

        return new RefModel(reff)
        {
            Competition = model.Competition
        };
    


    }

    public async Task<ErrorOr<List<RefModel>>> GetAllAsync() =>
        await dbContext.Refs.AsNoTracking()
                            .Include(r => r.Competition)
                            .Select(r => new RefModel(r))
                            .ToListAsync();

    public async Task<ErrorOr<Success>> UpdateAsync(RefModel model)
    {
        var result = await dbContext.Refs.AsNoTracking()
                                         .Include(r => r.Competition)
                                         .Where(r => r.PublicId == model.Id)
                                         .ExecuteUpdateAsync(r => r.SetProperty(r => r.PublicId, model.Id)
                                                                     .SetProperty(r => r.Name, model.Name.Value)
                                                                     .SetProperty(r => r.PhoneNumber, model.PhoneNumber.Value)
                                                                     .SetProperty(r => r.Email, model.Email.Value));

        return result > 0 ? Result.Success : Error.NotFound();

    }

    public async Task<ErrorOr<Success>> DeleteAsync(string refId)
    {
        var result = await dbContext.Refs.AsNoTracking()
                                         .Include(r => r.Name)
                                         .Include(r => r.PhoneNumber)
                                         .Include(r => r.Email)
                                         .Where(r => r.PublicId == refId)
                                         .ExecuteDeleteAsync();
        return result > 0 ? Result.Success : Error.NotFound();

    }

}
