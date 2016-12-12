namespace MyLinq.MyCollections
{
	public interface IMyEqualityComparer<in T>
	{
		bool Equals(T x, T y);
		int GetHashCode(T obj);
	}
}