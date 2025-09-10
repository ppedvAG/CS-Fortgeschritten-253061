using Common.Models;

namespace Common.Contracts
{
    public interface ITransportService
    {
        void Load(string brandName);
        List<Car>? Unload();
        void ShowInfo();
    }
}