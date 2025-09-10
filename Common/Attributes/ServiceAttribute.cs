namespace Common.Attributes;

public class ServiceAttribute : Attribute
{
    public string Description { get; }

    public int Order { get; }

    public ServiceAttribute(string description, int order = 0)
    {
        Description = description;
        Order = order;
    }
}
