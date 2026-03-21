#nullable enable

using System;

namespace Cupl.Watchables
{
	internal struct MappedWatchable<T, U> :
		IWatchable<U>
	{
		public event Action<U>? ValueChanged
		{
			add => source.ValueChanged += GetSourceValueChangedHandler(value);
			remove => source.ValueChanged -= GetSourceValueChangedHandler(value);
		}

		private readonly IWatchable<T> source;
		private readonly Func<T, U> function;

		public U Value => function(source.Value);

		public MappedWatchable(IWatchable<T> source, Func<T, U> function)
		{
			this.source = source;
			this.function = function;
		}

		private readonly Action<T> GetSourceValueChangedHandler(Action<U>? handler)
		{
			var function = this.function;

			// By keeping the lambda here, it should be the same anonymous function each time this is called.
			return s => handler?.Invoke(function(s));
		}
	}
}
