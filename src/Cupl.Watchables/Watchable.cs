#nullable enable

using System;
using System.Collections.Generic;

namespace Cupl.Watchables
{
	public static class Watchable
	{
		public static IWatchable<TResult> Map<TSource, TResult>(this IWatchable<TSource> source, Func<TSource, TResult> selector)
		{
			return new MappedWatchable<TSource, TResult>(source, selector);
		}

		public static IWatchable<TResult> Map<T0, T1, TResult>(this IWatchable<(T0, T1)> source, Func<T0, T1, TResult> selector)
		{
			return source.Map(tuple => selector(tuple.Item1, tuple.Item2));
		}

		public static IWatchable<T> Unwrap<T>(this IWatchable<IWatchable<T>> watchable)
		{
			return new UnwrappedWatchable<T>(watchable);
		}

		public static IWatchableEnumerable<T> WatchMany<T>(this IEnumerable<IWatchable<T>> watchables)
		{
			if (watchables is IWatchableEnumerable<IWatchable<T>> watchableEnumerable)
				return watchableEnumerable.WatchMany();
			else
				return new WatchableSequence<T>(watchables);
		}

		public static IWatchableEnumerable<T> WatchMany<T>(this IWatchableEnumerable<IWatchable<T>> watchables)
		{
			return watchables.Map(e => e.WatchMany()).Unwrap().ToWatchableEnumerable();
		}

		public static IWatchable<(T0, T1)> WatchMany<T0, T1>(this (IWatchable<T0>, IWatchable<T1>) tuple)
		{
			return new WatchableTuple<T0, T1>(tuple.Item1, tuple.Item2);
		}

		public static IWatchableEnumerable<T> ToWatchableEnumerable<T>(this IWatchable<IEnumerable<T>> watchable)
		{
			if (watchable is IWatchableEnumerable<T> watchableEnumerable)
				return watchableEnumerable;
			else
				return new WatchableEnumerable<T>(watchable);
		}
	}
}
