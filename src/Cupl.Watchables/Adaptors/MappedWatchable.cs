#nullable enable

using System;

namespace Cupl.Watchables
{
	internal class MappedWatchable<T, U> :
		IWatchable<U>
	{
		private Action<U>? valueChanged;
		public event Action<U>? ValueChanged
		{
			add
			{
				if (valueChanged == null)
					source.ValueChanged += HandleSourceValueChanged;
				valueChanged += value;
			}
			remove
			{
				valueChanged -= value;
				if (valueChanged == null)
					source.ValueChanged -= HandleSourceValueChanged;
			}
		}

		private readonly IWatchable<T> source;
		private readonly Func<T, U> function;

		public U Value => function(source.Value);

		public MappedWatchable(IWatchable<T> source, Func<T, U> function)
		{
			this.source = source;
			this.function = function;
		}

		private void HandleSourceValueChanged(T sourceValue)
		{
			valueChanged?.Invoke(function(sourceValue));
		}
	}
}
