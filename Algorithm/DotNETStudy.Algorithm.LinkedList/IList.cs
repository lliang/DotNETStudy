namespace DotNETStudy.Algorithm.LinkedList
{
    public interface IList<E>
    {
        void Clear();

        int Size();

        bool IsEmpty();

        bool Contains(E element);

        void Add(E element);

        E Get(int index);

        E Set(int index, E element);

        void Add(int index, E element);

        E Remove(int index);

        int IndexOf(E element);
    }
}
