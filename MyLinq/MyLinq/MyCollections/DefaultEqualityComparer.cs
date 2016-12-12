namespace MyLinq.MyCollections
{
	public class DefaultEqualityComparer<TKey> : IMyEqualityComparer<TKey>
	{
		public bool Equals(TKey x, TKey y)
		{
			return object.Equals(x, y);
		}

		public int GetHashCode(TKey obj)
		{
			return obj.GetHashCode();
		}
	}
}