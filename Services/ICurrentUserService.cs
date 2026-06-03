namespace ProjectService.Services;

public interface ICurrentUserService
{
    Guid? UserId { get; }
}
