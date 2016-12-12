using System;

namespace MyLinq.MyCollections
{
	public interface IMyEnumerator<out T> : IDisposable
	{
		T Current { get; }
		bool MoveNext();
		void Reset();
	}
}