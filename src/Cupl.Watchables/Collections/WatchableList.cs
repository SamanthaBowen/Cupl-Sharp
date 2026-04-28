#nullable enable

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace Cupl.Watchables.Collections
{
	public class WatchableList<T> : IWatchableEnumerable<T>, IList<T>, IReadOnlyList<T>, INotifyCollectionChanged
	{
		private struct ElementsChangedEventArgs
		{
			public int OldStartIndex { get; }
			public IReadOnlyList<T> OldValues { get; }
			public int NewStartIndex { get; }
			public IReadOnlyList<T> NewValues { get; }

			internal ElementsChangedEventArgs(int oldStartIndex, IReadOnlyList<T> oldValues, int newStartIndex, IReadOnlyList<T> newValues)
			{
				OldStartIndex = oldStartIndex;
				OldValues = oldValues;
				NewStartIndex = newStartIndex;
				NewValues = newValues;
			}
		}

		public event Action<IEnumerable<T>>? ValueChanged;
		private event Action<ElementsChangedEventArgs>? ElementsChanged;

		public event NotifyCollectionChangedEventHandler? CollectionChanged
		{
			add => ElementsChanged += GetElementsChangedHandler(value);
			remove => ElementsChanged -= GetElementsChangedHandler(value);
		}

		private List<T> inner;

		public T this[int index]
		{
			get => inner[index];
			set
			{
				var valueChanged = ValueChanged;

				if (ElementsChanged is Action<ElementsChangedEventArgs> elementsChanged)
				{
					var oldValue = inner[index];
					inner[index] = value;
					elementsChanged?.Invoke(new ElementsChangedEventArgs(index, new T[] { oldValue }, index, new T[] { value }));
				}
				else
					inner[index] = value;

				valueChanged?.Invoke(inner);
			}
		}

		public int Count => inner.Count;

		bool ICollection<T>.IsReadOnly => false;

		public int Capacity
		{
			get => inner.Capacity;
			set => inner.Capacity = value;
		}

		IEnumerable<T> IWatchable<IEnumerable<T>>.Value => inner;

		public WatchableList()
		{
			inner = new List<T>();
		}

		public WatchableList(IEnumerable<T> collection)
		{
			inner = new List<T>(collection);
		}

		public WatchableList(int capacity)
		{
			inner = new List<T>(capacity);
		}

		public IEnumerator<T> GetEnumerator() => inner.GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		public bool Contains(T item) => inner.Contains(item);
		public void CopyTo(T[] array, int arrayIndex) => inner.CopyTo(array, arrayIndex);
		public int EnsureCapacity(int capacity) => inner.Capacity;
		public int IndexOf(T item) => inner.IndexOf(item);
		public void TrimExcess() => inner.TrimExcess();

		public void Add(T item)
		{
			var valueChanged = ValueChanged;

			if (ElementsChanged is Action<ElementsChangedEventArgs> elementsChanged)
			{
				int index = Count;
				inner.Add(item);
				elementsChanged?.Invoke(new ElementsChangedEventArgs(index, new T[0], index, new T[] { item }));
			}
			else
				inner.Add(item);

			valueChanged?.Invoke(inner);
		}

		public void AddRange(ICollection<T> collection)
		{
			var valueChanged = ValueChanged;

			if (ElementsChanged is Action<ElementsChangedEventArgs> elementsChanged)
			{
				int index = Count;
				var items = collection.ToArray();
				inner.AddRange(items);
				elementsChanged?.Invoke(new ElementsChangedEventArgs(index, new T[0], index, items));
			}
			else
				inner.AddRange(collection);

			valueChanged?.Invoke(inner);
		}

		public void Clear()
		{
			var valueChanged = ValueChanged;

			if (ElementsChanged is Action<ElementsChangedEventArgs> elementsChanged)
			{
				var oldValues = inner.ToArray();
				inner.Clear();
				elementsChanged?.Invoke(new ElementsChangedEventArgs(0, oldValues, 0, new T[0]));
			}
			else
				inner.Clear();

			valueChanged?.Invoke(inner);
		}

		public void Insert(int index, T item)
		{
			var valueChanged = ValueChanged;

			if (ElementsChanged is Action<ElementsChangedEventArgs> elementsChanged)
			{
				inner.Insert(index, item);
				elementsChanged?.Invoke(new ElementsChangedEventArgs(index, new T[0], index, new T[] { item }));
			}
			else
				inner.Insert(index, item);

			valueChanged?.Invoke(inner);
		}

		public void InsertRange(int index, ICollection<T> collection)
		{
			var valueChanged = ValueChanged;

			if (ElementsChanged is Action<ElementsChangedEventArgs> elementsChanged)
			{
				var items = collection.ToArray();
				inner.InsertRange(index, items);
				elementsChanged?.Invoke(new ElementsChangedEventArgs(index, new T[0], index, items));
			}
			else
				inner.InsertRange(index, collection);

			valueChanged?.Invoke(inner);
		}

		public bool Remove(T item)
		{
			var index = inner.IndexOf(item);

			if (0 <= index)
			{
				RemoveAt(index);
				return true;
			}
			else
				return false;
		}

		public void RemoveAt(int index)
		{
			var valueChanged = ValueChanged;

			if (ElementsChanged is Action<ElementsChangedEventArgs> elementsChanged)
			{
				var oldValue = inner[index];
				inner.RemoveAt(index);
				elementsChanged?.Invoke(new ElementsChangedEventArgs(index, new T[] { oldValue }, index, new T[0]));
			}
			else
				inner.RemoveAt(index);

			valueChanged?.Invoke(inner);
		}

		public void RemoveRange(int index, int count)
		{
			var valueChanged = ValueChanged;

			if (ElementsChanged is Action<ElementsChangedEventArgs> elementsChanged)
			{
				var oldValues = inner.Skip(index).Take(count).ToArray();
				inner.RemoveRange(index, count);
				elementsChanged?.Invoke(new ElementsChangedEventArgs(index, oldValues, index, new T[0]));
			}
			else
				inner.RemoveRange(index, count);

			valueChanged?.Invoke(inner);
		}

		private Action<ElementsChangedEventArgs> GetElementsChangedHandler(NotifyCollectionChangedEventHandler? collectionChangedEventHandler)
		{
			return
				e =>
				{
					if (e.OldValues == e.NewValues)
						collectionChangedEventHandler?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Move, e.NewValues, e.NewStartIndex, e.OldStartIndex));
					else if (e.OldStartIndex == e.NewStartIndex && e.OldValues.Count == e.NewValues.Count)
						collectionChangedEventHandler?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, (IList)e.NewValues, (IList)e.OldValues, e.OldStartIndex));
					else
					{
						if (e.OldValues.Any())
							collectionChangedEventHandler?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, (IList)e.OldValues, e.OldStartIndex));

						if (e.NewValues.Any())
							collectionChangedEventHandler?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, (IList)e.NewValues, e.NewStartIndex));
					}
				};
		}
	}
}
