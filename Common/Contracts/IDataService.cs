using Common.Services;

namespace Common.Contracts
{
    public interface IDataService<T>
    {
        event EventHandler<DataChangedEventArgs<T>> DataChanged;

        List<T> Data { get; }
    }
}