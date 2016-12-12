namespace MyLinq.MyCollections
{
	public class KeyValuPair<TKey, TValue>
	{
		public TKey Key { get; }
		public TValue Value { get; }

		public KeyValuPair(TKey key, TValue value)
		{
			Key = key;
			Value = value;
		}
	}
}