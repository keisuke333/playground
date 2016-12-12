using System;

namespace MyLinq.MyCollections
{
	// Open addressing
	public class MyHashMap<TKey, TValue> : IMyEnumerable<KeyValuPair<TKey, TValue>>
	{
		private readonly IMyEqualityComparer<TKey> _comparer;
		private int _index;
		private int[] _buckets;
		private Entry[] _entries;

		internal sealed class Entry
		{
			public int Next { get; set; }
			public TKey Key { get; set; }
			public TValue Value { get; set; }
		}

		public MyHashMap() : this(32)
		{
		}

		public MyHashMap(int bucketSize) : this(bucketSize, new DefaultEqualityComparer<TKey>())
		{
		}

		public MyHashMap(IMyEqualityComparer<TKey> comparer) : this(32, comparer)
		{
		}

		public MyHashMap(int bucketSize, IMyEqualityComparer<TKey> comparer)
		{
			_index = -1;
			_buckets = new int[bucketSize];
			_entries = new Entry[_buckets.Length];
			for (var i = 0; i < _buckets.Length; i++)
			{
				_buckets[i] = -1;
			}
			_comparer = comparer;
		}

		public TValue Add(TKey key, TValue value)
		{
			if (key == null)
			{
				throw new ArgumentNullException();
			}

			_index++;
			if (_index == _buckets.Length/3)
			{
				ExpandBucket();
			}

			var bucketIndex = ToIndex(key.GetHashCode());
			if (_buckets[bucketIndex] == -1)
			{
				_buckets[bucketIndex] = _index;
				_entries[_index] = new Entry
				{
					Next = -1,
					Key = key,
					Value = value
				};
				return value;
			}

			var existingEntry = _entries[_buckets[bucketIndex]];
			if (_comparer.Equals(key, existingEntry.Key))
			{
				throw new ArgumentException("key is already exists.");
			}
			while (existingEntry.Next != -1)
			{
				existingEntry = _entries[existingEntry.Next];
				if (_comparer.Equals(key, existingEntry.Key))
				{
					throw new ArgumentException("key is already exists.");
				}
			}

			existingEntry.Next = _index;
			_entries[_index] = new Entry
			{
				Next = -1,
				Key = key,
				Value = value
			};
			return value;
		}

		public bool TryGetValue(TKey key, out TValue value)
		{
			var bucketIndex = ToIndex(key.GetHashCode());
			if (_buckets[bucketIndex] == -1)
			{
				value = default(TValue);
				return false;
			}

			var entry = _entries[_buckets[bucketIndex]];
			if (_comparer.Equals(key, entry.Key))
			{
				value = entry.Value;
				return true;
			}
			while (entry.Next != -1)
			{
				entry = _entries[entry.Next];
				if (_comparer.Equals(key, entry.Key))
				{
					value = entry.Value;
					return true;
				}
			}

			value = default(TValue);
			return false;
		}

		public bool ContainsKey(TKey key)
		{
			var bucketIndex = ToIndex(key.GetHashCode());
			return _buckets[bucketIndex] != -1;
		}

		public void Clear()
		{
			_index = -1;
			_buckets = new int[32];
			_entries = new Entry[_buckets.Length];
			for (var i = 0; i < _buckets.Length; i++)
			{
				_buckets[i] = -1;
			}
		}

		public int Size()
		{
			return _index + 1;
		}

		public IMyEnumerator<KeyValuPair<TKey, TValue>> GetMyEnumerator()
		{
			var arg = new Entry[_index + 1];
			Array.Copy(_entries, arg, _index + 1);
			return new HashMapEnumerator<TKey, TValue>(arg);
		}

		private int ToIndex(int hash)
		{
			return (hash & 0x7FFFFFFF)%_buckets.Length;
		}

		private void ExpandBucket()
		{
			var nextBuckets = new int[_buckets.Length*3];
			Array.Copy(_buckets, nextBuckets, _buckets.Length/3);
			_buckets = nextBuckets;

			var nextEntries = new Entry[_buckets.Length];
			Array.Copy(_entries, nextEntries, _entries.Length);
			_entries = nextEntries;
		}
	}

	internal sealed class HashMapEnumerator<TKey, TValue> : IMyEnumerator<KeyValuPair<TKey, TValue>>
	{
		private readonly MyHashMap<TKey, TValue>.Entry[] _entries;
		private int _index;

		public KeyValuPair<TKey, TValue> Current
			=> new KeyValuPair<TKey, TValue>(_entries[_index].Key, _entries[_index].Value);

		public HashMapEnumerator(MyHashMap<TKey, TValue>.Entry[] entries)
		{
			_entries = entries;
			_index = -1;
		}

		public void Dispose()
		{
		}


		public bool MoveNext()
		{
			_index++;
			return _index != _entries.Length;
		}

		public void Reset()
		{
			_index = -1;
		}
	}
}