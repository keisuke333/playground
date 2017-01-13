using System;

namespace MyLinq.MyCollections
{
	/// <summary>
	/// 単純なBinarySearchTreeでの実装
	/// </summary>
	public class MySortedDictionary<TKey, TValue> : IMyDictionary<TKey, TValue>
	{
		private readonly IMyComparer<TKey> _comparer;
		private Node _root;
		private Node Begin => Min(_root);

		public MySortedDictionary(IMyComparer<TKey> comparer)
		{
			_comparer = comparer;
		}

		public void Add(TKey key, TValue value)
		{
			if (key == null)
			{
				throw new ArgumentNullException();
			}

			if (_root == null)
			{
				_root = new Node(key, value, null);
				return;
			}

			var node = _root;
			var previous = node;
			while (node != null)
			{
				previous = node;
				var position = _comparer.Compare(node.Key, key);
				if (position > 0)
				{
					node = node.Left;
				}
				else if (position < 0)
				{
					node = node.Right;
				}
				else
				{
					throw new ArgumentException();
				}
			}

			node = new Node(key, value, previous);
			if (_comparer.Compare(previous.Key, key) > 0)
			{
				previous.Left = node;
			}
			else
			{
				previous.Right = node;
			}
		}

		public bool ContainsKey(TKey key)
		{
			if (key == null)
			{
				throw new ArgumentNullException();
			}

			return _root != null && Find(key) != null;
		}

		public bool Remove(TKey key)
		{
			if (key == null)
			{
				throw new ArgumentNullException();
			}

			var node = Find(key);
			if (node == null)
			{
				return false;
			}

			if (node.Left == null)
			{
				Replace(node, node.Right);
			}
			else if (node.Right == null)
			{
				Replace(node, node.Left);
			}
			else
			{
				var min = Min(node.Right);
				node.Key = min.Key;
				node.Value = min.Value;
				Replace(min, min.Right);
			}
			return true;
		}

		public bool TryGetValue(TKey key, out TValue value)
		{
			var node = Find(key);
			if (node == null)
			{
				value = default(TValue);
				return false;
			}
			value = node.Value;
			return false;
		}

		private void Replace(Node removeTarget, Node removeTargetChild)
		{
			var parent = removeTarget.Parent;
			if (removeTargetChild != null)
			{
				removeTargetChild.Parent = parent;
			}

			if (removeTarget == _root)
			{
				_root = removeTargetChild;
			}
			else if (parent.Left != null && parent.Left == removeTarget)
			{
				parent.Left = removeTargetChild;
			}
			else
			{
				parent.Right = removeTargetChild;
			}
		}

		private Node Find(TKey key)
		{
			var node = _root;
			while (node != null)
			{
				var position = _comparer.Compare(node.Key, key);
				if (position > 0)
				{
					node = node.Left;
				}
				else if (position < 0)
				{
					node = node.Right;
				}
				else
				{
					return node;
				}
			}
			return null;
		}

		private Node Min(Node node)
		{
			if (node == null)
			{
				return null;
			}

			while (node.Left != null)
			{
				node = node.Left;
			}
			return node;
		}

		private Node Next(Node node)
		{
			if (node == null)
			{
				return null;
			}

			if (node.Right != null)
			{
				return Min(node.Right);
			}

			while (node.Parent != null && node.Parent.Left != node)
			{
				node = node.Parent;
			}
			return node.Parent;
		}

		public IMyEnumerator<KeyValuPair<TKey, TValue>> GetMyEnumerator()
		{
			return new MySortedDictionaryEnumerator(this);
		}

		private sealed class Node
		{
			internal TKey Key;
			internal TValue Value;
			internal Node Left, Right, Parent;

			internal Node(TKey key, TValue value, Node parent)
			{
				Key = key;
				Value = value;
				Parent = parent;
				Left = Right = null;
			}
		}

		private sealed class MySortedDictionaryEnumerator : IMyEnumerator<KeyValuPair<TKey, TValue>>
		{
			private readonly MySortedDictionary<TKey, TValue> _sortedDictionary;
			private Node _current;

			public KeyValuPair<TKey, TValue> Current => new KeyValuPair<TKey, TValue>(_current.Key, _current.Value);

			public MySortedDictionaryEnumerator(MySortedDictionary<TKey, TValue> sortedDictionary)
			{
				_sortedDictionary = sortedDictionary;
			}

			public void Dispose()
			{
				// do nothing.
			}

			public bool MoveNext()
			{
				return (_current = _current == null ? _sortedDictionary.Begin : _sortedDictionary.Next(_current)) != null;
			}

			public void Reset()
			{
				_current = null;
			}
		}
	}
}