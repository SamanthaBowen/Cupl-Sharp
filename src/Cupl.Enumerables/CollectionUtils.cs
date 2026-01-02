using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Cupl.Enumerables
{
	public static class CollectionUtils
	{
		public static TValue? GetValueOrNull<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> source, TKey key)
			where TValue : struct
		{
			return source.TryGetValue(key, out var value) ? value : (TValue?)null;
		}

		public static int? IndexOfOrNull<T>(this IList<T> source, T value)
		{
			var index = source.IndexOf(value);
			return (0 <= index) ? index : (int?)null;
		}

		[return: MaybeNull]
		public static T PeekOrDefault<T>(this Queue<T> source)
		{
			return source.TryPeek(out var result) ? result : default;
		}

		public static T? PeekOrNull<T>(this Queue<T> source)
			where T : struct
		{
			return source.TryPeek(out var result) ? result : (T?)null;
		}

		[return: MaybeNull]
		public static T DequeueOrDefault<T>(this Queue<T> source)
		{
			return source.TryDequeue(out var result) ? result : default;
		}

		public static T? DequeueOrNull<T>(this Queue<T> source)
			where T : struct
		{
			return source.TryDequeue(out var result) ? result : (T?)null;
		}

		[return: MaybeNull]
		public static T PeekOrDefault<T>(this Stack<T> source)
		{
			return source.TryPeek(out var result) ? result : default;
		}

		public static T? PeekOrNull<T>(this Stack<T> source)
			where T : struct
		{
			return source.TryPeek(out var result) ? result : (T?)null;
		}

		[return: MaybeNull]
		public static T PopOrDefault<T>(this Stack<T> source)
		{
			return source.TryPop(out var result) ? result : default;
		}

		public static T? PopOrNull<T>(this Stack<T> source)
			where T : struct
		{
			return source.TryPop(out var result) ? result : (T?)null;
		}
	}
}
