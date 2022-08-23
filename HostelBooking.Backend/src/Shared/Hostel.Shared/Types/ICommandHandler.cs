namespace Hostel.Shared.Types
{
    public interface ICommandHandler<in TCommand> where TCommand : class, ICommand
    {
        Task HandleAsync(TCommand command, CancellationToken cancellationToken);
    }
}
