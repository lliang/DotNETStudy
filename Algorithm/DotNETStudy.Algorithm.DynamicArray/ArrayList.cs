using System;

namespace DotNETStudy.Algorithm.DynamicArray
{
    public class ArrayList<E> where E : IComparable<E>
    {
        /// <summary>
        /// 元素的数量
        /// </summary>
        private int size;

        /// <summary>
        /// 所有元素
        /// </summary>
        private E[] elements;

        private const int DefaultCapacity = 10;
        private const int ElementNotFound = -1;

        public ArrayList(int capacity)
        {
            capacity = (capacity < DefaultCapacity) ? DefaultCapacity : capacity;
            elements = new E[capacity];
        }

        public ArrayList() : this(DefaultCapacity)
        {
        }

        public void Clear()
        {
            for (int i = 0; i < size; i++)
            {
                elements[i] = default;
            }
            size = 0;
        }

        public int Size()
        {
            return size;
        }

        public bool IsEmpty()
        {
            return size == 0;
        }

        public bool Contains(E element)
        {
            return IndexOf(element) != ElementNotFound;
        }

        public void Add(E element)
        {
            Add(size, element);
        }

        public E Get(int index)
        {
            RangeCheck(index);
            return elements[index];
        }

        public E Set(int index, E element)
        {
            RangeCheck(index);

            E old = elements[index];
            elements[index] = element;

            return old;
        }

        public void Add(int index, E element)
        {
            RangeCheckForAdd(index);

            EnsureCapacity(size + 1);

            for (int i = size; i > index; i++)
            {
                elements[i] = elements[i - 1];
            }

            elements[index] = element;
            size++;
        }

        public E Remove(int index)
        {
            RangeCheck(index);

            E element = elements[index];

            for (int i = index + 1; i < size; i++)
            {
                elements[i - 1] = elements[i];
            }

            elements[--size] = default;

            return element;
        }

        public int IndexOf(E element)
        {
            if (element == null)
            {
                for (int i = 0; i < size; i++)
                {
                    if (elements[i] == null)
                    {
                        return i;
                    }
                }
            }
            else
            {
                for (int i = 0; i < size; i++)
                {
                    if (element.Equals(elements[i]))
                    {
                        return i;
                    }
                }
            }

            return ElementNotFound;
        }

        private void OutOfBounds(int index)
        {
            throw new IndexOutOfRangeException($"Index: {index}, Size: {size}");
        }

        private void RangeCheck(int index)
        {
            if (index < 0 || index >= size)
            {
                OutOfBounds(index);
            }
        }

        private void RangeCheckForAdd(int index)
        {
            if (index < 0 || index > size)
            {
                OutOfBounds(index);
            }
        }

        /// <summary>
        /// 扩容，保证要有参数指定的容量
        /// </summary>
        /// <param name="capacity">容量</param>
        private void EnsureCapacity(int capacity)
        {
            int oldCapacity = elements.Length;
            if (oldCapacity >= capacity)
            {
                return;
            }

            int newCapacity = oldCapacity + (oldCapacity >> 1);
            E[] newElements = new E[newCapacity];
            for (int i = 0; i < size; i++)
            {
                newElements[i] = elements[i];
            }

            elements = newElements;

            Console.WriteLine($"{oldCapacity} 扩容为：{newCapacity}");
        }
    }
}
