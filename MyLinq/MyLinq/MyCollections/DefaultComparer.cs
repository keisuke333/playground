using System;

namespace MyLinq.MyCollections
{
	public class DefaultComparer<T> : IMyComparer<T> where T : IComparable
	{
		public int Compare(T x, T y)
		{
			return x.CompareTo(y);
		}
	}
}