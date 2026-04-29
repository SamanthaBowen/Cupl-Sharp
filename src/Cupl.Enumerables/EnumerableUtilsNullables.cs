using System;
using System.Collections.Generic;
using System.Linq;

namespace Cupl.Enumerables
{
	public static partial class EnumerableUtils
	{
		private static IEnumerable<T>? NotEmptyOrNull<T>(this IEnumerable<T> source) =>
			source.Any() ? source : null;

		public static T? ElementAtOrNull<T>(this IEnumerable<T> source, int index)
			where T : struct
		{
			return source.Cast<T?>().ElementAtOrDefault(index);
		}

		public static T? FirstOrNull<T>(this IEnumerable<T> source)
			where T : struct
		{
			return source.Cast<T?>().FirstOrDefault();
		}

		public static T? FirstOrNull<T>(this IEnumerable<T> source, Func<T, bool> predicate)
			where T : struct
		{
			return source.Where(predicate).FirstOrNull();
		}

		public static T? LastOrNull<T>(this IEnumerable<T> source)
			where T : struct
		{
			return source.Cast<T?>().LastOrDefault();
		}

		public static T? LastOrNull<T>(this IEnumerable<T> source, Func<T, bool> predicate)
			where T : struct
		{
			return source.Where(predicate).LastOrNull();
		}

		public static decimal? MaxOrNull(this IEnumerable<decimal> source) =>
			source.NotEmptyOrNull()?.Max();
		public static double? MaxOrNull(this IEnumerable<double> source) =>
			source.NotEmptyOrNull()?.Max();
		public static int? MaxOrNull(this IEnumerable<int> source) =>
			source.NotEmptyOrNull()?.Max();
		public static long? MaxOrNull(this IEnumerable<long> source) =>
			source.NotEmptyOrNull()?.Max();
		public static float? MaxOrNull(this IEnumerable<float> source) =>
			source.NotEmptyOrNull()?.Max();

		public static T? MaxOrNull<T>(this IEnumerable<T> source)
			where T : struct
		{
			return source.NotEmptyOrNull()?.Max();
		}

		public static TResult? MaxOrNull<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
			where TResult : struct
		{
			return source.NotEmptyOrNull()?.Max(selector);
		}

		public static decimal? MinOrNull(this IEnumerable<decimal> source) =>
			source.NotEmptyOrNull()?.Min();
		public static double? MinOrNull(this IEnumerable<double> source) =>
			source.NotEmptyOrNull()?.Min();
		public static int? MinOrNull(this IEnumerable<int> source) =>
			source.NotEmptyOrNull()?.Min();
		public static long? MinOrNull(this IEnumerable<long> source) =>
			source.NotEmptyOrNull()?.Min();
		public static float? MinOrNull(this IEnumerable<float> source) =>
			source.NotEmptyOrNull()?.Min();

		public static T? MinOrNull<T>(this IEnumerable<T> source)
			where T : struct
		{
			return source.NotEmptyOrNull()?.Min();
		}

		public static TResult? MinOrNull<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
			where TResult : struct
		{
			return source.NotEmptyOrNull()?.Min(selector);
		}

		#if !NETSTANDARD2_1

		public static TSource? MaxByOrNull<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
			where TSource : struct
		{
			return source.NotEmptyOrNull()?.MaxBy(keySelector);
		}

		public static TSource? MinByOrNull<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
			where TSource : struct
		{
			return source.NotEmptyOrNull()?.MinBy(keySelector);
		}

		#endif

		public static T? SingleOrNull<T>(this IEnumerable<T> source)
			where T : struct
		{
			return source.Cast<T?>().SingleOrDefault();
		}

		public static T? SingleOrNull<T>(this IEnumerable<T> source, Func<T, bool> predicate)
			where T : struct
		{
			return source.Where(predicate).SingleOrNull();
		}

		public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T?> source)
			where T : class
		{
			return (IEnumerable<T>)source.Where(x => x != null);
		}

		public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T?> source)
			where T : struct
		{
			return source.Where(x => x != null).Select(x => x!.Value);
		}
	}
}
