#nullable enable

using System;

namespace Cupl.Watchables
{
	public interface IWatchable<out T>
	{
		public event Action<T>? ValueChanged;

		public T Value { get; }
	}
}
