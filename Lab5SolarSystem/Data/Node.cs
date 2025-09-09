using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Lab5SolarSystem.Data
{
    [Serializable]
    public class Node<T>
    {
        private T _item;

        //[field:NonSerialized]
        private Node<T> _parentNode = default!;

        private List<Node<T>> _children = new List<Node<T>>();

        public Node()
        {
        }

        public Node(T item)
        {
            _item = item;
        }

        public Node(T item, Node<T> parentNode)
        {
            _item = item;
            _parentNode = parentNode;
        }

        public Node<T> SetParentNode(Node<T> parentNode)
        {
            ParentNode = parentNode;
            return this;
        }

        public void SetParentNodeInChilds()
        {
            foreach (Node<T> child in _children)
                child.SetParentNode(this);
        }

        public T Item
        {
            get { return _item; }
            set { _item = value; }
        }

        public List<Node<T>> Childrens { get => _children; set => _children = value; }

        [JsonIgnore]
        [XmlIgnore]
        [field: NonSerialized]
        public Node<T> ParentNode { get => _parentNode; set => _parentNode = value; }

        public Node<T> AddChildren(params T[] childrens)
        {
            Childrens.AddRange(childrens.Select(c => new Node<T>(c, this)));
            return this;
        }

        public void AddChildren(params Node<T>[] childrens)
        {
            foreach (Node<T> child in childrens)
                child.ParentNode = this;
            Childrens.AddRange(childrens);
        }
    }
}
