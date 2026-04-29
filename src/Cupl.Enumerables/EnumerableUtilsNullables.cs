using System;
using System.Collections.Generic;
using System.Linq;

namespace Cupl.Enumerables
{
	public static partial class EnumerableUtils
	{
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
			source.Max(x => new decimal?(x));
		public static double? MaxOrNull(this IEnumerable<double> source) =>
			source.Max(x => new double?(x));
		public static int? MaxOrNull(this IEnumerable<int> source) =>
			source.Max(x => new int?(x));
		public static long? MaxOrNull(this IEnumerable<long> source) =>
			source.Max(x => new long?(x));
		public static float? MaxOrNull(this IEnumerable<float> source) =>
			source.Max(x => new float?(x));

		public static T? MaxOrNull<T>(this IEnumerable<T> source)
			where T : struct
		{
			return source.Max(x => new T?(x));
		}

		public static decimal? MinOrNull(this IEnumerable<decimal> source) =>
			source.Min(x => new decimal?(x));
		public static double? MinOrNull(this IEnumerable<double> source) =>
			source.Min(x => new double?(x));
		public static int? MinOrNull(this IEnumerable<int> source) =>
			source.Min(x => new int?(x));
		public static long? MinOrNull(this IEnumerable<long> source) =>
			source.Min(x => new long?(x));
		public static float? MinOrNull(this IEnumerable<float> source) =>
			source.Min(x => new float?(x));

		public static T? MinOrNull<T>(this IEnumerable<T> source)
			where T : struct
		{
			return source.Min(x => new T?(x));
		}

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
