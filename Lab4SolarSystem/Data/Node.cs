namespace Lab_SolarSystem.Data;

public class Node<T>
{
    public T Item { get; set; }

    public Node<T>? ParentNode { get; set; }

    public List<Node<T>> Childrens { get; set; } = [];

	public Node(T item, Node<T>? parentNode)
    {
        Item = item;
        ParentNode = parentNode;
        ParentNode?.Childrens.Add(this);
    }
}