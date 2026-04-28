#nullable enable

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace Cupl.Watchables.Collections
{
	public class WatchableHashSet<T> : IWatchableEnumerable<T>, ISet<T>
		#if !NETSTANDARD2_1
			, IReadOnlySet<T>
		#endif
	{
		private struct ElementsChangedEventArgs
		{
			public IEnumerable<T> OldValues { get; }
			public IEnumerable<T> NewValues { get; }

			internal ElementsChangedEventArgs(IEnumerable<T> oldValues, IEnumerable<T> newValues)
			{
				OldValues = oldValues;
				NewValues = newValues;
			}
		}

		public event Action<IEnumerable<T>>? ValueChanged;
		private event Action<ElementsChangedEventArgs>? ElementsChanged;

		private HashSet<T> inner;

		public int Count => inner.Count;
		public IEqualityComparer<T> Comparer => inner.Comparer;

		bool ICollection<T>.IsReadOnly => false;

		IEnumerable<T> IWatchable<IEnumerable<T>>.Value => inner;

		public WatchableHashSet()
		{
			inner = new HashSet<T>();
		}

		public WatchableHashSet(IEnumerable<T> collection)
		{
			inner = new HashSet<T>(collection);
		}

		public WatchableHashSet(IEqualityComparer<T>? comparer)
		{
			inner = new HashSet<T>(comparer);
		}

		public WatchableHashSet(int capacity)
		{
			inner = new HashSet<T>(capacity);
		}

		public WatchableHashSet(IEnumerable<T> collection, IEqualityComparer<T>? comparer)
		{
			inner = new HashSet<T>(collection, comparer);
		}

		public WatchableHashSet(int capacity, IEqualityComparer<T>? comparer)
		{
			inner = new HashSet<T>(capacity, comparer);
		}

		public bool IsProperSubsetOf(IEnumerable<T> other) => inner.IsProperSubsetOf(other);
		public bool IsProperSupersetOf(IEnumerable<T> other) => inner.IsProperSupersetOf(other);
		public bool IsSubsetOf(IEnumerable<T> other) => inner.IsSubsetOf(other);
		public bool IsSupersetOf(IEnumerable<T> other) => inner.IsSupersetOf(other);
		public bool Overlaps(IEnumerable<T> other) => inner.Overlaps(other);
		public bool SetEquals(IEnumerable<T> other) => inner.SetEquals(other);
		public bool Contains(T item) => inner.Contains(item);
		public void CopyTo(T[] array, int arrayIndex) => inner.CopyTo(array, arrayIndex);
		public IEnumerator<T> GetEnumerator() => inner.GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		public int EnsureCapacity(int capacity) => inner.EnsureCapacity(capacity);

		public bool Add(T item)
		{
			if (inner.Add(item))
			{
				var valueChanged = ValueChanged;
				ElementsChanged?.Invoke(new ElementsChangedEventArgs(new T[0], new T[] { item }));
				valueChanged?.Invoke(inner);
				return true;
			}
			else
				return false;
		}

		void ICollection<T>.Add(T item) => Add(item);

		public void ExceptWith(IEnumerable<T> other)
		{
			var valueChanged = ValueChanged;

			if (ElementsChanged is Action<ElementsChangedEventArgs> elementsChanged)
			{
				var removedItems = other.Where(inner.Contains).Distinct().ToArray();
				inner.ExceptWith(removedItems);
				elementsChanged.Invoke(new ElementsChangedEventArgs(removedItems, new T[0]));
			}
			else
				inner.ExceptWith(other);

			valueChanged?.Invoke(inner);
		}

		public void IntersectWith(IEnumerable<T> other)
		{
			var valueChanged = ValueChanged;

			if (ElementsChanged is Action<ElementsChangedEventArgs> elementsChanged)
			{
				var removedItems = other.Where(item => !inner.Contains(item)).Distinct().ToArray();
				inner.IntersectWith(removedItems);
				elementsChanged.Invoke(new ElementsChangedEventArgs(removedItems, new T[0]));
			}
			else
				inner.IntersectWith(other);

			valueChanged?.Invoke(inner);
		}

		public void SymmetricExceptWith(IEnumerable<T> other)
		{
			var valueChanged = ValueChanged;

			if (ElementsChanged is Action<ElementsChangedEventArgs> elementsChanged)
			{
				var affectedItemsLookup = other.Distinct().ToLookup(inner.Contains);
				var removedItems = affectedItemsLookup[true].ToArray();
				var addedItems = affectedItemsLookup[false].ToArray();
				inner.SymmetricExceptWith(removedItems.Concat(addedItems));
				elementsChanged.Invoke(new ElementsChangedEventArgs(removedItems, addedItems));
			}
			else
				inner.SymmetricExceptWith(other);

			valueChanged?.Invoke(inner);
		}

		public void UnionWith(IEnumerable<T> other)
		{
			var valueChanged = ValueChanged;

			if (ElementsChanged is Action<ElementsChangedEventArgs> elementsChanged)
			{
				var addedItems = other.Distinct().Where(item => !inner.Contains(item)).ToArray();
				inner.UnionWith(addedItems);
				elementsChanged.Invoke(new ElementsChangedEventArgs(new T[0], addedItems));
			}
			else
				inner.UnionWith(other);

			valueChanged?.Invoke(inner);
		}

		public void Clear()
		{
			var valueChanged = ValueChanged;

			if (ElementsChanged is Action<ElementsChangedEventArgs> elementsChanged)
			{
				var oldValues = inner.ToArray();
				inner.Clear();
				elementsChanged.Invoke(new ElementsChangedEventArgs(oldValues, new T[0]));
			}
			else
				inner.Clear();

			valueChanged?.Invoke(inner);
		}

		public bool Remove(T item)
		{
			if (inner.Remove(item))
			{
				var valueChanged = ValueChanged;
				ElementsChanged?.Invoke(new ElementsChangedEventArgs(new T[] { item }, new T[0]));
				valueChanged?.Invoke(inner);
				return true;
			}
			else
				return false;
		}
	}
}
