using System;

namespace MyLinq.MyCollections
{
	public interface IMyOrderedEnumerable<out T> : IMyEnumerable<T>
	{
		IMyOrderedEnumerable<T> CreateOrderedEnumerable<TKey>(Func<T, TKey> keySelector, IMyComparer<TKey> comparer,
			bool descending);
	}
}