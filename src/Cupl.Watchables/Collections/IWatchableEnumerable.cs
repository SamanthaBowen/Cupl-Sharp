#nullable enable

using System.Collections.Generic;

namespace Cupl.Watchables.Collections
{
	public interface IWatchableEnumerable<out T> : IWatchable<IEnumerable<T>>, IEnumerable<T>
	{
	}
}
