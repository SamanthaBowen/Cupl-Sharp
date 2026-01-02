using System;
using System.Collections.Generic;
using System.Linq;

namespace Cupl.Enumerables
{
	public static partial class EnumerableUtils
	{
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

		public static T? ElementAtOrNull<T>(this IEnumerable<T> source, int index)
			where T : struct
		{
			return source.Cast<T?>().ElementAtOrDefault(index);
		}
	}
}
