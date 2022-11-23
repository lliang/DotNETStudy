namespace DotNETStudy.Algorithm.BinarySearchTree
{
    public class Node<E>
    {
        public E Element { get; set; }

        public Node<E> Left { get; set; }

        public Node<E> Right { get; set; }

        public Node<E> Parent { get; set; }

        public Node(E element, Node<E> parent)
        {
            Element = element;
            Parent = parent;
        }
    }
}
