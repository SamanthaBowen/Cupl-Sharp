#nullable enable

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Cupl.Watchables
{
	internal readonly struct WatchableTuple<T1, T2> :
		IWatchable<(T1, T2)>
	{
		public event Action<(T1, T2)>? ValueChanged
		{
			add
			{
				Watchable1.ValueChanged += GetElementValueChangedHandler<T1>(value);
				Watchable2.ValueChanged += GetElementValueChangedHandler<T2>(value);
			}
			remove
			{
				Watchable1.ValueChanged -= GetElementValueChangedHandler<T1>(value);
				Watchable2.ValueChanged -= GetElementValueChangedHandler<T2>(value);
			}
		}

		public readonly IWatchable<T1> Watchable1 { get; }
		public readonly IWatchable<T2> Watchable2 { get; }

		public readonly (T1, T2) Value => (Watchable1.Value, Watchable2.Value);

		public WatchableTuple(IWatchable<T1> watchable1, IWatchable<T2> watchable2)
		{
			Watchable1 = watchable1;
			Watchable2 = watchable2;
		}

		private readonly Action<T> GetElementValueChangedHandler<T>(Action<(T1, T2)>? handler)
		{
			// Making a copy is okay because this is readonly.
			var this_ = this;

			// By keeping the lambda here, it should be the same anonymous function each time this is called.
			return s => handler?.Invoke(this_.Value);
		}
	}
}
