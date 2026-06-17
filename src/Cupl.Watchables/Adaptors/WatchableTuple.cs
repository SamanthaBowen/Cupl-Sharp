#nullable enable

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Cupl.Watchables
{
	internal class WatchableTuple<T1, T2> :
		IWatchable<(T1, T2)>, IWatchable<Tuple<T1, T2>>
	{
		private Action<(T1, T2)>? valueChanged;
		public event Action<(T1, T2)>? ValueChanged
		{
			add
			{
				if (valueChanged == null)
				{
					Watchable1.ValueChanged += HandleElement1ValueChanged;
					Watchable2.ValueChanged += HandleElement2ValueChanged;
				}
				valueChanged += value;
			}
			remove
			{
				valueChanged -= value;
				if (valueChanged == null)
				{
					Watchable1.ValueChanged -= HandleElement1ValueChanged;
					Watchable2.ValueChanged -= HandleElement2ValueChanged;
				}
			}
		}

		private Action<Tuple<T1, T2>>? tupleValueChanged;
		event Action<Tuple<T1, T2>>? IWatchable<Tuple<T1, T2>>.ValueChanged
		{
			add
			{
				if (tupleValueChanged == null)
					ValueChanged += OnTupleValueChanged;
				tupleValueChanged += value;
			}
			remove
			{
				tupleValueChanged -= value;
				if (tupleValueChanged == null)
					ValueChanged -= OnTupleValueChanged;
			}
		}

		public IWatchable<T1> Watchable1 { get; }
		public IWatchable<T2> Watchable2 { get; }

		public (T1, T2) Value => (Watchable1.Value, Watchable2.Value);
		Tuple<T1, T2> IWatchable<Tuple<T1, T2>>.Value => Value.ToTuple();

		public WatchableTuple(IWatchable<T1> watchable1, IWatchable<T2> watchable2)
		{
			Watchable1 = watchable1;
			Watchable2 = watchable2;
		}

		private void OnTupleValueChanged((T1, T2) value) => tupleValueChanged?.Invoke(value.ToTuple());

		private void HandleElement1ValueChanged(T1 _) => valueChanged?.Invoke(Value);
		private void HandleElement2ValueChanged(T2 _) => valueChanged?.Invoke(Value);
	}

	internal class WatchableTuple<T1, T2, T3> :
		IWatchable<(T1, T2, T3)>, IWatchable<Tuple<T1, T2, T3>>
	{
		private Action<(T1, T2, T3)>? valueChanged;
		public event Action<(T1, T2, T3)>? ValueChanged
		{
			add
			{
				if (valueChanged == null)
				{
					Watchable1.ValueChanged += HandleElement1ValueChanged;
					Watchable2.ValueChanged += HandleElement2ValueChanged;
					Watchable3.ValueChanged += HandleElement3ValueChanged;
				}
				valueChanged += value;
			}
			remove
			{
				valueChanged -= value;
				if (valueChanged == null)
				{
					Watchable1.ValueChanged -= HandleElement1ValueChanged;
					Watchable2.ValueChanged -= HandleElement2ValueChanged;
					Watchable3.ValueChanged -= HandleElement3ValueChanged;
				}
			}
		}

		private Action<Tuple<T1, T2, T3>>? tupleValueChanged;
		event Action<Tuple<T1, T2, T3>>? IWatchable<Tuple<T1, T2, T3>>.ValueChanged
		{
			add
			{
				if (tupleValueChanged == null)
					ValueChanged += OnTupleValueChanged;
				tupleValueChanged += value;
			}
			remove
			{
				tupleValueChanged -= value;
				if (tupleValueChanged == null)
					ValueChanged -= OnTupleValueChanged;
			}
		}

		public IWatchable<T1> Watchable1 { get; }
		public IWatchable<T2> Watchable2 { get; }
		public IWatchable<T3> Watchable3 { get; }

		public (T1, T2, T3) Value => (Watchable1.Value, Watchable2.Value, Watchable3.Value);
		Tuple<T1, T2, T3> IWatchable<Tuple<T1, T2, T3>>.Value => Value.ToTuple();

		public WatchableTuple(IWatchable<T1> watchable1, IWatchable<T2> watchable2, IWatchable<T3> watchable3)
		{
			Watchable1 = watchable1;
			Watchable2 = watchable2;
			Watchable3 = watchable3;
		}

		private void OnTupleValueChanged((T1, T2, T3) value) => tupleValueChanged?.Invoke(value.ToTuple());

		private void HandleElement1ValueChanged(T1 _) => valueChanged?.Invoke(Value);
		private void HandleElement2ValueChanged(T2 _) => valueChanged?.Invoke(Value);
		private void HandleElement3ValueChanged(T3 _) => valueChanged?.Invoke(Value);
	}

	internal class WatchableTuple<T1, T2, T3, T4> :
		IWatchable<(T1, T2, T3, T4)>, IWatchable<Tuple<T1, T2, T3, T4>>
	{
		private Action<(T1, T2, T3, T4)>? valueChanged;
		public event Action<(T1, T2, T3, T4)>? ValueChanged
		{
			add
			{
				if (valueChanged == null)
				{
					Watchable1.ValueChanged += HandleElement1ValueChanged;
					Watchable2.ValueChanged += HandleElement2ValueChanged;
					Watchable3.ValueChanged += HandleElement3ValueChanged;
					Watchable4.ValueChanged += HandleElement4ValueChanged;
				}
				valueChanged += value;
			}
			remove
			{
				valueChanged -= value;
				if (valueChanged == null)
				{
					Watchable1.ValueChanged -= HandleElement1ValueChanged;
					Watchable2.ValueChanged -= HandleElement2ValueChanged;
					Watchable3.ValueChanged -= HandleElement3ValueChanged;
					Watchable4.ValueChanged -= HandleElement4ValueChanged;
				}
			}
		}

		private Action<Tuple<T1, T2, T3, T4>>? tupleValueChanged;
		event Action<Tuple<T1, T2, T3, T4>>? IWatchable<Tuple<T1, T2, T3, T4>>.ValueChanged
		{
			add
			{
				if (tupleValueChanged == null)
					ValueChanged += OnTupleValueChanged;
				tupleValueChanged += value;
			}
			remove
			{
				tupleValueChanged -= value;
				if (tupleValueChanged == null)
					ValueChanged -= OnTupleValueChanged;
			}
		}

		public IWatchable<T1> Watchable1 { get; }
		public IWatchable<T2> Watchable2 { get; }
		public IWatchable<T3> Watchable3 { get; }
		public IWatchable<T4> Watchable4 { get; }

		public (T1, T2, T3, T4) Value => (Watchable1.Value, Watchable2.Value, Watchable3.Value, Watchable4.Value);
		Tuple<T1, T2, T3, T4> IWatchable<Tuple<T1, T2, T3, T4>>.Value => Value.ToTuple();

		public WatchableTuple(IWatchable<T1> watchable1, IWatchable<T2> watchable2, IWatchable<T3> watchable3, IWatchable<T4> watchable4)
		{
			Watchable1 = watchable1;
			Watchable2 = watchable2;
			Watchable3 = watchable3;
			Watchable4 = watchable4;
		}

		private void OnTupleValueChanged((T1, T2, T3, T4) value) => tupleValueChanged?.Invoke(value.ToTuple());

		private void HandleElement1ValueChanged(T1 _) => valueChanged?.Invoke(Value);
		private void HandleElement2ValueChanged(T2 _) => valueChanged?.Invoke(Value);
		private void HandleElement3ValueChanged(T3 _) => valueChanged?.Invoke(Value);
		private void HandleElement4ValueChanged(T4 _) => valueChanged?.Invoke(Value);
	}

	internal class WatchableTuple<T1, T2, T3, T4, T5> :
		IWatchable<(T1, T2, T3, T4, T5)>, IWatchable<Tuple<T1, T2, T3, T4, T5>>
	{
		private Action<(T1, T2, T3, T4, T5)>? valueChanged;
		public event Action<(T1, T2, T3, T4, T5)>? ValueChanged
		{
			add
			{
				if (valueChanged == null)
				{
					Watchable1.ValueChanged += HandleElement1ValueChanged;
					Watchable2.ValueChanged += HandleElement2ValueChanged;
					Watchable3.ValueChanged += HandleElement3ValueChanged;
					Watchable4.ValueChanged += HandleElement4ValueChanged;
					Watchable5.ValueChanged += HandleElement5ValueChanged;
				}
				valueChanged += value;
			}
			remove
			{
				valueChanged -= value;
				if (valueChanged == null)
				{
					Watchable1.ValueChanged -= HandleElement1ValueChanged;
					Watchable2.ValueChanged -= HandleElement2ValueChanged;
					Watchable3.ValueChanged -= HandleElement3ValueChanged;
					Watchable4.ValueChanged -= HandleElement4ValueChanged;
					Watchable5.ValueChanged -= HandleElement5ValueChanged;
				}
			}
		}

		private Action<Tuple<T1, T2, T3, T4, T5>>? tupleValueChanged;
		event Action<Tuple<T1, T2, T3, T4, T5>>? IWatchable<Tuple<T1, T2, T3, T4, T5>>.ValueChanged
		{
			add
			{
				if (tupleValueChanged == null)
					ValueChanged += OnTupleValueChanged;
				tupleValueChanged += value;
			}
			remove
			{
				tupleValueChanged -= value;
				if (tupleValueChanged == null)
					ValueChanged -= OnTupleValueChanged;
			}
		}

		public IWatchable<T1> Watchable1 { get; }
		public IWatchable<T2> Watchable2 { get; }
		public IWatchable<T3> Watchable3 { get; }
		public IWatchable<T4> Watchable4 { get; }
		public IWatchable<T5> Watchable5 { get; }

		public (T1, T2, T3, T4, T5) Value =>
			(Watchable1.Value, Watchable2.Value, Watchable3.Value, Watchable4.Value, Watchable5.Value);
		Tuple<T1, T2, T3, T4, T5> IWatchable<Tuple<T1, T2, T3, T4, T5>>.Value => Value.ToTuple();

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

		private void OnTupleValueChanged((T1, T2, T3, T4, T5) value) => tupleValueChanged?.Invoke(value.ToTuple());

		private void HandleElement1ValueChanged(T1 _) => valueChanged?.Invoke(Value);
		private void HandleElement2ValueChanged(T2 _) => valueChanged?.Invoke(Value);
		private void HandleElement3ValueChanged(T3 _) => valueChanged?.Invoke(Value);
		private void HandleElement4ValueChanged(T4 _) => valueChanged?.Invoke(Value);
		private void HandleElement5ValueChanged(T5 _) => valueChanged?.Invoke(Value);
	}

	internal class WatchableTuple<T1, T2, T3, T4, T5, T6> :
		IWatchable<(T1, T2, T3, T4, T5, T6)>, IWatchable<Tuple<T1, T2, T3, T4, T5, T6>>
	{
		private Action<(T1, T2, T3, T4, T5, T6)>? valueChanged;
		public event Action<(T1, T2, T3, T4, T5, T6)>? ValueChanged
		{
			add
			{
				if (valueChanged == null)
				{
					Watchable1.ValueChanged += HandleElement1ValueChanged;
					Watchable2.ValueChanged += HandleElement2ValueChanged;
					Watchable3.ValueChanged += HandleElement3ValueChanged;
					Watchable4.ValueChanged += HandleElement4ValueChanged;
					Watchable5.ValueChanged += HandleElement5ValueChanged;
					Watchable6.ValueChanged += HandleElement6ValueChanged;
				}
				valueChanged += value;
			}
			remove
			{
				valueChanged -= value;
				if (valueChanged == null)
				{
					Watchable1.ValueChanged -= HandleElement1ValueChanged;
					Watchable2.ValueChanged -= HandleElement2ValueChanged;
					Watchable3.ValueChanged -= HandleElement3ValueChanged;
					Watchable4.ValueChanged -= HandleElement4ValueChanged;
					Watchable5.ValueChanged -= HandleElement5ValueChanged;
					Watchable6.ValueChanged -= HandleElement6ValueChanged;
				}
			}
		}

		private Action<Tuple<T1, T2, T3, T4, T5, T6>>? tupleValueChanged;
		event Action<Tuple<T1, T2, T3, T4, T5, T6>>? IWatchable<Tuple<T1, T2, T3, T4, T5, T6>>.ValueChanged
		{
			add
			{
				if (tupleValueChanged == null)
					ValueChanged += OnTupleValueChanged;
				tupleValueChanged += value;
			}
			remove
			{
				tupleValueChanged -= value;
				if (tupleValueChanged == null)
					ValueChanged -= OnTupleValueChanged;
			}
		}

		public IWatchable<T1> Watchable1 { get; }
		public IWatchable<T2> Watchable2 { get; }
		public IWatchable<T3> Watchable3 { get; }
		public IWatchable<T4> Watchable4 { get; }
		public IWatchable<T5> Watchable5 { get; }
		public IWatchable<T6> Watchable6 { get; }

		public (T1, T2, T3, T4, T5, T6) Value =>
			(Watchable1.Value, Watchable2.Value, Watchable3.Value, Watchable4.Value, Watchable5.Value, Watchable6.Value);
		Tuple<T1, T2, T3, T4, T5, T6> IWatchable<Tuple<T1, T2, T3, T4, T5, T6>>.Value => Value.ToTuple();

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

		private void OnTupleValueChanged((T1, T2, T3, T4, T5, T6) value) => tupleValueChanged?.Invoke(value.ToTuple());

		private void HandleElement1ValueChanged(T1 _) => valueChanged?.Invoke(Value);
		private void HandleElement2ValueChanged(T2 _) => valueChanged?.Invoke(Value);
		private void HandleElement3ValueChanged(T3 _) => valueChanged?.Invoke(Value);
		private void HandleElement4ValueChanged(T4 _) => valueChanged?.Invoke(Value);
		private void HandleElement5ValueChanged(T5 _) => valueChanged?.Invoke(Value);
		private void HandleElement6ValueChanged(T6 _) => valueChanged?.Invoke(Value);
	}

	internal class WatchableTuple<T1, T2, T3, T4, T5, T6, T7> :
		IWatchable<(T1, T2, T3, T4, T5, T6, T7)>, IWatchable<Tuple<T1, T2, T3, T4, T5, T6, T7>>
	{
		private Action<(T1, T2, T3, T4, T5, T6, T7)>? valueChanged;
		public event Action<(T1, T2, T3, T4, T5, T6, T7)>? ValueChanged
		{
			add
			{
				if (valueChanged == null)
				{
					Watchable1.ValueChanged += HandleElement1ValueChanged;
					Watchable2.ValueChanged += HandleElement2ValueChanged;
					Watchable3.ValueChanged += HandleElement3ValueChanged;
					Watchable4.ValueChanged += HandleElement4ValueChanged;
					Watchable5.ValueChanged += HandleElement5ValueChanged;
					Watchable6.ValueChanged += HandleElement6ValueChanged;
					Watchable7.ValueChanged += HandleElement7ValueChanged;
				}
				valueChanged += value;
			}
			remove
			{
				valueChanged -= value;
				if (valueChanged == null)
				{
					Watchable1.ValueChanged -= HandleElement1ValueChanged;
					Watchable2.ValueChanged -= HandleElement2ValueChanged;
					Watchable3.ValueChanged -= HandleElement3ValueChanged;
					Watchable4.ValueChanged -= HandleElement4ValueChanged;
					Watchable5.ValueChanged -= HandleElement5ValueChanged;
					Watchable6.ValueChanged -= HandleElement6ValueChanged;
					Watchable7.ValueChanged -= HandleElement7ValueChanged;
				}
			}
		}

		private Action<Tuple<T1, T2, T3, T4, T5, T6, T7>>? tupleValueChanged;
		event Action<Tuple<T1, T2, T3, T4, T5, T6, T7>>? IWatchable<Tuple<T1, T2, T3, T4, T5, T6, T7>>.ValueChanged
		{
			add
			{
				if (tupleValueChanged == null)
					ValueChanged += OnTupleValueChanged;
				tupleValueChanged += value;
			}
			remove
			{
				tupleValueChanged -= value;
				if (tupleValueChanged == null)
					ValueChanged -= OnTupleValueChanged;
			}
		}

		public IWatchable<T1> Watchable1 { get; }
		public IWatchable<T2> Watchable2 { get; }
		public IWatchable<T3> Watchable3 { get; }
		public IWatchable<T4> Watchable4 { get; }
		public IWatchable<T5> Watchable5 { get; }
		public IWatchable<T6> Watchable6 { get; }
		public IWatchable<T7> Watchable7 { get; }

		public (T1, T2, T3, T4, T5, T6, T7) Value =>
			(Watchable1.Value, Watchable2.Value, Watchable3.Value, Watchable4.Value, Watchable5.Value, Watchable6.Value, Watchable7.Value);
		Tuple<T1, T2, T3, T4, T5, T6, T7> IWatchable<Tuple<T1, T2, T3, T4, T5, T6, T7>>.Value => Value.ToTuple();

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

		private void OnTupleValueChanged((T1, T2, T3, T4, T5, T6, T7) value) => tupleValueChanged?.Invoke(value.ToTuple());

		private void HandleElement1ValueChanged(T1 _) => valueChanged?.Invoke(Value);
		private void HandleElement2ValueChanged(T2 _) => valueChanged?.Invoke(Value);
		private void HandleElement3ValueChanged(T3 _) => valueChanged?.Invoke(Value);
		private void HandleElement4ValueChanged(T4 _) => valueChanged?.Invoke(Value);
		private void HandleElement5ValueChanged(T5 _) => valueChanged?.Invoke(Value);
		private void HandleElement6ValueChanged(T6 _) => valueChanged?.Invoke(Value);
		private void HandleElement7ValueChanged(T7 _) => valueChanged?.Invoke(Value);
	}
}
