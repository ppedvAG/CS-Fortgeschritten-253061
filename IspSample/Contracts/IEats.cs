namespace IspSample.Contracts
{
    public interface IEats
    {
        string FavoriteFood { get; set; }

        void Eat();
    }
}