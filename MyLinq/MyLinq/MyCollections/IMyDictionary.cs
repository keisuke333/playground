namespace MyLinq.MyCollections
{
	public interface IMyDictionary<TKey, TValue> : IMyEnumerable<KeyValuPair<TKey, TValue>>
	{
		void Add(TKey key, TValue value);
		bool ContainsKey(TKey key);
		bool Remove(TKey key);
		bool TryGetValue(TKey key, out TValue value);
	}
}