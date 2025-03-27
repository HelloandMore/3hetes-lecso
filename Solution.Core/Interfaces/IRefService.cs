namespace Solution.Core.Interfaces;

public interface IRefService
{
    Task<ErrorOr<RefModel>> CreateAsync(RefModel model);
    Task<ErrorOr<Success>> UpdateAsync(RefModel model);
    Task<ErrorOr<Success>> DeleteAsync(string refId);

    Task<ErrorOr<List<RefModel>>> GetAllAsync();

}
