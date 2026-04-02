#nullable enable

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Cupl.Watchables
{
	internal readonly struct WatchableTuple<T1, T2> :
		IWatchable<(T1, T2)>, IWatchable<Tuple<T1, T2>>
	{
		private readonly Action<(T1, T2)> GetValueChangedHandler(Action<Tuple<T1, T2>>? handler)
		{
			// By keeping the lambda here, it should be the same anonymous function each time this is called.
			return tuple => handler?.Invoke(tuple.ToTuple());
		}

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

		event Action<Tuple<T1, T2>>? IWatchable<Tuple<T1, T2>>.ValueChanged
		{
			add => ValueChanged += GetValueChangedHandler(value);
			remove => ValueChanged -= GetValueChangedHandler(value);
		}

		public readonly IWatchable<T1> Watchable1 { get; }
		public readonly IWatchable<T2> Watchable2 { get; }

		public readonly (T1, T2) Value => (Watchable1.Value, Watchable2.Value);
		readonly Tuple<T1, T2> IWatchable<Tuple<T1, T2>>.Value => Value.ToTuple();

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

	internal readonly struct WatchableTuple<T1, T2, T3> :
		IWatchable<(T1, T2, T3)>, IWatchable<Tuple<T1, T2, T3>>
	{
		private readonly Action<(T1, T2, T3)> GetValueChangedHandler(Action<Tuple<T1, T2, T3>>? handler)
		{
			// By keeping the lambda here, it should be the same anonymous function each time this is called.
			return tuple => handler?.Invoke(tuple.ToTuple());
		}

		public event Action<(T1, T2, T3)>? ValueChanged
		{
			add
			{
				Watchable1.ValueChanged += GetElementValueChangedHandler<T1>(value);
				Watchable2.ValueChanged += GetElementValueChangedHandler<T2>(value);
				Watchable3.ValueChanged += GetElementValueChangedHandler<T3>(value);
			}
			remove
			{
				Watchable1.ValueChanged -= GetElementValueChangedHandler<T1>(value);
				Watchable2.ValueChanged -= GetElementValueChangedHandler<T2>(value);
				Watchable3.ValueChanged -= GetElementValueChangedHandler<T3>(value);
			}
		}

		event Action<Tuple<T1, T2, T3>>? IWatchable<Tuple<T1, T2, T3>>.ValueChanged
		{
			add => ValueChanged += GetValueChangedHandler(value);
			remove => ValueChanged -= GetValueChangedHandler(value);
		}

		public readonly IWatchable<T1> Watchable1 { get; }
		public readonly IWatchable<T2> Watchable2 { get; }
		public readonly IWatchable<T3> Watchable3 { get; }

		public readonly (T1, T2, T3) Value => (Watchable1.Value, Watchable2.Value, Watchable3.Value);
		readonly Tuple<T1, T2, T3> IWatchable<Tuple<T1, T2, T3>>.Value => Value.ToTuple();

		public WatchableTuple(IWatchable<T1> watchable1, IWatchable<T2> watchable2, IWatchable<T3> watchable3)
		{
			Watchable1 = watchable1;
			Watchable2 = watchable2;
			Watchable3 = watchable3;
		}

		private readonly Action<T> GetElementValueChangedHandler<T>(Action<(T1, T2, T3)>? handler)
		{
			// Making a copy is okay because this is readonly.
			var this_ = this;

			// By keeping the lambda here, it should be the same anonymous function each time this is called.
			return s => handler?.Invoke(this_.Value);
		}
	}

	internal readonly struct WatchableTuple<T1, T2, T3, T4> :
		IWatchable<(T1, T2, T3, T4)>, IWatchable<Tuple<T1, T2, T3, T4>>
	{
		private readonly Action<(T1, T2, T3, T4)> GetValueChangedHandler(Action<Tuple<T1, T2, T3, T4>>? handler)
		{
			// By keeping the lambda here, it should be the same anonymous function each time this is called.
			return tuple => handler?.Invoke(tuple.ToTuple());
		}

		public event Action<(T1, T2, T3, T4)>? ValueChanged
		{
			add
			{
				Watchable1.ValueChanged += GetElementValueChangedHandler<T1>(value);
				Watchable2.ValueChanged += GetElementValueChangedHandler<T2>(value);
				Watchable3.ValueChanged += GetElementValueChangedHandler<T3>(value);
				Watchable4.ValueChanged += GetElementValueChangedHandler<T4>(value);
			}
			remove
			{
				Watchable1.ValueChanged -= GetElementValueChangedHandler<T1>(value);
				Watchable2.ValueChanged -= GetElementValueChangedHandler<T2>(value);
				Watchable3.ValueChanged -= GetElementValueChangedHandler<T3>(value);
				Watchable4.ValueChanged -= GetElementValueChangedHandler<T4>(value);
			}
		}

		event Action<Tuple<T1, T2, T3, T4>>? IWatchable<Tuple<T1, T2, T3, T4>>.ValueChanged
		{
			add => ValueChanged += GetValueChangedHandler(value);
			remove => ValueChanged -= GetValueChangedHandler(value);
		}

		public readonly IWatchable<T1> Watchable1 { get; }
		public readonly IWatchable<T2> Watchable2 { get; }
		public readonly IWatchable<T3> Watchable3 { get; }
		public readonly IWatchable<T4> Watchable4 { get; }

		public readonly (T1, T2, T3, T4) Value => (Watchable1.Value, Watchable2.Value, Watchable3.Value, Watchable4.Value);
		readonly Tuple<T1, T2, T3, T4> IWatchable<Tuple<T1, T2, T3, T4>>.Value => Value.ToTuple();

		public WatchableTuple(IWatchable<T1> watchable1, IWatchable<T2> watchable2, IWatchable<T3> watchable3, IWatchable<T4> watchable4)
		{
			Watchable1 = watchable1;
			Watchable2 = watchable2;
			Watchable3 = watchable3;
			Watchable4 = watchable4;
		}

		private readonly Action<T> GetElementValueChangedHandler<T>(Action<(T1, T2, T3, T4)>? handler)
		{
			// Making a copy is okay because this is readonly.
			var this_ = this;

			// By keeping the lambda here, it should be the same anonymous function each time this is called.
			return s => handler?.Invoke(this_.Value);
		}
	}

	internal readonly struct WatchableTuple<T1, T2, T3, T4, T5> :
		IWatchable<(T1, T2, T3, T4, T5)>, IWatchable<Tuple<T1, T2, T3, T4, T5>>
	{
		private readonly Action<(T1, T2, T3, T4, T5)> GetValueChangedHandler(Action<Tuple<T1, T2, T3, T4, T5>>? handler)
		{
			// By keeping the lambda here, it should be the same anonymous function each time this is called.
			return tuple => handler?.Invoke(tuple.ToTuple());
		}

		public event Action<(T1, T2, T3, T4, T5)>? ValueChanged
		{
			add
			{
				Watchable1.ValueChanged += GetElementValueChangedHandler<T1>(value);
				Watchable2.ValueChanged += GetElementValueChangedHandler<T2>(value);
				Watchable3.ValueChanged += GetElementValueChangedHandler<T3>(value);
				Watchable4.ValueChanged += GetElementValueChangedHandler<T4>(value);
				Watchable5.ValueChanged += GetElementValueChangedHandler<T5>(value);
			}
			remove
			{
				Watchable1.ValueChanged -= GetElementValueChangedHandler<T1>(value);
				Watchable2.ValueChanged -= GetElementValueChangedHandler<T2>(value);
				Watchable3.ValueChanged -= GetElementValueChangedHandler<T3>(value);
				Watchable4.ValueChanged -= GetElementValueChangedHandler<T4>(value);
				Watchable5.ValueChanged -= GetElementValueChangedHandler<T5>(value);
			}
		}

		event Action<Tuple<T1, T2, T3, T4, T5>>? IWatchable<Tuple<T1, T2, T3, T4, T5>>.ValueChanged
		{
			add => ValueChanged += GetValueChangedHandler(value);
			remove => ValueChanged -= GetValueChangedHandler(value);
		}

		public readonly IWatchable<T1> Watchable1 { get; }
		public readonly IWatchable<T2> Watchable2 { get; }
		public readonly IWatchable<T3> Watchable3 { get; }
		public readonly IWatchable<T4> Watchable4 { get; }
		public readonly IWatchable<T5> Watchable5 { get; }

		public readonly (T1, T2, T3, T4, T5) Value =>
			(Watchable1.Value, Watchable2.Value, Watchable3.Value, Watchable4.Value, Watchable5.Value);
		readonly Tuple<T1, T2, T3, T4, T5> IWatchable<Tuple<T1, T2, T3, T4, T5>>.Value => Value.ToTuple();

		public WatchableTuple
		(
			IWatchable<T1> watchable1, IWatchable<T2> watchable2, IWatchable<T3> watchable3, IWatchable<T4> watchable4,
			IWatchable<T5> watchable5
		)
		{
			Watchable1 = watchable1;
			Watchable2 = watchable2;
			Watchable3 = watchable3;
			Watchable4 = watchable4;
			Watchable5 = watchable5;
		}

		private readonly Action<T> GetElementValueChangedHandler<T>(Action<(T1, T2, T3, T4, T5)>? handler)
		{
			// Making a copy is okay because this is readonly.
			var this_ = this;

			// By keeping the lambda here, it should be the same anonymous function each time this is called.
			return s => handler?.Invoke(this_.Value);
		}
	}

	internal readonly struct WatchableTuple<T1, T2, T3, T4, T5, T6> :
		IWatchable<(T1, T2, T3, T4, T5, T6)>, IWatchable<Tuple<T1, T2, T3, T4, T5, T6>>
	{
		private readonly Action<(T1, T2, T3, T4, T5, T6)> GetValueChangedHandler(Action<Tuple<T1, T2, T3, T4, T5, T6>>? handler)
		{
			// By keeping the lambda here, it should be the same anonymous function each time this is called.
			return tuple => handler?.Invoke(tuple.ToTuple());
		}

		public event Action<(T1, T2, T3, T4, T5, T6)>? ValueChanged
		{
			add
			{
				Watchable1.ValueChanged += GetElementValueChangedHandler<T1>(value);
				Watchable2.ValueChanged += GetElementValueChangedHandler<T2>(value);
				Watchable3.ValueChanged += GetElementValueChangedHandler<T3>(value);
				Watchable4.ValueChanged += GetElementValueChangedHandler<T4>(value);
				Watchable5.ValueChanged += GetElementValueChangedHandler<T5>(value);
				Watchable6.ValueChanged += GetElementValueChangedHandler<T6>(value);
			}
			remove
			{
				Watchable1.ValueChanged -= GetElementValueChangedHandler<T1>(value);
				Watchable2.ValueChanged -= GetElementValueChangedHandler<T2>(value);
				Watchable3.ValueChanged -= GetElementValueChangedHandler<T3>(value);
				Watchable4.ValueChanged -= GetElementValueChangedHandler<T4>(value);
				Watchable5.ValueChanged -= GetElementValueChangedHandler<T5>(value);
				Watchable6.ValueChanged -= GetElementValueChangedHandler<T6>(value);
			}
		}

		event Action<Tuple<T1, T2, T3, T4, T5, T6>>? IWatchable<Tuple<T1, T2, T3, T4, T5, T6>>.ValueChanged
		{
			add => ValueChanged += GetValueChangedHandler(value);
			remove => ValueChanged -= GetValueChangedHandler(value);
		}

		public readonly IWatchable<T1> Watchable1 { get; }
		public readonly IWatchable<T2> Watchable2 { get; }
		public readonly IWatchable<T3> Watchable3 { get; }
		public readonly IWatchable<T4> Watchable4 { get; }
		public readonly IWatchable<T5> Watchable5 { get; }
		public readonly IWatchable<T6> Watchable6 { get; }

		public readonly (T1, T2, T3, T4, T5, T6) Value =>
			(Watchable1.Value, Watchable2.Value, Watchable3.Value, Watchable4.Value, Watchable5.Value, Watchable6.Value);
		readonly Tuple<T1, T2, T3, T4, T5, T6> IWatchable<Tuple<T1, T2, T3, T4, T5, T6>>.Value => Value.ToTuple();

		public WatchableTuple
		(
			IWatchable<T1> watchable1, IWatchable<T2> watchable2, IWatchable<T3> watchable3, IWatchable<T4> watchable4,
			IWatchable<T5> watchable5, IWatchable<T6> watchable6
		)
		{
			Watchable1 = watchable1;
			Watchable2 = watchable2;
			Watchable3 = watchable3;
			Watchable4 = watchable4;
			Watchable5 = watchable5;
			Watchable6 = watchable6;
		}

		private readonly Action<T> GetElementValueChangedHandler<T>(Action<(T1, T2, T3, T4, T5, T6)>? handler)
		{
			// Making a copy is okay because this is readonly.
			var this_ = this;

			// By keeping the lambda here, it should be the same anonymous function each time this is called.
			return s => handler?.Invoke(this_.Value);
		}
	}

	internal readonly struct WatchableTuple<T1, T2, T3, T4, T5, T6, T7> :
		IWatchable<(T1, T2, T3, T4, T5, T6, T7)>, IWatchable<Tuple<T1, T2, T3, T4, T5, T6, T7>>
	{
		private readonly Action<(T1, T2, T3, T4, T5, T6, T7)> GetValueChangedHandler(Action<Tuple<T1, T2, T3, T4, T5, T6, T7>>? handler)
		{
			// By keeping the lambda here, it should be the same anonymous function each time this is called.
			return tuple => handler?.Invoke(tuple.ToTuple());
		}

		public event Action<(T1, T2, T3, T4, T5, T6, T7)>? ValueChanged
		{
			add
			{
				Watchable1.ValueChanged += GetElementValueChangedHandler<T1>(value);
				Watchable2.ValueChanged += GetElementValueChangedHandler<T2>(value);
				Watchable3.ValueChanged += GetElementValueChangedHandler<T3>(value);
				Watchable4.ValueChanged += GetElementValueChangedHandler<T4>(value);
				Watchable5.ValueChanged += GetElementValueChangedHandler<T5>(value);
				Watchable6.ValueChanged += GetElementValueChangedHandler<T6>(value);
				Watchable7.ValueChanged += GetElementValueChangedHandler<T7>(value);
			}
			remove
			{
				Watchable1.ValueChanged -= GetElementValueChangedHandler<T1>(value);
				Watchable2.ValueChanged -= GetElementValueChangedHandler<T2>(value);
				Watchable3.ValueChanged -= GetElementValueChangedHandler<T3>(value);
				Watchable4.ValueChanged -= GetElementValueChangedHandler<T4>(value);
				Watchable5.ValueChanged -= GetElementValueChangedHandler<T5>(value);
				Watchable6.ValueChanged -= GetElementValueChangedHandler<T6>(value);
				Watchable7.ValueChanged -= GetElementValueChangedHandler<T7>(value);
			}
		}

		event Action<Tuple<T1, T2, T3, T4, T5, T6, T7>>? IWatchable<Tuple<T1, T2, T3, T4, T5, T6, T7>>.ValueChanged
		{
			add => ValueChanged += GetValueChangedHandler(value);
			remove => ValueChanged -= GetValueChangedHandler(value);
		}

		public readonly IWatchable<T1> Watchable1 { get; }
		public readonly IWatchable<T2> Watchable2 { get; }
		public readonly IWatchable<T3> Watchable3 { get; }
		public readonly IWatchable<T4> Watchable4 { get; }
		public readonly IWatchable<T5> Watchable5 { get; }
		public readonly IWatchable<T6> Watchable6 { get; }
		public readonly IWatchable<T7> Watchable7 { get; }

		public readonly (T1, T2, T3, T4, T5, T6, T7) Value =>
			(Watchable1.Value, Watchable2.Value, Watchable3.Value, Watchable4.Value, Watchable5.Value, Watchable6.Value, Watchable7.Value);
		readonly Tuple<T1, T2, T3, T4, T5, T6, T7> IWatchable<Tuple<T1, T2, T3, T4, T5, T6, T7>>.Value => Value.ToTuple();

		public WatchableTuple
		(
			IWatchable<T1> watchable1, IWatchable<T2> watchable2, IWatchable<T3> watchable3, IWatchable<T4> watchable4,
			IWatchable<T5> watchable5, IWatchable<T6> watchable6, IWatchable<T7> watchable7
		)
		{
			Watchable1 = watchable1;
			Watchable2 = watchable2;
			Watchable3 = watchable3;
			Watchable4 = watchable4;
			Watchable5 = watchable5;
			Watchable6 = watchable6;
			Watchable7 = watchable7;
		}

		private readonly Action<T> GetElementValueChangedHandler<T>(Action<(T1, T2, T3, T4, T5, T6, T7)>? handler)
		{
			// Making a copy is okay because this is readonly.
			var this_ = this;

			// By keeping the lambda here, it should be the same anonymous function each time this is called.
			return s => handler?.Invoke(this_.Value);
		}
	}
}
