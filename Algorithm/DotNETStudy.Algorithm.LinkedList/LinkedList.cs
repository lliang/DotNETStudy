namespace DotNETStudy.Algorithm.LinkedList
{
    public class LinkedList<E> : AbstractList<E>
    {
        private Node<E> first;
        private Node<E> last;

        public override void Clear()
        {
            size = 0;
            first = null;
            last = null;
        }

        public override E Get(int index)
        {
            return FindNode(index).Element;
        }

        public override E Set(int index, E element)
        {
            Node<E> node = FindNode(index);
            E old = node.Element;
            node.Element = element;
            return old;
        }

        public override void Add(int index, E element)
        {
            RangeCheckForAdd(index);

            if (index == size)
            {
                Node<E> oldLast = last;
                last = new Node<E>(oldLast, element, null);
                if (oldLast == null)
                {
                    first = last;
                }
                else
                {
                    oldLast.Next = last;
                }
            }
            else
            {
                Node<E> next = FindNode(index);
                Node<E> prev = next.Prev;
                Node<E> node = new Node<E>(prev, element, next);

                next.Prev = node;

                if (prev == null)
                {
                    first = node;
                }
                else
                {
                    prev.Next = node;
                }
            }
        }

        public override E Remove(int index)
        {
            RangeCheck(index);

            Node<E> node = FindNode(index);
            Node<E> prev = node.Prev;
            Node<E> next = node.Next;

            if (prev == null)
            {
                first = next;
            }
            else
            {
                prev.Next = next;
            }

            if (next == null)
            {
                last = prev;
            }
            else
            {
                next.Prev = prev;
            }

            size--;
            return node.Element;
        }

        public override int IndexOf(E element)
        {
            Node<E> node = first;

            for (int i = 0; i < size; i++)
            {
                if (element == null)
                {
                    if (node.Element == null)
                    {
                        return i;
                    }
                }
                else
                {
                    if (element.Equals(node.Element))
                    {
                        return i;
                    }
                }

                node = node.Next;
            }

            return ElementNotFound;
        }

        private Node<E> FindNode(int index)
        {
            RangeCheck(index);

            Node<E> currentNode;

            if (index <= (size >> 1))
            {
                currentNode = first;
                for (int i = 0; i < index; i++)
                {
                    currentNode = currentNode.Next;
                }
            }
            else
            {
                currentNode = last;
                for (int i = size - 1; i > index; i++)
                {
                    currentNode = currentNode.Prev;
                }
            }

            return currentNode;
        }
    }
}
