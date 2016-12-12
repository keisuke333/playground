using System;

namespace MyLinq.MyCollections
{
	public class MyArrayList<TElement> : IMyEnumerable<TElement>
	{
		private TElement[] _elements;
		private int _index;

		public MyArrayList() : this(4)
		{
		}

		public MyArrayList(int initialSize)
		{
			_elements = new TElement[initialSize];
			_index = -1;
		}

		public void Add(TElement element)
		{
			_index++;
			if (_index == _elements.Length)
			{
				var tmp = new TElement[_index*2];
				Array.Copy(_elements, tmp, _index);
				_elements = tmp;
			}
			_elements[_index] = element;
		}

		public TElement Get(int index)
		{
			return _elements[index];
		}

		public IMyEnumerator<TElement> GetMyEnumerator()
		{
			var arg = new TElement[_index + 1];
			Array.Copy(_elements, arg, _index + 1);
			return new ArrayListEnumerator<TElement>(arg);
		}

		public static MyArrayList<TElement> EmptyList()
		{
			return new MyArrayList<TElement>(0);
		}
	}

	internal sealed class ArrayListEnumerator<TElement> : IMyEnumerator<TElement>
	{
		private readonly TElement[] _elements;
		private int _index;

		public TElement Current => _elements[_index];

		public ArrayListEnumerator(TElement[] elements)
		{
			_elements = elements;
			_index = -1;
		}

		public void Dispose()
		{
		}

		public bool MoveNext()
		{
			_index++;
			return _index != _elements.Length;
		}

		public void Reset()
		{
			_index = -1;
		}
	}
}