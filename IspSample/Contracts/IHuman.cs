namespace IspSample.Contracts
{
    // Super-Interface was mehrere Interfaces vereint
    // Nicht empfohlen. Warum?
    // Weil unendlich viele Kombinationen denkbar sind
    public interface IHuman : IChef, IMoveable, IEats
    {
    }
}