// See https://aka.ms/new-console-template for more information
using System.Collections;

int[] nums1 = { 1, 2, 3, 4, 5 };
var roc = new ReadOnlyCollection(nums1);
foreach (var n in roc)
{
    Console.WriteLine(n);
}

class ReadOnlyCollection : IEnumerable
{
    private int[] _array;

    public ReadOnlyCollection(int[] array)
    {
        _array = array;
    }

    public IEnumerator GetEnumerator()
    {
        return new Enumerator(this);
    }

    class Enumerator : IEnumerator
    {
        private ReadOnlyCollection _collection;
        private int _index;

        public Enumerator(ReadOnlyCollection collection)
        {
            _collection = collection;
            _index = -1;
        }

        public object Current => _collection._array[_index];

        public bool MoveNext()
        {
            if (++_index < _collection._array.Length)
            {
                return true;
            }
            return false;
        }

        public void Reset()
        {
            _index = -1;
        }
    }
}