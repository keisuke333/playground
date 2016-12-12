using System;
using MyLinq.MyCollections;

namespace MyLinq
{
	public static class MyLinq
	{
		public static bool Any<TSource>(this IMyEnumerable<TSource> source)
		{
			if (source == null) throw new ArgumentNullException();
			using (var enumerator = source.GetMyEnumerator())
			{
				return enumerator.MoveNext();
			}
		}

		public static bool Any<TSource>(this IMyEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null || predicate == null) throw new ArgumentNullException();
			using (var enumerator = source.GetMyEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (predicate(enumerator.Current))
					{
						return true;
					}
				}
			}
			return false;
		}

		public static bool All<TSource>(this IMyEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null || predicate == null) throw new ArgumentNullException();
			using (var enumerator = source.GetMyEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!predicate(enumerator.Current))
					{
						return false;
					}
				}
			}
			return true;
		}

		public static int Count<TSource>(this IMyEnumerable<TSource> source)
		{
			if (source == null) throw new ArgumentNullException();
			var count = 0;
			using (var enumerator = source.GetMyEnumerator())
			{
				while (enumerator.MoveNext())
				{
					count++;
				}
			}
			return count;
		}

		public static int Count<TSource>(this IMyEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null || predicate == null) throw new ArgumentNullException();
			var count = 0;
			using (var enumerator = source.GetMyEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (predicate(enumerator.Current))
					{
						count++;
					}
				}
			}
			return count;
		}

		public static TSource First<TSource>(this IMyEnumerable<TSource> source)
		{
			if (source == null) throw new ArgumentNullException();
			using (var enumerator = source.GetMyEnumerator())
			{
				if (enumerator.MoveNext())
				{
					return enumerator.Current;
				}
			}
			throw new InvalidOperationException();
		}

		public static TSource First<TSource>(this IMyEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null || predicate == null) throw new ArgumentNullException();
			using (var enumerator = source.GetMyEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (predicate(enumerator.Current))
					{
						return enumerator.Current;
					}
				}
			}
			throw new InvalidOperationException();
		}

		public static TSource FirstOrDefault<TSource>(this IMyEnumerable<TSource> source)
		{
			if (source == null) throw new ArgumentNullException();
			using (var enumerator = source.GetMyEnumerator())
			{
				if (enumerator.MoveNext())
				{
					return enumerator.Current;
				}
			}
			return default(TSource);
		}

		public static TSource FirstOrDefault<TSource>(this IMyEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null || predicate == null) throw new ArgumentNullException();
			using (var enumerator = source.GetMyEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (predicate(enumerator.Current))
					{
						return enumerator.Current;
					}
				}
			}
			return default(TSource);
		}

		public static TSource Single<TSource>(this IMyEnumerable<TSource> source)
		{
			if (source == null) throw new ArgumentNullException();
			using (var enumerator = source.GetMyEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					throw new InvalidOperationException();
				}
				var current = enumerator.Current;
				if (!enumerator.MoveNext())
				{
					return current;
				}
			}
			throw new InvalidOperationException();
		}

		public static TSource Single<TSource>(this IMyEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null) throw new ArgumentNullException();
			using (var enumerator = source.GetMyEnumerator())
			{
				var result = default(TSource);
				var foundCount = 0;
				while (enumerator.MoveNext())
				{
					if (!predicate(enumerator.Current) || foundCount > 1)
					{
						continue;
					}
					result = enumerator.Current;
					foundCount++;
				}
				if (foundCount == 1)
				{
					return result;
				}
			}
			throw new InvalidOperationException();
		}

		public static TSource SingleOrDefault<TSource>(this IMyEnumerable<TSource> source)
		{
			if (source == null) throw new ArgumentNullException();
			using (var enumerator = source.GetMyEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					throw new InvalidOperationException();
				}
				var current = enumerator.Current;
				if (!enumerator.MoveNext())
				{
					return current;
				}
			}
			return default(TSource);
		}

		public static TSource SingleOrDefault<TSource>(this IMyEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null) throw new ArgumentNullException();
			using (var enumerator = source.GetMyEnumerator())
			{
				var result = default(TSource);
				var foundCount = 0;
				while (enumerator.MoveNext())
				{
					if (!predicate(enumerator.Current) || foundCount > 0)
					{
						continue;
					}
					result = enumerator.Current;
					foundCount++;
				}
				return result;
			}
		}

		public static TSource ElementAt<TSource>(this IMyEnumerable<TSource> source, int index)
		{
			if (source == null) throw new ArgumentNullException();
			if (index < 0) throw new ArgumentOutOfRangeException();
			using (var enumerator = source.GetMyEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (index == 0)
					{
						return enumerator.Current;
					}
					index--;
				}
			}
			throw new ArgumentOutOfRangeException();
		}

		public static TSource ElementAtOrDefault<TSource>(this IMyEnumerable<TSource> source, int index)
		{
			if (source == null) throw new ArgumentNullException();
			if (index < 0) return default(TSource);
			using (var enumerator = source.GetMyEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (index == 0)
					{
						return enumerator.Current;
					}
					index--;
				}
			}
			return default(TSource);
		}

		public static IMyEnumerable<TSource> Take<TSource>(this IMyEnumerable<TSource> source, int count)
		{
			if (source == null) throw new ArgumentNullException();
			return new TakeEnumerable<TSource>(source, count);
		}

		private sealed class TakeEnumerable<TSource> : IMyEnumerable<TSource>
		{
			private readonly IMyEnumerable<TSource> _source;
			private readonly long _count;

			public TakeEnumerable(IMyEnumerable<TSource> source, long count)
			{
				_source = source;
				_count = count;
			}

			public IMyEnumerator<TSource> GetMyEnumerator()
			{
				return new TakeEnumerator(_source.GetMyEnumerator(), _count);
			}

			private sealed class TakeEnumerator : IMyEnumerator<TSource>
			{
				private readonly IMyEnumerator<TSource> _enumerator;
				private readonly long _count;
				private long _index;
				public TSource Current => _enumerator.Current;

				public TakeEnumerator(IMyEnumerator<TSource> enumerator, long count)
				{
					_enumerator = enumerator;
					_count = count;
					_index = -1;
				}

				public void Dispose()
				{
					_enumerator.Dispose();
				}

				public bool MoveNext()
				{
					_index++;
					return _enumerator.MoveNext() && _index < _count;
				}

				public void Reset()
				{
					_index = -1;
					_enumerator.Reset();
				}
			}
		}

		public static IMyEnumerable<TSource> Skip<TSource>(this IMyEnumerable<TSource> source, int count)
		{
			if (source == null) throw new ArgumentNullException();
			return new SkipEnumerable<TSource>(source, count);
		}

		private sealed class SkipEnumerable<TSource> : IMyEnumerable<TSource>
		{
			private readonly IMyEnumerable<TSource> _source;
			private readonly int _count;

			public SkipEnumerable(IMyEnumerable<TSource> source, int count)
			{
				_source = source;
				_count = count;
			}

			public IMyEnumerator<TSource> GetMyEnumerator()
			{
				return new SkipEnumerator(_source.GetMyEnumerator(), _count);
			}

			private sealed class SkipEnumerator : IMyEnumerator<TSource>
			{
				private readonly IMyEnumerator<TSource> _enumerator;
				private readonly int _count;
				private bool _isSkipped;
				public TSource Current => _enumerator.Current;

				public SkipEnumerator(IMyEnumerator<TSource> enumerator, int count)
				{
					_enumerator = enumerator;
					_count = count;
					_isSkipped = false;
				}

				public void Dispose()
				{
					_enumerator.Dispose();
				}

				public bool MoveNext()
				{
					if (_isSkipped) return _enumerator.MoveNext();
					var count = _count;
					while (count > 0 && _enumerator.MoveNext())
					{
						count--;
					}
					_isSkipped = true;
					return _enumerator.MoveNext();
				}

				public void Reset()
				{
					_isSkipped = false;
					_enumerator.Reset();
				}
			}
		}

		public static IMyEnumerable<TSource> DefaultIfEmpty<TSource>(this IMyEnumerable<TSource> source)
		{
			if (source == null) throw new ArgumentNullException();
			return new DefaultIfEmptyEnumerable<TSource>(source);
		}

		private sealed class DefaultIfEmptyEnumerable<TSource> : IMyEnumerable<TSource>
		{
			private readonly IMyEnumerable<TSource> _source;

			public DefaultIfEmptyEnumerable(IMyEnumerable<TSource> source)
			{
				_source = source;
			}

			public IMyEnumerator<TSource> GetMyEnumerator()
			{
				return new DefaultIfEmptyEnumerator(_source.GetMyEnumerator());
			}

			private sealed class DefaultIfEmptyEnumerator : IMyEnumerator<TSource>
			{
				private readonly IMyEnumerator<TSource> _enumerator;
				private bool _hasMore;
				public TSource Current => _hasMore ? _enumerator.Current : default(TSource);

				public DefaultIfEmptyEnumerator(IMyEnumerator<TSource> enumerator)
				{
					_enumerator = enumerator;
					_hasMore = true;
				}

				public void Dispose()
				{
					_enumerator.Dispose();
				}

				public bool MoveNext()
				{
					if (_hasMore)
					{
						_hasMore = _enumerator.MoveNext();
					}
					return true;
				}

				public void Reset()
				{
					_enumerator.Reset();
				}
			}
		}

		public static IMyEnumerable<TSource> Concat<TSource>(this IMyEnumerable<TSource> first,
			IMyEnumerable<TSource> second)
		{
			if (first == null || second == null) throw new ArgumentNullException();
			return new ConcatEnumerable<TSource>(first, second);
		}

		private sealed class ConcatEnumerable<TSource> : IMyEnumerable<TSource>
		{
			private readonly IMyEnumerable<TSource> _first;
			private readonly IMyEnumerable<TSource> _second;

			public ConcatEnumerable(IMyEnumerable<TSource> first, IMyEnumerable<TSource> second)
			{
				_first = first;
				_second = second;
			}

			public IMyEnumerator<TSource> GetMyEnumerator()
			{
				return new ConcatEnumerator(_first.GetMyEnumerator(), _second.GetMyEnumerator());
			}

			private sealed class ConcatEnumerator : IMyEnumerator<TSource>
			{
				private readonly IMyEnumerator<TSource> _firstEnumerator;
				private readonly IMyEnumerator<TSource> _secondEnumerator;
				private bool _firstEnumeratorHasMore;
				private bool _secondEnumeratorHasMore;

				public TSource Current =>
					_firstEnumeratorHasMore ? _firstEnumerator.Current : _secondEnumerator.Current;

				public ConcatEnumerator(IMyEnumerator<TSource> firstEnumerator, IMyEnumerator<TSource> secondEnumerator)
				{
					_firstEnumerator = firstEnumerator;
					_secondEnumerator = secondEnumerator;
					_firstEnumeratorHasMore = true;
					_secondEnumeratorHasMore = true;
				}

				public void Dispose()
				{
					using (_firstEnumerator)
					using (_secondEnumerator)
					{
					}
				}

				public bool MoveNext()
				{
					if (_firstEnumeratorHasMore)
					{
						_firstEnumeratorHasMore = _firstEnumerator.MoveNext();
					}
					if (!_firstEnumeratorHasMore && _secondEnumeratorHasMore)
					{
						_secondEnumeratorHasMore = _secondEnumerator.MoveNext();
					}
					return _firstEnumeratorHasMore || _secondEnumeratorHasMore;
				}

				public void Reset()
				{
					_firstEnumerator.Reset();
					_secondEnumerator.Reset();
				}
			}
		}

		public static TSource[] ToArray<TSource>(this IMyEnumerable<TSource> source)
		{
			if (source == null) throw new ArgumentNullException();
			var array = new TSource[4];
			var index = 0;
			using (var enumerator = source.GetMyEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (array.Length == index)
					{
						// 用意した容量いっぱいになったら適当に拡張する
						var tmp = new TSource[index*2];
						Array.Copy(array, tmp, index);
						array = tmp;
					}
					array[index++] = enumerator.Current;
				}
			}
			if (array.Length == index)
			{
				return array;
			}
			var result = new TSource[index];
			Array.Copy(array, result, index);
			return result;
		}

		public static bool SequenceEqual<TSource>(this IMyEnumerable<TSource> first, IMyEnumerable<TSource> second)
		{
			if (first == null || second == null) throw new ArgumentNullException();
			using (var firstEnumerator = first.GetMyEnumerator())
			using (var secondEnumerator = second.GetMyEnumerator())
			{
				while (firstEnumerator.MoveNext())
				{
					if (secondEnumerator.MoveNext() && Equals(firstEnumerator.Current, secondEnumerator.Current))
					{
						continue;
					}
					return false;
				}
				if (secondEnumerator.MoveNext())
				{
					return false;
				}
			}
			return true;
		}

		public static bool SequenceEqual<TSource>(this IMyEnumerable<TSource> first, IMyEnumerable<TSource> second,
			IMyEqualityComparer<TSource> comparer)
		{
			if (first == null || second == null) throw new ArgumentNullException();
			using (var firstEnumerator = first.GetMyEnumerator())
			using (var secondEnumerator = second.GetMyEnumerator())
			{
				while (firstEnumerator.MoveNext())
				{
					if (secondEnumerator.MoveNext() &&
					    comparer.Equals(firstEnumerator.Current, secondEnumerator.Current))
					{
						continue;
					}
					return false;
				}
				if (secondEnumerator.MoveNext())
				{
					return false;
				}
			}
			return true;
		}

		public static IMyEnumerable<TSource> Where<TSource>(this IMyEnumerable<TSource> source,
			Func<TSource, bool> predicate)
		{
			if (source == null || predicate == null) throw new ArgumentNullException();
			return Where(source, (value, index) => predicate(value));
		}

		public static IMyEnumerable<TSource> Where<TSource>(this IMyEnumerable<TSource> source,
			Func<TSource, int, bool> predicate)
		{
			if (source == null || predicate == null) throw new ArgumentNullException();
			return new WhereEnumerable<TSource>(source, predicate);
		}

		private sealed class WhereEnumerable<TSource> : IMyEnumerable<TSource>
		{
			private readonly IMyEnumerable<TSource> _source;
			private readonly Func<TSource, int, bool> _predicate;

			public WhereEnumerable(IMyEnumerable<TSource> source, Func<TSource, int, bool> predicate)
			{
				_source = source;
				_predicate = predicate;
			}

			public IMyEnumerator<TSource> GetMyEnumerator()
			{
				return new WhereEnumerator(_source.GetMyEnumerator(), _predicate);
			}

			private sealed class WhereEnumerator : IMyEnumerator<TSource>
			{
				private readonly IMyEnumerator<TSource> _enumerator;
				private readonly Func<TSource, int, bool> _predicate;
				private int _index;

				public TSource Current => _enumerator.Current;

				public WhereEnumerator(IMyEnumerator<TSource> enumerator, Func<TSource, int, bool> predicate)
				{
					_enumerator = enumerator;
					_predicate = predicate;
					_index = 0;
				}

				public void Dispose()
				{
					_enumerator.Dispose();
				}

				public bool MoveNext()
				{
					if (!_enumerator.MoveNext())
					{
						return false;
					}
					while (!_predicate(_enumerator.Current, _index))
					{
						if (!_enumerator.MoveNext())
						{
							return false;
						}
						_index++;
					}
					return true;
				}

				public void Reset()
				{
					_index = 0;
					_enumerator.Reset();
				}
			}
		}

		public static IMyEnumerable<TSource> TakeWhile<TSource>(this IMyEnumerable<TSource> source,
			Func<TSource, bool> predicate)
		{
			if (source == null || predicate == null) throw new ArgumentNullException();
			return TakeWhile(source, (value, index) => predicate(value));
		}

		public static IMyEnumerable<TSource> TakeWhile<TSource>(this IMyEnumerable<TSource> source,
			Func<TSource, int, bool> predicate)
		{
			if (source == null || predicate == null) throw new ArgumentNullException();
			return new TakeWhileEnumerable<TSource>(source, predicate);
		}

		private sealed class TakeWhileEnumerable<TSource> : IMyEnumerable<TSource>
		{
			private readonly IMyEnumerable<TSource> _source;
			private readonly Func<TSource, int, bool> _predicate;

			public TakeWhileEnumerable(IMyEnumerable<TSource> source, Func<TSource, int, bool> predicate)
			{
				_source = source;
				_predicate = predicate;
			}

			public IMyEnumerator<TSource> GetMyEnumerator()
			{
				return new TakeWhileEnumerator(_source.GetMyEnumerator(), _predicate);
			}

			private sealed class TakeWhileEnumerator : IMyEnumerator<TSource>
			{
				private readonly IMyEnumerator<TSource> _enumerator;
				private readonly Func<TSource, int, bool> _predicate;
				private int _index;
				private bool _isFound;

				public TSource Current => _enumerator.Current;

				public TakeWhileEnumerator(IMyEnumerator<TSource> enumerator, Func<TSource, int, bool> predicate)
				{
					_enumerator = enumerator;
					_predicate = predicate;
					_index = 0;
					_isFound = false;
				}

				public void Dispose()
				{
					_enumerator.Dispose();
				}

				public bool MoveNext()
				{
					if (_isFound || !_enumerator.MoveNext())
					{
						return false;
					}
					if (_predicate(_enumerator.Current, _index))
					{
						_isFound = true;
						return false;
					}
					_index++;
					return true;
				}

				public void Reset()
				{
					_index = 0;
					_isFound = false;
					_enumerator.Reset();
				}
			}
		}

		public static IMyEnumerable<TSource> SkipWhile<TSource>(this IMyEnumerable<TSource> source,
			Func<TSource, bool> predicate)
		{
			if (source == null || predicate == null) throw new ArgumentNullException();
			return SkipWhile(source, (value, index) => predicate(value));
		}

		public static IMyEnumerable<TSource> SkipWhile<TSource>(this IMyEnumerable<TSource> source,
			Func<TSource, int, bool> predicate)
		{
			if (source == null || predicate == null) throw new ArgumentNullException();
			return new SkipWhileEnumerable<TSource>(source, predicate);
		}

		private sealed class SkipWhileEnumerable<TSource> : IMyEnumerable<TSource>
		{
			private readonly IMyEnumerable<TSource> _source;
			private readonly Func<TSource, int, bool> _predicate;

			public SkipWhileEnumerable(IMyEnumerable<TSource> source, Func<TSource, int, bool> predicate)
			{
				_source = source;
				_predicate = predicate;
			}

			public IMyEnumerator<TSource> GetMyEnumerator()
			{
				return new SkipWhileEnumerator(_source.GetMyEnumerator(), _predicate);
			}

			private sealed class SkipWhileEnumerator : IMyEnumerator<TSource>
			{
				private readonly IMyEnumerator<TSource> _enumerator;
				private readonly Func<TSource, int, bool> _predicate;
				private int _index;
				private bool _isFound;

				public TSource Current => _enumerator.Current;

				public SkipWhileEnumerator(IMyEnumerator<TSource> enumerator, Func<TSource, int, bool> predicate)
				{
					_enumerator = enumerator;
					_predicate = predicate;
					_index = 0;
					_isFound = false;
				}

				public void Dispose()
				{
					_enumerator.Dispose();
				}

				public bool MoveNext()
				{
					if (!_enumerator.MoveNext())
					{
						return false;
					}
					if (_isFound)
					{
						return true;
					}
					while (!_predicate(_enumerator.Current, _index))
					{
						if (!_enumerator.MoveNext())
						{
							return false;
						}
						_index++;
					}
					return _isFound = true;
				}

				public void Reset()
				{
					_isFound = false;
					_index = 0;
					_enumerator.Reset();
				}
			}
		}

		// 非ジェネリックなIMyEnumerbleを検討
		public static IMyEnumerable<TResult> OfType<TResult>(this IMyEnumerable<object> source)
		{
			return new OfTypeEnumerable<TResult>(source);
		}

		private sealed class OfTypeEnumerable<TResult> : IMyEnumerable<TResult>
		{
			private readonly IMyEnumerable<object> _source;

			public OfTypeEnumerable(IMyEnumerable<object> source)
			{
				_source = source;
			}

			public IMyEnumerator<TResult> GetMyEnumerator()
			{
				return new OfTypeEnumerator(_source.GetMyEnumerator());
			}

			private sealed class OfTypeEnumerator : IMyEnumerator<TResult>
			{
				private readonly IMyEnumerator<object> _enumerator;

				public OfTypeEnumerator(IMyEnumerator<object> enumerator)
				{
					_enumerator = enumerator;
				}

				public TResult Current => (TResult) _enumerator.Current;

				public void Dispose()
				{
					_enumerator.Dispose();
				}

				public bool MoveNext()
				{
					if (_enumerator.MoveNext())
					{
						return _enumerator.Current is TResult;
					}
					return false;
				}

				public void Reset()
				{
					_enumerator.Reset();
				}
			}
		}

		public static IMyEnumerable<TResult> Select<TSource, TResult>(this IMyEnumerable<TSource> source,
			Func<TSource, TResult> selector)
		{
			return new SelectEnumerable<TSource, TResult>(source, (value, index) => selector(value));
		}

		public static IMyEnumerable<TResult> Select<TSource, TResult>(this IMyEnumerable<TSource> source,
			Func<TSource, int, TResult> selector)
		{
			return new SelectEnumerable<TSource, TResult>(source, selector);
		}

		private sealed class SelectEnumerable<TSource, TResult> : IMyEnumerable<TResult>
		{
			private readonly IMyEnumerable<TSource> _source;
			private readonly Func<TSource, int, TResult> _selector;

			public SelectEnumerable(IMyEnumerable<TSource> source, Func<TSource, int, TResult> selector)
			{
				_source = source;
				_selector = selector;
			}

			public IMyEnumerator<TResult> GetMyEnumerator()
			{
				return new SelectEnumerator(_source.GetMyEnumerator(), _selector);
			}

			private sealed class SelectEnumerator : IMyEnumerator<TResult>
			{
				private readonly IMyEnumerator<TSource> _enumerator;
				private readonly Func<TSource, int, TResult> _selector;
				private int _index;

				public TResult Current => _selector(_enumerator.Current, _index);

				public SelectEnumerator(IMyEnumerator<TSource> enumerator, Func<TSource, int, TResult> selector)
				{
					_enumerator = enumerator;
					_selector = selector;
					_index = 0;
				}

				public void Dispose()
				{
					_enumerator.Dispose();
				}

				public bool MoveNext()
				{
					_index++;
					return _enumerator.MoveNext();
				}

				public void Reset()
				{
					_index = 0;
					_enumerator.Reset();
				}
			}
		}

		public static IMyEnumerable<TResult> SelectMany<TSource, TResult>(this IMyEnumerable<TSource> source,
			Func<TSource, IMyEnumerable<TResult>> selector)
		{
			return new SelectManyEnumerable<TSource, TResult, TResult>(source, (value, index) => selector(value),
				(value, collection) => collection);
		}

		public static IMyEnumerable<TResult> SelectMany<TSource, TResult>(this IMyEnumerable<TSource> source,
			Func<TSource, int, IMyEnumerable<TResult>> selector)
		{
			return new SelectManyEnumerable<TSource, TResult, TResult>(source, selector,
				(value, collection) => collection);
		}

		public static IMyEnumerable<TResult> SelectMany<TSource, TCollection, TResult>(
			this IMyEnumerable<TSource> source,
			Func<TSource, IMyEnumerable<TCollection>> collectionSelector,
			Func<TSource, TCollection, TResult> resultSelector)
		{
			return new SelectManyEnumerable<TSource, TCollection, TResult>(source,
				(value, index) => collectionSelector(value),
				resultSelector);
		}

		public static IMyEnumerable<TResult> SelectMany<TSource, TCollection, TResult>(
			this IMyEnumerable<TSource> source,
			Func<TSource, int, IMyEnumerable<TCollection>> collectionSelector,
			Func<TSource, TCollection, TResult> resultSelector)
		{
			return new SelectManyEnumerable<TSource, TCollection, TResult>(source, collectionSelector, resultSelector);
		}

		private sealed class SelectManyEnumerable<TSource, TCollection, TResult> : IMyEnumerable<TResult>
		{
			private readonly IMyEnumerable<TSource> _source;
			private readonly Func<TSource, int, IMyEnumerable<TCollection>> _collectionSelector;
			private readonly Func<TSource, TCollection, TResult> _resultSelector;

			public SelectManyEnumerable(IMyEnumerable<TSource> source,
				Func<TSource, int, IMyEnumerable<TCollection>> collectionSelector,
				Func<TSource, TCollection, TResult> resultSelector)
			{
				_source = source;
				_collectionSelector = collectionSelector;
				_resultSelector = resultSelector;
			}

			public IMyEnumerator<TResult> GetMyEnumerator()
			{
				return new SelectManyEnumerator(_source.GetMyEnumerator(), _collectionSelector, _resultSelector);
			}

			private sealed class SelectManyEnumerator : IMyEnumerator<TResult>
			{
				private readonly IMyEnumerator<TSource> _enumerator;
				private readonly Func<TSource, int, IMyEnumerable<TCollection>> _collectionSelector;
				private readonly Func<TSource, TCollection, TResult> _resultSelector;
				private IMyEnumerator<TCollection> _innerEnumerator;
				private int _index;

				public TResult Current => _resultSelector(_enumerator.Current, _innerEnumerator.Current);

				public SelectManyEnumerator(IMyEnumerator<TSource> enumerator,
					Func<TSource, int, IMyEnumerable<TCollection>> collectionSelector,
					Func<TSource, TCollection, TResult> resultSelector)
				{
					_enumerator = enumerator;
					_collectionSelector = collectionSelector;
					_resultSelector = resultSelector;
					_index = 0;
				}

				public void Dispose()
				{
					using (_innerEnumerator)
					using (_enumerator)
					{
					}
				}

				public bool MoveNext()
				{
					if (_innerEnumerator != null)
					{
						if (_innerEnumerator.MoveNext())
						{
							return true;
						}
						_innerEnumerator.Dispose();
						_innerEnumerator = null;
					}
					while (_enumerator.MoveNext())
					{
						if (_innerEnumerator == null)
						{
							_innerEnumerator = _collectionSelector(_enumerator.Current, _index).GetMyEnumerator();
							_index++;
						}
						if (_innerEnumerator.MoveNext())
						{
							return true;
						}
					}
					return false;
				}

				public void Reset()
				{
					_index = 0;
					_innerEnumerator.Reset();
					_innerEnumerator = null;
					_enumerator.Reset();
				}
			}
		}

		public static IMyEnumerable<TResult> Zip<TFirst, TSecond, TResult>(this IMyEnumerable<TFirst> first,
			IMyEnumerable<TSecond> second, Func<TFirst, TSecond, TResult> resultSelector)
		{
			if (first == null || second == null || resultSelector == null) throw new ArgumentNullException();
			return new ZipEnumerable<TFirst, TSecond, TResult>(first, second, resultSelector);
		}

		private sealed class ZipEnumerable<TFirst, TSecond, TResult> : IMyEnumerable<TResult>
		{
			private readonly IMyEnumerable<TFirst> _first;
			private readonly IMyEnumerable<TSecond> _second;
			private readonly Func<TFirst, TSecond, TResult> _resultSelector;

			public ZipEnumerable(IMyEnumerable<TFirst> first, IMyEnumerable<TSecond> second,
				Func<TFirst, TSecond, TResult> resultSelector)
			{
				_first = first;
				_second = second;
				_resultSelector = resultSelector;
			}

			public IMyEnumerator<TResult> GetMyEnumerator()
			{
				return new ZipEnumerator(_first.GetMyEnumerator(), _second.GetMyEnumerator(), _resultSelector);
			}

			private sealed class ZipEnumerator : IMyEnumerator<TResult>
			{
				private readonly IMyEnumerator<TFirst> _firstEnumerator;
				private readonly IMyEnumerator<TSecond> _secondEnumerator;
				private readonly Func<TFirst, TSecond, TResult> _resultSelector;

				public ZipEnumerator(IMyEnumerator<TFirst> firstEnumerator, IMyEnumerator<TSecond> secondEnumerator,
					Func<TFirst, TSecond, TResult> resultSelector)
				{
					_firstEnumerator = firstEnumerator;
					_secondEnumerator = secondEnumerator;
					_resultSelector = resultSelector;
				}

				public TResult Current => _resultSelector(_firstEnumerator.Current, _secondEnumerator.Current);

				public void Dispose()
				{
					using (_firstEnumerator)
					using (_secondEnumerator)
					{
					}
				}

				public bool MoveNext()
				{
					return _firstEnumerator.MoveNext() && _secondEnumerator.MoveNext();
				}

				public void Reset()
				{
					_firstEnumerator.Reset();
					_secondEnumerator.Reset();
				}
			}
		}

		public static TSource Aggregate<TSource>(this IMyEnumerable<TSource> source,
			Func<TSource, TSource, TSource> func)
		{
			if (source == null || func == null) throw new ArgumentNullException();
			using (var e = source.GetMyEnumerator())
			{
				if (!e.MoveNext())
				{
					throw new ArgumentException();
				}
				var accumulator = e.Current;
				while (e.MoveNext())
				{
					accumulator = func(accumulator, e.Current);
				}
				return accumulator;
			}
		}

		public static TAccumulate Aggregate<TSource, TAccumulate>(this IMyEnumerable<TSource> source, TAccumulate seed,
			Func<TAccumulate, TSource, TAccumulate> func)
		{
			if (source == null || seed == null || func == null) throw new ArgumentNullException();
			using (var e = source.GetMyEnumerator())
			{
				var accumulator = seed;
				while (e.MoveNext())
				{
					accumulator = func(accumulator, e.Current);
				}
				return accumulator;
			}
		}

		public static TResult Aggregate<TSource, TAccumulate, TResult>(this IMyEnumerable<TSource> source,
			TAccumulate seed,
			Func<TAccumulate, TSource, TAccumulate> func, Func<TAccumulate, TResult> resultSelector)
		{
			if (source == null || seed == null || func == null || resultSelector == null)
				throw new ArgumentNullException();
			using (var e = source.GetMyEnumerator())
			{
				var accumulator = seed;
				while (e.MoveNext())
				{
					accumulator = func(accumulator, e.Current);
				}
				return resultSelector(accumulator);
			}
		}

		private class DefaultMyEqualityCamparer<TSource> : IMyEqualityComparer<TSource>
		{
			public bool Equals(TSource x, TSource y)
			{
				return object.Equals(x, y);
			}

			public int GetHashCode(TSource obj)
			{
				return obj.GetHashCode();
			}
		}

		public static IMyEnumerable<TSource> Distinct<TSource>(this IMyEnumerable<TSource> source)
		{
			return new DistinctEnumerable<TSource>(source, new DefaultMyEqualityCamparer<TSource>());
		}

		public static IMyEnumerable<TSource> Distinct<TSource>(this IMyEnumerable<TSource> source,
			IMyEqualityComparer<TSource> comparer)
		{
			return new DistinctEnumerable<TSource>(source, comparer);
		}

		private sealed class DistinctEnumerable<TSource> : IMyEnumerable<TSource>
		{
			private readonly IMyEnumerable<TSource> _source;
			private readonly IMyEqualityComparer<TSource> _comparer;

			public DistinctEnumerable(IMyEnumerable<TSource> source, IMyEqualityComparer<TSource> comparer)
			{
				_source = source;
				_comparer = comparer;
			}

			public IMyEnumerator<TSource> GetMyEnumerator()
			{
				return new DistinctEnumerator(_source.GetMyEnumerator(), _comparer);
			}

			private sealed class DistinctEnumerator : IMyEnumerator<TSource>
			{
				private readonly IMyEnumerator<TSource> _enumerator;
				private readonly IMyEqualityComparer<TSource> _comparer;
				private Set<TSource> _set;

				public TSource Current => _enumerator.Current;

				public DistinctEnumerator(IMyEnumerator<TSource> enumerator, IMyEqualityComparer<TSource> comparer)
				{
					_enumerator = enumerator;
					_comparer = comparer;
					_set = new Set<TSource>(comparer);
				}

				public void Dispose()
				{
					_enumerator.Dispose();
				}

				public bool MoveNext()
				{
					while (_enumerator.MoveNext())
					{
						if (_set.Add(_enumerator.Current))
						{
							return true;
						}
					}
					return false;
				}

				public void Reset()
				{
					_set = new Set<TSource>(_comparer);
					_enumerator.Reset();
				}
			}
		}

		private sealed class Set<T>
		{
			private int _index;
			private T[] _values;
			private readonly IMyEqualityComparer<T> _comparer;

			public Set(IMyEqualityComparer<T> comparer)
			{
				_comparer = comparer;
				_index = 0;
				_values = new T[4];
			}

			// TODO: 重複判定の高速化
			public bool Add(T value)
			{
				for (var i = 0; i < _index; i++)
				{
					if (_comparer.Equals(value, _values[i]))
					{
						return false;
					}
				}

				if (_values.Length == _index)
				{
					var tmp = new T[_index*2];
					Array.Copy(_values, tmp, _index);
					_values = tmp;
				}
				_values[_index++] = value;
				return true;
			}
		}

		// TODO: 名前再考
		private abstract class IndexComparer<TSource> : IMyComparer<int>
		{
			internal IndexComparer<TSource> Parent { get; set; }
			public abstract void MappingKeys(TSource[] source);
			public abstract int Compare(int x, int y);
		}

		private sealed class IndexComparer<TSource, TKey> : IndexComparer<TSource>
		{
			private readonly Func<TSource, TKey> _keySelector;
			private readonly IMyComparer<TKey> _comparer;
			private readonly bool _descending;
			private TKey[] _keys;

			public IndexComparer(Func<TSource, TKey> keySelector, IMyComparer<TKey> comparer, bool descending = false)
			{
				_keySelector = keySelector;
				_comparer = comparer;
				_descending = descending;
			}

			public override void MappingKeys(TSource[] source)
			{
				_keys = new TKey[source.Length];
				for (var i = 0; i < source.Length; i++)
				{
					_keys[i] = _keySelector(source[i]);
				}
				Parent?.MappingKeys(source);
			}

			public override int Compare(int index1, int index2)
			{
				if (Parent != null)
				{
					var parentResult = Parent.Compare(index1, index2);
					if (parentResult != 0)
					{
						return parentResult;
					}
				}
				var result = _comparer.Compare(_keys[index1], _keys[index2]);
				return _descending ? -result : result;
			}
		}

		public static IMyOrderedEnumerable<TSource> OrderBy<TSource, TKey>(this IMyEnumerable<TSource> source,
			Func<TSource, TKey> keySelector) where TKey : IComparable
		{
			return new OrderedEnumerable<TSource, TKey>(source, keySelector, new DefaultComparer<TKey>());
		}

		public static IMyOrderedEnumerable<TSource> OrderBy<TSource, TKey>(this IMyEnumerable<TSource> source,
			Func<TSource, TKey> keySelector, IMyComparer<TKey> comparer)
		{
			return new OrderedEnumerable<TSource, TKey>(source, keySelector, comparer);
		}

		public static IMyOrderedEnumerable<TSource> OrderByDescending<TSource, TKey>(this IMyEnumerable<TSource> source,
			Func<TSource, TKey> keySelector) where TKey : IComparable
		{
			return new OrderedEnumerable<TSource, TKey>(source, keySelector, new DefaultComparer<TKey>(), true);
		}

		public static IMyOrderedEnumerable<TSource> OrderByDescending<TSource, TKey>(this IMyEnumerable<TSource> source,
			Func<TSource, TKey> keySelector, IMyComparer<TKey> comparer)
		{
			return new OrderedEnumerable<TSource, TKey>(source, keySelector, comparer, true);
		}

		public static IMyOrderedEnumerable<TSource> ThenBy<TSource, TKey>(this IMyOrderedEnumerable<TSource> source,
			Func<TSource, TKey> keySelector) where TKey : IComparable
		{
			return source.CreateOrderedEnumerable(keySelector, new DefaultComparer<TKey>(), false);
		}

		public static IMyOrderedEnumerable<TSource> ThenBy<TSource, TKey>(this IMyOrderedEnumerable<TSource> source,
			Func<TSource, TKey> keySelector, IMyComparer<TKey> comparer)
		{
			return source.CreateOrderedEnumerable(keySelector, comparer, false);
		}

		public static IMyOrderedEnumerable<TSource> ThenByDescending<TSource, TKey>(
			this IMyOrderedEnumerable<TSource> source,
			Func<TSource, TKey> keySelector) where TKey : IComparable
		{
			return source.CreateOrderedEnumerable(keySelector, new DefaultComparer<TKey>(), true);
		}

		public static IMyOrderedEnumerable<TSource> ThenByDescending<TSource, TKey>(
			this IMyOrderedEnumerable<TSource> source,
			Func<TSource, TKey> keySelector, IMyComparer<TKey> comparer)
		{
			return source.CreateOrderedEnumerable(keySelector, comparer, true);
		}

		private sealed class OrderedEnumerable<TSource, TKey> : IMyOrderedEnumerable<TSource>
		{
			private readonly IMyEnumerable<TSource> _source;
			private IndexComparer<TSource> _indexComparer;

			public OrderedEnumerable(IMyEnumerable<TSource> source, Func<TSource, TKey> keySelector,
				IMyComparer<TKey> comparer,
				bool descending = false)
			{
				_source = source;
				_indexComparer = new IndexComparer<TSource, TKey>(keySelector, comparer, descending);
			}

			public IMyEnumerator<TSource> GetMyEnumerator()
			{
				return new OrderedEnumerator(_source, _indexComparer);
			}

			public IMyOrderedEnumerable<TSource> CreateOrderedEnumerable<TNextKey>(Func<TSource, TNextKey> keySelector,
				IMyComparer<TNextKey> comparer, bool descending = false)
			{
				var indexComparer = new IndexComparer<TSource, TNextKey>(keySelector, comparer, descending)
				{
					Parent = _indexComparer
				};
				return new OrderedEnumerable<TSource, TNextKey>(_source, keySelector, comparer, descending)
				{
					_indexComparer = indexComparer
				};
			}

			private sealed class OrderedEnumerator : IMyEnumerator<TSource>
			{
				private readonly IMyEnumerable<TSource> _source;
				private TSource[] _sourceArray;
				private readonly IndexComparer<TSource> _indexComparer;
				private int _index;
				private int[] _indexMap;

				public TSource Current => _sourceArray[_indexMap[_index]];

				public OrderedEnumerator(IMyEnumerable<TSource> source, IndexComparer<TSource> indexComparer)
				{
					_source = source;
					_indexComparer = indexComparer;
					_index = -1;
				}

				public void Dispose()
				{
					// do nothing
				}

				public bool MoveNext()
				{
					if (_sourceArray == null)
					{
						_sourceArray = _source.ToArray();
						_indexComparer.MappingKeys(_sourceArray);
					}

					_index++;
					if (_index == _sourceArray.Length)
					{
						return false;
					}

					if (_indexMap == null)
					{
						_indexMap = MakeSortedIndexMap();
					}
					return true;
				}

				public void Reset()
				{
					_sourceArray = null;
					_indexMap = null;
					_index = -1;
				}

				private int[] MakeSortedIndexMap()
				{
					var indexMap = new int[_sourceArray.Length];
					for (var i = 0; i < _sourceArray.Length; i++)
					{
						indexMap[i] = i;
					}
					MergeSort(0, _sourceArray.Length, indexMap, new int[indexMap.Length/2]);
					return indexMap;
				}

				private void MergeSort(int left, int right, int[] indexMap, int[] work)
				{
					if (left + 1 >= right) return;
					var mid = (left + right)/2;
					MergeSort(left, mid, indexMap, work);
					MergeSort(mid, right, indexMap, work);
					Merge(left, mid, right, indexMap, work);
				}

				private void Merge(int left, int mid, int right, int[] indexMap, int[] work)
				{
					int i, j, k;
					for (i = left, j = 0; i != mid; i++, j++)
					{
						work[j] = indexMap[i];
					}

					mid -= left;
					for (j = 0, k = left; i != right && j != mid; k++)
					{
						if (_indexComparer.Compare(indexMap[i], work[j]) <= 0)
						{
							indexMap[k] = indexMap[i];
							i++;
						}
						else
						{
							indexMap[k] = work[j];
							j++;
						}
					}

					while (i < right)
					{
						indexMap[k] = indexMap[i];
						i++;
						k++;
					}
					while (j < mid)
					{
						indexMap[k] = work[j];
						j++;
						k++;
					}
				}
			}
		}

		public static IMyEnumerable<TResult> Join<TOuter, TInner, TKey, TResult>(this IMyEnumerable<TOuter> outer,
			IMyEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector,
			Func<TOuter, TInner, TResult> resultSelector)
		{
			if (inner == null || outerKeySelector == null || innerKeySelector == null || resultSelector == null)
				throw new ArgumentNullException();

			return new JoinEnumerable<TOuter, TInner, TKey, TResult>(outer, inner, outerKeySelector, innerKeySelector,
				resultSelector, new DefaultEqualityComparer<TKey>());
		}

		public static IMyEnumerable<TResult> Join<TOuter, TInner, TKey, TResult>(this IMyEnumerable<TOuter> outer,
			IMyEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector,
			Func<TOuter, TInner, TResult> resultSelector, IMyEqualityComparer<TKey> comparer)
		{
			if (inner == null || outerKeySelector == null || innerKeySelector == null || resultSelector == null ||
			    comparer == null) throw new ArgumentNullException();

			return new JoinEnumerable<TOuter, TInner, TKey, TResult>(outer, inner, outerKeySelector, innerKeySelector,
				resultSelector, comparer);
		}

		private sealed class JoinEnumerable<TOuter, TInner, TKey, TResult> : IMyEnumerable<TResult>
		{
			private readonly IMyEnumerable<TOuter> _outer;
			private readonly IMyEnumerable<TInner> _inner;
			private readonly Func<TOuter, TKey> _outerKeySelector;
			private readonly Func<TInner, TKey> _innerKeySelector;
			private readonly Func<TOuter, TInner, TResult> _resultSelector;
			private readonly IMyEqualityComparer<TKey> _comparer;

			public JoinEnumerable(IMyEnumerable<TOuter> outer, IMyEnumerable<TInner> inner,
				Func<TOuter, TKey> outerKeySelector,
				Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector,
				IMyEqualityComparer<TKey> comparer)
			{
				_outer = outer;
				_inner = inner;
				_outerKeySelector = outerKeySelector;
				_innerKeySelector = innerKeySelector;
				_resultSelector = resultSelector;
				_comparer = comparer;
			}

			public IMyEnumerator<TResult> GetMyEnumerator()
			{
				return new JoinEnumerator(_outer.GetMyEnumerator(), _inner.GetMyEnumerator(),
					_outerKeySelector, _innerKeySelector, _resultSelector, _comparer);
			}

			private sealed class JoinEnumerator : IMyEnumerator<TResult>
			{
				private readonly IMyEnumerator<TOuter> _outerEnumerator;
				private readonly IMyEnumerator<TInner> _innerEnumerator;
				private readonly Func<TOuter, TKey> _outerKeySelector;
				private readonly Func<TInner, TKey> _innerKeySelector;
				private readonly Func<TOuter, TInner, TResult> _resultSelector;
				private readonly IMyEqualityComparer<TKey> _comparer;
				private readonly MyHashMap<TOuter, TKey> _outerKeyMap;
				private readonly MyHashMap<TInner, TKey> _innerKeyMap;
				private bool _isJoining;


				public TResult Current => _resultSelector(_outerEnumerator.Current, _innerEnumerator.Current);

				public JoinEnumerator(IMyEnumerator<TOuter> outerEnumerator, IMyEnumerator<TInner> innerEnumerator,
					Func<TOuter, TKey> outerKeySelector,
					Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector,
					IMyEqualityComparer<TKey> comparer)
				{
					_outerEnumerator = outerEnumerator;
					_innerEnumerator = innerEnumerator;
					_outerKeySelector = outerKeySelector;
					_innerKeySelector = innerKeySelector;
					_resultSelector = resultSelector;
					_comparer = comparer;
					_outerKeyMap = new MyHashMap<TOuter, TKey>();
					_innerKeyMap = new MyHashMap<TInner, TKey>();
					_isJoining = false;
				}

				public void Dispose()
				{
					using (_outerEnumerator)
					using (_innerEnumerator)
					{
					}
				}

				public bool MoveNext()
				{
					if (_isJoining)
					{
						TKey outerKey;
						if (!_outerKeyMap.TryGetValue(_outerEnumerator.Current, out outerKey))
						{
							outerKey = _outerKeyMap.Add(_outerEnumerator.Current,
								_outerKeySelector(_outerEnumerator.Current));
						}
						while (_innerEnumerator.MoveNext())
						{
							TKey innerKey;
							if (!_innerKeyMap.TryGetValue(_innerEnumerator.Current, out innerKey))
							{
								innerKey = _innerKeyMap.Add(_innerEnumerator.Current,
									_innerKeySelector(_innerEnumerator.Current));
							}
							if (_comparer.Equals(outerKey, innerKey))
							{
								return true;
							}
						}
						_isJoining = false;
						_innerEnumerator.Reset();
					}

					while (_outerEnumerator.MoveNext())
					{
						TKey outerKey;
						if (!_outerKeyMap.TryGetValue(_outerEnumerator.Current, out outerKey))
						{
							outerKey = _outerKeyMap.Add(_outerEnumerator.Current,
								_outerKeySelector(_outerEnumerator.Current));
						}
						while (_innerEnumerator.MoveNext())
						{
							TKey innerKey;
							if (!_innerKeyMap.TryGetValue(_innerEnumerator.Current, out innerKey))
							{
								innerKey = _innerKeyMap.Add(_innerEnumerator.Current,
									_innerKeySelector(_innerEnumerator.Current));
							}
							if (!_comparer.Equals(outerKey, innerKey))
							{
								continue;
							}
							_isJoining = true;
							return true;
						}
						_innerEnumerator.Reset();
					}
					return false;
				}

				public void Reset()
				{
					_outerEnumerator.Reset();
					_innerEnumerator.Reset();
					_outerKeyMap.Clear();
					_innerKeyMap.Clear();
					_isJoining = false;
				}
			}
		}

		private sealed class MyGrouping<TKey, TElement> : IMyGrouping<TKey, TElement>
		{
			private readonly IMyEnumerable<TElement> _source;

			public TKey Key { get; }

			public MyGrouping(TKey key, IMyEnumerable<TElement> source)
			{
				Key = key;
				_source = source;
			}

			public IMyEnumerator<TElement> GetMyEnumerator()
			{
				return _source.GetMyEnumerator();
			}
		}

		public static IMyEnumerable<IMyGrouping<TKey, TSource>> GroupBy<TSource, TKey>(
			this IMyEnumerable<TSource> source, Func<TSource, TKey> keySelector)
		{
			return new GroupByEnumerable<TSource, TKey, TSource, IMyGrouping<TKey, TSource>>(source, keySelector,
				element => element, (key, elements) => new MyGrouping<TKey, TSource>(key, elements),
				new DefaultEqualityComparer<TKey>());
		}

		public static IMyEnumerable<IMyGrouping<TKey, TSource>> GroupBy<TSource, TKey>(
			this IMyEnumerable<TSource> source,
			Func<TSource, TKey> keySelector,
			IMyEqualityComparer<TKey> comparer)
		{
			return new GroupByEnumerable<TSource, TKey, TSource, IMyGrouping<TKey, TSource>>(source, keySelector,
				element => element, (key, elements) => new MyGrouping<TKey, TSource>(key, elements), comparer);
		}

		public static IMyEnumerable<IMyGrouping<TKey, TElement>> GroupBy<TSource, TKey, TElement>(
			this IMyEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
		{
			return new GroupByEnumerable<TSource, TKey, TElement, IMyGrouping<TKey, TElement>>(source, keySelector,
				elementSelector, (key, elements) => new MyGrouping<TKey, TElement>(key, elements),
				new DefaultEqualityComparer<TKey>());
		}

		public static IMyEnumerable<IMyGrouping<TKey, TElement>> GroupBy<TSource, TKey, TElement>(
			this IMyEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector,
			IMyEqualityComparer<TKey> comparer)
		{
			return new GroupByEnumerable<TSource, TKey, TElement, IMyGrouping<TKey, TElement>>(source, keySelector,
				elementSelector, (key, elements) => new MyGrouping<TKey, TElement>(key, elements), comparer);
		}

		public static IMyEnumerable<TResult> GroupBy<TSource, TKey, TResult>(
			this IMyEnumerable<TSource> source,
			Func<TSource, TKey> keySelector,
			Func<TKey, IMyEnumerable<TSource>, TResult> resultSelector)
		{
			return new GroupByEnumerable<TSource, TKey, TSource, TResult>(source, keySelector, element => element,
				resultSelector, new DefaultEqualityComparer<TKey>());
		}

		public static IMyEnumerable<TResult> GroupBy<TSource, TKey, TElement, TResult>(
			this IMyEnumerable<TSource> source,
			Func<TSource, TKey> keySelector,
			Func<TSource, TElement> elementSelector,
			Func<TKey, IMyEnumerable<TElement>, TResult> resultSelector)
		{
			return new GroupByEnumerable<TSource, TKey, TElement, TResult>(source, keySelector, elementSelector,
				resultSelector, new DefaultEqualityComparer<TKey>());
		}

		public static IMyEnumerable<TResult> GroupBy<TSource, TKey, TElement, TResult>(
			this IMyEnumerable<TSource> source,
			Func<TSource, TKey> keySelector,
			Func<TSource, TElement> elementSelector,
			Func<TKey, IMyEnumerable<TElement>, TResult> resultSelector,
			IMyEqualityComparer<TKey> comparer)
		{
			return new GroupByEnumerable<TSource, TKey, TElement, TResult>(source, keySelector, elementSelector,
				resultSelector, comparer);
		}

		private sealed class GroupByEnumerable<TSource, TKey, TElement, TResult> : IMyEnumerable<TResult>
		{
			private readonly IMyEnumerable<TSource> _source;
			private readonly Func<TSource, TKey> _keySelector;
			private readonly Func<TSource, TElement> _elementSelector;
			private readonly Func<TKey, IMyEnumerable<TElement>, TResult> _resultSelector;
			private readonly IMyEqualityComparer<TKey> _comparer;

			public GroupByEnumerable(IMyEnumerable<TSource> source, Func<TSource, TKey> keySelector,
				Func<TSource, TElement> elementSelector, Func<TKey, IMyEnumerable<TElement>, TResult> resultSelector,
				IMyEqualityComparer<TKey> comparer)
			{
				_source = source;
				_keySelector = keySelector;
				_elementSelector = elementSelector;
				_resultSelector = resultSelector;
				_comparer = comparer;
			}

			public IMyEnumerator<TResult> GetMyEnumerator()
			{
				return new GroupByEnumerator<TSource, TKey, TElement, TResult>(_source.GetMyEnumerator(), _keySelector,
					_elementSelector, _resultSelector, _comparer);
			}
		}

		private sealed class GroupByEnumerator<TSource, TKey, TElement, TResult> : IMyEnumerator<TResult>
		{
			private readonly IMyEnumerator<TSource> _source;
			private readonly Func<TSource, TKey> _keySelector;
			private readonly Func<TSource, TElement> _elementSelector;
			private readonly Func<TKey, IMyEnumerable<TElement>, TResult> _resultSelector;
			private readonly IMyEqualityComparer<TKey> _comparer;
			private MyHashMap<TKey, MyArrayList<TElement>> _map;
			private IMyEnumerator<KeyValuPair<TKey, MyArrayList<TElement>>> _mapEnumerator;

			public TResult Current =>
				_resultSelector(_mapEnumerator.Current.Key, _mapEnumerator.Current.Value);

			public GroupByEnumerator(IMyEnumerator<TSource> source, Func<TSource, TKey> keySelector,
				Func<TSource, TElement> elementSelector, Func<TKey, IMyEnumerable<TElement>, TResult> resultSelector,
				IMyEqualityComparer<TKey> comparer)
			{
				_source = source;
				_keySelector = keySelector;
				_elementSelector = elementSelector;
				_resultSelector = resultSelector;
				_comparer = comparer;
				_map = new MyHashMap<TKey, MyArrayList<TElement>>(_comparer);
			}

			public void Dispose()
			{
				_source.Dispose();
			}

			public bool MoveNext()
			{
				if (_mapEnumerator == null)
				{
					while (_source.MoveNext())
					{
						var currentKey = _keySelector(_source.Current);
						MyArrayList<TElement> group;
						if (!_map.TryGetValue(currentKey, out group))
						{
							group = new MyArrayList<TElement>();
							_map.Add(currentKey, group);
						}
						group.Add(_elementSelector(_source.Current));
					}
					_mapEnumerator = _map.GetMyEnumerator();
				}
				return _mapEnumerator.MoveNext();
			}

			public void Reset()
			{
				_source.Reset();
				_map = new MyHashMap<TKey, MyArrayList<TElement>>(_comparer);
				_mapEnumerator = null;
			}
		}

		public static IMyEnumerable<TResult> GroupJoin<TOuter, TInner, TKey, TResult>(
			this IMyEnumerable<TOuter> outer, IMyEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector,
			Func<TInner, TKey> innerKeySelector, Func<TOuter, IMyEnumerable<TInner>, TResult> resultSelector)
		{
			if (outer == null || inner == null || outerKeySelector == null || innerKeySelector == null ||
			    resultSelector == null) throw new ArgumentException();
			return new GroupJoinEnumerable<TOuter, TInner, TKey, TResult>(outer, inner, outerKeySelector,
				innerKeySelector, resultSelector, new DefaultEqualityComparer<TKey>());
		}

		public static IMyEnumerable<TResult> GroupJoin<TOuter, TInner, TKey, TResult>(
			this IMyEnumerable<TOuter> outer,
			IMyEnumerable<TInner> inner,
			Func<TOuter, TKey> outerKeySelector,
			Func<TInner, TKey> innerKeySelector,
			Func<TOuter, IMyEnumerable<TInner>, TResult> resultSelector,
			IMyEqualityComparer<TKey> comparer)
		{
			if (outer == null || inner == null || outerKeySelector == null || innerKeySelector == null ||
			    resultSelector == null || comparer == null) throw new ArgumentException();
			return new GroupJoinEnumerable<TOuter, TInner, TKey, TResult>(outer, inner, outerKeySelector,
				innerKeySelector, resultSelector, comparer);
		}

		private sealed class GroupJoinEnumerable<TOuter, TInner, TKey, TResult> : IMyEnumerable<TResult>
		{
			private readonly IMyEnumerable<TOuter> _outer;
			private readonly IMyEnumerable<TInner> _inner;
			private readonly Func<TOuter, TKey> _outerKeySelector;
			private readonly Func<TInner, TKey> _innerKeySelector;
			private readonly Func<TOuter, IMyEnumerable<TInner>, TResult> _resultSelector;
			private readonly IMyEqualityComparer<TKey> _comparer;

			public GroupJoinEnumerable(IMyEnumerable<TOuter> outer, IMyEnumerable<TInner> inner,
				Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector,
				Func<TOuter, IMyEnumerable<TInner>, TResult> resultSelector, IMyEqualityComparer<TKey> comparer)
			{
				_outer = outer;
				_inner = inner;
				_outerKeySelector = outerKeySelector;
				_innerKeySelector = innerKeySelector;
				_resultSelector = resultSelector;
				_comparer = comparer;
			}

			public IMyEnumerator<TResult> GetMyEnumerator()
			{
				return new GroupJoinEnumerator(this);
			}

			private sealed class GroupJoinEnumerator : IMyEnumerator<TResult>
			{
				private readonly GroupJoinEnumerable<TOuter, TInner, TKey, TResult> _enumerable;
				private readonly IMyEnumerator<TOuter> _outerEnumerator;
				private readonly IMyEnumerator<TInner> _innerEnumerator;
				private IMyEnumerator<Tuple<TOuter, MyArrayList<TInner>>> _resultEnumerator;

				public TResult Current
					=> _enumerable._resultSelector(_resultEnumerator.Current.Item1, _resultEnumerator.Current.Item2);

				public GroupJoinEnumerator(GroupJoinEnumerable<TOuter, TInner, TKey, TResult> enumerable)
				{
					_enumerable = enumerable;
					_outerEnumerator = _enumerable._outer.GetMyEnumerator();
					_innerEnumerator = _enumerable._inner.GetMyEnumerator();
					_resultEnumerator = null;
				}

				public void Dispose()
				{
					using (_outerEnumerator)
					using (_innerEnumerator)
					{
					}
				}

				public bool MoveNext()
				{
					if (_resultEnumerator != null)
					{
						return _resultEnumerator.MoveNext();
					}

					var innersMap = new MyHashMap<TKey, MyArrayList<TInner>>(_enumerable._comparer);
					var results = new MyArrayList<Tuple<TOuter, MyArrayList<TInner>>>();
					while (_innerEnumerator.MoveNext())
					{
						var innerKey = _enumerable._innerKeySelector(_innerEnumerator.Current);
						MyArrayList<TInner> group;
						if (!innersMap.TryGetValue(innerKey, out group))
						{
							group = new MyArrayList<TInner>();
							innersMap.Add(innerKey, group);
						}
						group.Add(_innerEnumerator.Current);
					}

					while (_outerEnumerator.MoveNext())
					{
						var outerKey = _enumerable._outerKeySelector(_outerEnumerator.Current);
						MyArrayList<TInner> inners;
						results.Add(new Tuple<TOuter, MyArrayList<TInner>>(_outerEnumerator.Current,
							innersMap.TryGetValue(outerKey, out inners) ? inners : new MyArrayList<TInner>(0)));
					}

					_resultEnumerator = results.GetMyEnumerator();
					return _resultEnumerator.MoveNext();
				}

				public void Reset()
				{
					_outerEnumerator.Reset();
					_innerEnumerator.Reset();
					_resultEnumerator = null;
				}
			}
		}
	}
}