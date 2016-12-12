namespace MyLinq.MyCollections
{
	public interface IMyComparer<in T>
	{
		int Compare(T x, T y);
	}
}