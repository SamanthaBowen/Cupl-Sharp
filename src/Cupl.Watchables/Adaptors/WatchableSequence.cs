#nullable enable

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Cupl.Watchables
{
	internal struct WatchableSequence<T> :
		IWatchableEnumerable<T>
	{
		public event Action<IEnumerable<T>>? ValueChanged
		{
			add
			{
				foreach (var watchable in watchables)
					watchable.ValueChanged += GetElementValueChangedHandler(value);
			}
			remove
			{
				foreach (var watchable in watchables)
					watchable.ValueChanged += GetElementValueChangedHandler(value);
			}
		}

		private readonly IWatchable<T>[] watchables;

		public readonly IEnumerable<T> Value => watchables.Select(w => w.Value);

		public WatchableSequence(IEnumerable<IWatchable<T>> watchables)
		{
			this.watchables = watchables.ToArray();
		}

		private readonly Action<T> GetElementValueChangedHandler(Action<IEnumerable<T>>? handler)
		{
			var value = Value;

			// By keeping the lambda here, it should be the same anonymous function each time this is called.
			return s => handler?.Invoke(value);
		}

		public IEnumerator<T> GetEnumerator() => Value.GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	}
}
