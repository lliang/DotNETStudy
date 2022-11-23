using System;

namespace DotNETStudy.Algorithm.LinkedList
{
    public abstract class AbstractList<E> : IList<E>
    {
        protected int size;
        protected const int ElementNotFound = -1;

        public void Add(E element)
        {
            Add(size, element);
        }

        public virtual void Add(int index, E element)
        {
            throw new NotImplementedException();
        }

        public virtual void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(E element)
        {
            return IndexOf(element) != ElementNotFound;
        }

        public virtual E Get(int index)
        {
            throw new NotImplementedException();
        }

        public virtual int IndexOf(E element)
        {
            throw new NotImplementedException();
        }

        public bool IsEmpty()
        {
            return size == 0;
        }

        public virtual E Remove(int index)
        {
            throw new NotImplementedException();
        }

        public virtual E Set(int index, E element)
        {
            throw new NotImplementedException();
        }

        public int Size()
        {
            return size;
        }

        protected void OutOfBounds(int index)
        {
            throw new IndexOutOfRangeException($"Index: {index}, Size: {size}");
        }

        protected void RangeCheck(int index)
        {
            if (index < 0 || index >= size)
            {
                OutOfBounds(index);
            }
        }

        protected void RangeCheckForAdd(int index)
        {
            if (index < 0 || index > size)
            {
                OutOfBounds(index);
            }
        }
    }
}
