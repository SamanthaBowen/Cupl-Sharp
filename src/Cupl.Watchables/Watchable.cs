#nullable enable

using System;
using System.Collections.Generic;
using Cupl.Watchables.Collections;

namespace Cupl.Watchables
{
	public static class Watchable
	{
		public static IWatchable<TResult> Map<TSource, TResult>(this IWatchable<TSource> source, Func<TSource, TResult> selector)
		{
			return new MappedWatchable<TSource, TResult>(source, selector);
		}

		public static IWatchable<TResult> Map<T1, T2, TResult>(this IWatchable<(T1, T2)> source, Func<T1, T2, TResult> selector)
		{
			return source.Map(tuple => selector(tuple.Item1, tuple.Item2));
		}

		public static IWatchable<TResult> Map<T1, T2, T3, TResult>(this IWatchable<(T1, T2, T3)> source, Func<T1, T2, T3, TResult> selector)
		{
			return source.Map(tuple => selector(tuple.Item1, tuple.Item2, tuple.Item3));
		}

		public static IWatchable<TResult> Map<T1, T2, T3, T4, TResult>(this IWatchable<(T1, T2, T3, T4)> source, Func<T1, T2, T3, T4, TResult> selector)
		{
			return source.Map(tuple => selector(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4));
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

		public static IWatchable<(T1, T2)> WatchMany<T1, T2>(this (IWatchable<T1>, IWatchable<T2>) tuple)
		{
			return new WatchableTuple<T1, T2>(tuple.Item1, tuple.Item2);
		}

		public static IWatchable<(T1, T2, T3)> WatchMany<T1, T2, T3>(this (IWatchable<T1>, IWatchable<T2>, IWatchable<T3>) tuple)
		{
			return new WatchableTuple<T1, T2, T3>(tuple.Item1, tuple.Item2, tuple.Item3);
		}

		public static IWatchable<(T1, T2, T3, T4)> WatchMany<T1, T2, T3, T4>(this (IWatchable<T1>, IWatchable<T2>, IWatchable<T3>, IWatchable<T4>) tuple)
		{
			return new WatchableTuple<T1, T2, T3, T4>(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4);
		}

		public static IWatchable<(T1, T2, T3, T4, T5)> WatchMany<T1, T2, T3, T4, T5>
		(
			this (IWatchable<T1>, IWatchable<T2>, IWatchable<T3>, IWatchable<T4>, IWatchable<T5>) tuple
		)
		{
			return new WatchableTuple<T1, T2, T3, T4, T5>(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5);
		}

		public static IWatchable<(T1, T2, T3, T4, T5, T6)> WatchMany<T1, T2, T3, T4, T5, T6>
		(
			this (IWatchable<T1>, IWatchable<T2>, IWatchable<T3>, IWatchable<T4>, IWatchable<T5>, IWatchable<T6>) tuple
		)
		{
			return
				new WatchableTuple<T1, T2, T3, T4, T5, T6>
				(
					tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5, tuple.Item6
				);
		}

		public static IWatchable<(T1, T2, T3, T4, T5, T6, T7)> WatchMany<T1, T2, T3, T4, T5, T6, T7>
		(
			this (IWatchable<T1>, IWatchable<T2>, IWatchable<T3>, IWatchable<T4>, IWatchable<T5>, IWatchable<T6>, IWatchable<T7>) tuple
		)
		{
			return
				new WatchableTuple<T1, T2, T3, T4, T5, T6, T7>
				(
					tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5, tuple.Item6, tuple.Item7
				);
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
