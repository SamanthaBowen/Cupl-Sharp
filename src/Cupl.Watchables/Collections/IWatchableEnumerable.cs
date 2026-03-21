#nullable enable

using System.Collections.Generic;

namespace Cupl.Watchables
{
	public interface IWatchableEnumerable<out T> : IWatchable<IEnumerable<T>>, IEnumerable<T>
	{
	}
}
