using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Cupl.Enumerables
{
	public static class StringUtils
	{
		public static string? NullIfEmpty(this string str)
		{
			if (str == string.Empty)
				return null;
			else
				return str;
		}

		public static int? IndexOfOrNull(this string str, char value)
		{
			var index = str.IndexOf(value);
			return (0 <= index) ? index : (int?)null;
		}

		public static int? LastIndexOfOrNull(this string str, char value)
		{
			var index = str.LastIndexOf(value);
			return (0 <= index) ? index : (int?)null;
		}

		public static bool TryRemovePrefix(this string str, string prefix, out string result)
		{
			if (str.StartsWith(prefix))
			{
				result = str[prefix.Length..];
				return true;
			}
			else
			{
				result = str;
				return false;
			}
		}

		public static bool TryRemovePrefix(this string str, char prefix, out string result)
		{
			if (str.StartsWith(prefix))
			{
				result = str[1..];
				return true;
			}
			else
			{
				result = str;
				return false;
			}
		}

		public static bool TryRemoveSuffix(this string str, string suffix, out string result)
		{
			if (str.EndsWith(suffix))
			{
				result = str[..^suffix.Length];
				return true;
			}
			else
			{
				result = str;
				return false;
			}
		}

		public static bool TryRemoveSuffix(this string str, char suffix, out string result)
		{
			if (str.EndsWith(suffix))
			{
				result = str[..^1];
				return true;
			}
			else
			{
				result = str;
				return false;
			}
		}

		public static string RemovePrefix(this string str, string prefix)
		{
			str.TryRemovePrefix(prefix, out var result);
			return result;
		}

		public static string RemovePrefix(this string str, char prefix)
		{
			str.TryRemovePrefix(prefix, out var result);
			return result;
		}

		public static string? RemovePrefixOrNull(this string str, string prefix)
		{
			return str.TryRemovePrefix(prefix, out var result) ? result : null;
		}

		public static string? RemovePrefixOrNull(this string str, char prefix)
		{
			return str.TryRemovePrefix(prefix, out var result) ? result : null;
		}

		public static string RemoveSuffix(this string str, string suffix)
		{
			str.TryRemoveSuffix(suffix, out var result);
			return result;
		}

		public static string RemoveSuffix(this string str, char suffix)
		{
			str.TryRemoveSuffix(suffix, out var result);
			return result;
		}

		public static string? RemoveSuffixOrNull(this string str, string suffix)
		{
			return str.TryRemoveSuffix(suffix, out var result) ? result : null;
		}

		public static string? RemoveSuffixOrNull(this string str, char suffix)
		{
			return str.TryRemoveSuffix(suffix, out var result) ? result : null;
		}
	}
}
