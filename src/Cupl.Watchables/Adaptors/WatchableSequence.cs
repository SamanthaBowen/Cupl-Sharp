#nullable enable

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cupl.Watchables.Collections;

namespace Cupl.Watchables
{
	internal class WatchableSequence<T> :
		IWatchableEnumerable<T>
	{
		private Action<IEnumerable<T>>? valueChanged;
		public event Action<IEnumerable<T>>? ValueChanged
		{
			add
			{
				if (valueChanged == null)
				{
					foreach (var watchable in watchables)
						watchable.ValueChanged += HandleElementValueChanged;
				}
				valueChanged += value;
			}
			remove
			{
				valueChanged -= value;
				if (valueChanged == null)
				{
					foreach (var watchable in watchables)
						watchable.ValueChanged -= HandleElementValueChanged;
				}
			}
		}

		private readonly IWatchable<T>[] watchables;

		public IEnumerable<T> Value => watchables.Select(w => w.Value);

		public WatchableSequence(IEnumerable<IWatchable<T>> watchables)
		{
			this.watchables = watchables.ToArray();
		}

		private void HandleElementValueChanged(T _)
		{
			valueChanged?.Invoke(Value);
		}

		public IEnumerator<T> GetEnumerator() => Value.GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	}
}
