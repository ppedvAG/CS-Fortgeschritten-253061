namespace DesignPatterns.Adapter;

public class PanPizza()
{
    public string Name => "NY Style Pfannen-Pizza";

    public void PutOilInPan()
    {
        Console.WriteLine("Put oil in pan");
    }

    public void FryInPan()
    {
        Console.WriteLine("Fry in pan");
    }
}
