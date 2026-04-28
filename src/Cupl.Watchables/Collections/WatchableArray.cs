#nullable enable

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace Cupl.Watchables.Collections
{
	public class WatchableArray<T> : IWatchableEnumerable<T>, IList<T>, IReadOnlyList<T>, INotifyCollectionChanged
	{
		public struct WatchablesEnumerable : IEnumerable<IWatchable<T>>
		{
			private WatchableArray<T> collection;

			public IWatchable<T> this[int index] =>
				(0 <= index && index < collection.Length)
					? new WatchableElement(collection, index)
					: throw new IndexOutOfRangeException();

			internal WatchablesEnumerable(WatchableArray<T> collection)
			{
				this.collection = collection;
			}

			public IEnumerator<IWatchable<T>> GetEnumerator()
			{
				var collection = this.collection;
				return Enumerable.Range(0, collection.Length).Select(index => (IWatchable<T>)new WatchableElement(collection, index)).GetEnumerator();
			}

			IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
		}

		private struct ElementsReplacedEventArgs
		{
			public int StartIndex { get; }
			public IReadOnlyList<T> OldValues { get; }
			public IReadOnlyList<T> NewValues { get; }

			internal ElementsReplacedEventArgs(int startIndex, IReadOnlyList<T> oldValues, IReadOnlyList<T> newValues)
			{
				StartIndex = startIndex;
				OldValues = oldValues;
				NewValues = newValues;
			}
		}

		private readonly struct WatchableElement : IWatchable<T>
		{
			public event Action<T>? ValueChanged
			{
				add => collection.ElementsReplaced += GetElementChangedHandler(value);
				remove => collection.ElementsReplaced -= GetElementChangedHandler(value);
			}

			private readonly WatchableArray<T> collection;
			private readonly int index;

			internal WatchableElement(WatchableArray<T> collection, int index)
			{
				this.collection = collection;
				this.index = index;
			}

			public T Value => collection[index];

			private Action<ElementsReplacedEventArgs> GetElementChangedHandler(Action<T>? valueChangedHandler)
			{
				var index = this.index;

				return
					e =>
					{
						var newItemsIndex = index - e.StartIndex;
						if (0 <= newItemsIndex && newItemsIndex < e.NewValues.Count)
							valueChangedHandler?.Invoke(e.NewValues[newItemsIndex]);
					};
			}
		}

		public event Action<IEnumerable<T>>? ValueChanged;
		private event Action<ElementsReplacedEventArgs>? ElementsReplaced;

		public event NotifyCollectionChangedEventHandler? CollectionChanged
		{
			add => ElementsReplaced += GetElementsReplacedHandler(value);
			remove => ElementsReplaced -= GetElementsReplacedHandler(value);
		}

		private T[] inner;

		public T this[int index]
		{
			get => inner[index];
			set
			{
				var valueChanged = ValueChanged;

				if (ElementsReplaced is Action<ElementsReplacedEventArgs> elementsReplaced)
				{
					var oldValue = inner[index];
					inner[index] = value;
					elementsReplaced?.Invoke(new ElementsReplacedEventArgs(index, new T[] { oldValue }, new T[] { value }));
				}
				else
					inner[index] = value;

				valueChanged?.Invoke(inner);
			}
		}

		public WatchablesEnumerable Watchables => new WatchablesEnumerable(this);

		public int Length => inner.Length;
		int ICollection<T>.Count => Length;
		int IReadOnlyCollection<T>.Count => Length;

		bool ICollection<T>.IsReadOnly => false;

		IEnumerable<T> IWatchable<IEnumerable<T>>.Value => inner;

		public WatchableArray(int length)
		{
			inner = new T[length];
		}

		public WatchableArray(IEnumerable<T> collection)
		{
			inner = collection.ToArray();
		}

		public IEnumerator<T> GetEnumerator()
		{
			return ((IEnumerable<T>)inner).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		public int IndexOf(T item) => Array.IndexOf<T>(inner, item);
		public bool Contains(T item) => inner.Contains(item);
		public void CopyTo(T[] array, int arrayIndex) => inner.CopyTo(array, arrayIndex);

		public void CopyFrom(int arrayIndex, IList<T> list)
		{
			var valueChanged = ValueChanged;

			if (ElementsReplaced is Action<ElementsReplacedEventArgs> elementsReplaced)
			{
				var count = list.Count;
				var oldValues = inner.Skip(arrayIndex).Take(count).ToArray();
				list.CopyTo(inner, arrayIndex);
				elementsReplaced?.Invoke(new ElementsReplacedEventArgs(arrayIndex, oldValues, inner.Skip(arrayIndex).Take(count).ToArray()));
			}
			else
				list.CopyTo(inner, arrayIndex);
			
			valueChanged?.Invoke(this);
		}

		void IList<T>.Insert(int index, T item) => throw new NotSupportedException();
		void IList<T>.RemoveAt(int index) => throw new NotSupportedException();
		void ICollection<T>.Add(T item) => throw new NotSupportedException();
		void ICollection<T>.Clear() => throw new NotSupportedException();
		bool ICollection<T>.Remove(T item) => throw new NotSupportedException();

		private Action<ElementsReplacedEventArgs> GetElementsReplacedHandler(NotifyCollectionChangedEventHandler? collectionChangedEventHandler)
		{
			return
				e => collectionChangedEventHandler?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, (IList)e.NewValues, (IList)e.OldValues, e.StartIndex));
		}
	}
}
