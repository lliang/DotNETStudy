namespace DotNETStudy.Algorithm.LinkedList
{
    public class Node<E>
    {
        public E Element { get; set; }
        public Node<E> Next { get; set; }
        public Node<E> Prev { get; set; }

        public Node(Node<E> prev, E element, Node<E> next)
        {
            Prev = prev;
            Element = element;
            Next = next;
        }
    }
}
