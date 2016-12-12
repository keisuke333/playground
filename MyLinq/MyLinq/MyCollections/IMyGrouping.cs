namespace MyLinq.MyCollections
{
	public interface IMyGrouping<out TKey, out TElement> : IMyEnumerable<TElement>
	{
		TKey Key { get; }
	}
}