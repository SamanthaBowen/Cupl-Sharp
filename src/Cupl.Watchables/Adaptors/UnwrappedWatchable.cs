#nullable enable

using System;

namespace Cupl.Watchables
{
	internal class UnwrappedWatchable<T> :
		IWatchable<T>
	{
		private Action<T>? valueChanged;
		public event Action<T>? ValueChanged
		{
			add
			{
				if (valueChanged == null)
				{
					inner = outer.Value;
					inner.ValueChanged += HandleInnerValueChanged;
					outer.ValueChanged += HandleOuterValueChanged;
				}

				valueChanged += value;
			}
			remove
			{
				valueChanged -= value;

				if (valueChanged == null)
				{
					outer.ValueChanged -= HandleOuterValueChanged;
					inner!.ValueChanged -= HandleInnerValueChanged;
					inner = null;
				}
			}
		}

		private IWatchable<IWatchable<T>> outer;
		private IWatchable<T>? inner;

		public T Value => (inner ?? outer.Value).Value;

		public UnwrappedWatchable(IWatchable<IWatchable<T>> outer)
		{
			this.outer = outer;
		}

		private void HandleOuterValueChanged(IWatchable<T> inner)
		{
			this.inner!.ValueChanged -= HandleInnerValueChanged;
			this.inner = inner;
			this.inner.ValueChanged += HandleInnerValueChanged;
			HandleInnerValueChanged(inner.Value);
		}

		private void HandleInnerValueChanged(T inner)
		{
			valueChanged?.Invoke(inner);
		}
	}
}
