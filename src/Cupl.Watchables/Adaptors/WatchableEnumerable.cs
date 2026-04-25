#nullable enable

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cupl.Watchables.Collections;

namespace Cupl.Watchables
{
	internal struct WatchableEnumerable<T> :
		IWatchableEnumerable<T>
	{
		public event Action<IEnumerable<T>>? ValueChanged
		{
			add => watchable.ValueChanged += value;
			remove => watchable.ValueChanged -= value;
		}

		private readonly IWatchable<IEnumerable<T>> watchable;

		public readonly IEnumerable<T> Value => watchable.Value;

		public WatchableEnumerable(IWatchable<IEnumerable<T>> watchable)
		{
			this.watchable = watchable;
		}

		public IEnumerator<T> GetEnumerator() => Value.GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	}
}
