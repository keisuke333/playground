namespace MyLinq.MyCollections
{
	public interface IMyEnumerable<out T>
	{
		IMyEnumerator<T> GetMyEnumerator();
	}
}